using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Lab5b_namespace;

namespace Lab5c_namespace
{
    public class Lab5c : MonoBehaviour
    {
        List<Individuo> individuos;
        Individuo selecIndividuo;

        VisualElement tarjeta1;
        VisualElement tarjeta2;
        VisualElement tarjeta3;
        VisualElement tarjeta4;

        TextField input_nombre;
        TextField input_apellido;
        VisualElement img1;
        VisualElement img2;
        VisualElement img3;
        [SerializeField] Texture2D foto1;
        [SerializeField] Texture2D foto2;
        [SerializeField] Texture2D foto3;

        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            tarjeta1 = root.Q("Tarjeta1");
            tarjeta2 = root.Q("Tarjeta2");
            tarjeta3 = root.Q("Tarjeta3");
            tarjeta4 = root.Q("Tarjeta4");
            

            input_nombre = root.Q<TextField>("InputNombre");
            input_apellido = root.Q<TextField>("InputApellido");
            img1 = root.Q("Img1");
            img2 = root.Q("Img2");
            img3 = root.Q("Img3");

            individuos = Basedatos.getData();

            VisualElement panelDcha = root.Q("Dcha");
            panelDcha.RegisterCallback<ClickEvent>(seleccionTarjeta);

            input_nombre.RegisterCallback<ChangeEvent<string>>(CambioNombre);
            input_apellido.RegisterCallback<ChangeEvent<string>>(CambioApellido);
            img1.RegisterCallback<ClickEvent>(e => CambioFoto(foto1));
            img2.RegisterCallback<ClickEvent>(e => CambioFoto(foto2));
            img3.RegisterCallback<ClickEvent>(e => CambioFoto(foto3));

            InitializeUI();
        }
        void CambioNombre(ChangeEvent<string> evt)
        {
            selecIndividuo.Nombre = evt.newValue;
        }
        void CambioApellido(ChangeEvent<string> evt)
        {
            selecIndividuo.Apellido = evt.newValue;
        }
        void CambioFoto(Texture2D nuevaFoto)
        {
            if (selecIndividuo != null)
            {
                selecIndividuo.Foto = nuevaFoto;
            }
        }
        void seleccionTarjeta(ClickEvent e)
        { 
            VisualElement tarjeta = e.target as VisualElement;
            selecIndividuo = tarjeta.userData as Individuo;

            input_nombre.SetValueWithoutNotify(selecIndividuo.Nombre);
            input_apellido.SetValueWithoutNotify(selecIndividuo.Apellido);
        }   

        void InitializeUI()
        {
            Tarjeta tar1 = new Tarjeta(tarjeta1, individuos[0]);
            Tarjeta tar2 = new Tarjeta(tarjeta2, individuos[1]);
            Tarjeta tar3 = new Tarjeta(tarjeta3, individuos[2]);
            Tarjeta tar4 = new Tarjeta(tarjeta4, individuos[3]);
        }
    }
}