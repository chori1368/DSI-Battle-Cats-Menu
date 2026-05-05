using UnityEngine.UIElements;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6_namespace
{
    public class Lab6 : MonoBehaviour
    {
        VisualElement botonCrear;
        Toggle toggleModificar;
        VisualElement contendor_dcha;
        TextField input_nombre;
        TextField input_apellido;
        Individuo individuoSelec; 
        VisualElement img1;
        VisualElement img2;
        VisualElement img3;
        List<Individuo> lista_individuos = new List<Individuo>();
        Button botonGuardar;
        string rutaArchivo;

        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            contendor_dcha = root.Q<VisualElement>("Dcha");
            input_nombre = root.Q<TextField>("InputNombre");
            input_apellido = root.Q<TextField>("InputApellido");
            botonCrear = root.Q<VisualElement>("BotonCrear");
            toggleModificar = root.Q<Toggle>("ToggleModificar");
            img1 = root.Q("Img1");
            img2 = root.Q("Img2");
            img3 = root.Q("Img3");
            botonGuardar = root.Q<Button>("BotonGuardar");
            botonGuardar.clicked += GuardarDatos;

            // Ruta del fichero (persistente)
            rutaArchivo = Application.persistentDataPath + "/individuos.json";

            contendor_dcha.RegisterCallback<ClickEvent>(seleccionTarjeta);
            botonCrear.RegisterCallback<ClickEvent>(NuevaTarjeta);
            input_nombre.RegisterCallback<ChangeEvent<string>>(CambioNombre);
            input_apellido.RegisterCallback<ChangeEvent<string>>(CambioApellido);
            img1.RegisterCallback<ClickEvent>(e => SeleccionarFotoDesdeUI(img1));
            img2.RegisterCallback<ClickEvent>(e => SeleccionarFotoDesdeUI(img2));
            img3.RegisterCallback<ClickEvent>(e => SeleccionarFotoDesdeUI(img3));

            CargarDatos();
        }

        void NuevaTarjeta(ClickEvent evt)
        {
            if (!toggleModificar.value)
            {
                VisualTreeAsset plantilla = Resources.Load<VisualTreeAsset>("Tarjeta");
                VisualElement tarjetaPlantilla = plantilla.Instantiate();

                contendor_dcha.Add(tarjetaPlantilla);
                tarjetas_borde_negro();
                tarjeta_borde_negro(tarjetaPlantilla);

                Individuo individuo = new Individuo(input_nombre.value, input_apellido.value);
                Tarjeta tar = new Tarjeta(tarjetaPlantilla, individuo);
                individuoSelec = individuo;

                lista_individuos.Add(individuo);
                string listaToJson = JsonHelperIndividuo.ToJson(lista_individuos, true);
                Debug.Log(listaToJson);

                List<Individuo> jsonToLista = JsonHelperIndividuo.FromJson<Individuo>(listaToJson);
                jsonToLista.ForEach(elem =>
                {
                    Debug.Log(elem);
                });
            }
        }

        void seleccionTarjeta(ClickEvent e)
        {
            VisualElement miTarjeta = e.target as VisualElement;
            individuoSelec = miTarjeta.userData as Individuo;

            input_nombre.SetValueWithoutNotify(individuoSelec.Nombre);
            input_apellido.SetValueWithoutNotify(individuoSelec.Apellido);
            toggleModificar.value = true;

            tarjetas_borde_negro();
            tarjeta_borde_negro(miTarjeta);
        }

        void CambioNombre(ChangeEvent<string> evt)
        {
            if (toggleModificar.value)
            {
                individuoSelec.Nombre = evt.newValue;
            }
        }

        void CambioApellido(ChangeEvent<string> evt)
        {
            if (toggleModificar.value)
            {
                individuoSelec.Apellido = evt.newValue;
            }
        }

        void tarjetas_borde_negro()
        {
            List<VisualElement> lista_tarjeta = contendor_dcha.Children().ToList();
            lista_tarjeta.ForEach(elem =>
            {
                VisualElement tarjeta = elem.Q("Tarjeta");

                tarjeta.style.borderBottomColor = Color.black;
                tarjeta.style.borderRightColor = Color.black;
                tarjeta.style.borderTopColor = Color.black;
                tarjeta.style.borderLeftColor = Color.black;
            });
        }
        void tarjeta_borde_negro(VisualElement tar)
        {
                VisualElement tarjeta = tar.Q("Tarjeta");

                tarjeta.style.borderBottomColor = Color.white;
                tarjeta.style.borderRightColor = Color.white;
                tarjeta.style.borderTopColor = Color.white;
                tarjeta.style.borderLeftColor = Color.white;
        }
        void GuardarDatos()
        {
            string json = JsonHelperIndividuo.ToJson(lista_individuos, true);
            System.IO.File.WriteAllText(rutaArchivo, json);

            Debug.Log("Datos guardados en: " + rutaArchivo);
        }

        void CargarDatos()
        {
            if (System.IO.File.Exists(rutaArchivo))
            {
                string json = System.IO.File.ReadAllText(rutaArchivo);
                lista_individuos = JsonHelperIndividuo.FromJson<Individuo>(json);

                foreach (Individuo ind in lista_individuos)
                {
                    if (!string.IsNullOrEmpty(ind.FotoID))
                    {
                        Texture2D tex = Resources.Load<Texture2D>("Fotos/" + ind.FotoID);
                        ind.Foto = tex;
                    }

                    VisualTreeAsset plantilla = Resources.Load<VisualTreeAsset>("Tarjeta");
                    VisualElement tarjetaPlantilla = plantilla.Instantiate();

                    contendor_dcha.Add(tarjetaPlantilla);
                    new Tarjeta(tarjetaPlantilla, ind);
                }

                Debug.Log("Datos cargados correctamente");
            }
        }

        void SeleccionarFotoDesdeUI(VisualElement img)
        {
            if (!toggleModificar.value) return;

            // Obtener la textura desde el background
            StyleBackground bg = img.resolvedStyle.backgroundImage;

            if (bg.value.sprite.texture != null)
            {
                individuoSelec.Foto = bg.value.sprite.texture;
                individuoSelec.FotoID = bg.value.sprite.texture.name;
            }
        }
    }
}