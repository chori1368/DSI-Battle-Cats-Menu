using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Proyecto : MonoBehaviour
{
    VisualElement root;

    // --- PANTALLAS ---
    VisualElement pantallaInicio;
    VisualElement pantallaSeleccion;
    VisualElement pantallaPrefabs;
    VisualElement pantallaCrear;
    List<VisualElement> pantallasList;

    // --- CONTROLES ---
    Button btnPlay, btnPrefabs, btnCrear;
    Button btnAtrasSeleccion, btnAtrasPrefabs, btnAtrasCrear; 
    Button btnGuardar, btnCrearCarta, btnBorrarFormacion;

    VisualElement contenedorCartas;
    VisualElement contenedorFormacion;
    
    Slider sliderVida, sliderDanio;
    CustomControl UIcrear_vidaControl;
    CustomControl UIcrear_danioControl;

    VisualElement gato1, gato2, gato3, gato4, gato5;

    VisualElement previewImagen;

    VisualTreeAsset tarjetaTemplate;
    List<Carta> baseDatos;
    Texture2D imagenSeleccionada;

    Carta cartaPreview;

    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        tarjetaTemplate = Resources.Load<VisualTreeAsset>("Tarjeta");
        baseDatos = BaseDatosCartas.GetData();

        ConfigurarPantallas();
        ConfigurarUI();
        
        MostrarPantalla(pantallaInicio);
    }

    void ConfigurarPantallas()
    {
        pantallaInicio = root.Q<VisualElement>("inicio");
        pantallaSeleccion = root.Q<VisualElement>("seleccion");
        pantallaPrefabs = root.Q<VisualElement>("menuprefabs");
        pantallaCrear = root.Q<VisualElement>("menucrear");

        pantallasList = new List<VisualElement>
        {
            pantallaInicio,
            pantallaSeleccion,
            pantallaPrefabs,
            pantallaCrear
        };
    }

    void MostrarPantalla(VisualElement pantallaAMostrar)
    {
        foreach (var pantalla in pantallasList)
        {
            if (pantalla != null)
            {
                pantalla.style.display = (pantalla == pantallaAMostrar) ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }
    }

    void ConfigurarUI()
    {
        btnPlay = root.Q<Button>("BtnPlay");
        btnPrefabs = root.Q<Button>("BtnPrefabs");
        btnCrear = root.Q<Button>("BtnCrear");

        btnAtrasSeleccion = pantallaSeleccion?.Q<Button>("BtnAtras");
        btnAtrasPrefabs = pantallaPrefabs?.Q<Button>("BtnAtras");
        btnAtrasCrear = pantallaCrear?.Q<Button>("BtnAtras");

        if (btnPlay != null) btnPlay.clicked += () => MostrarPantalla(pantallaSeleccion);
        if (btnPrefabs != null) btnPrefabs.clicked += () => 
        {
            GenerarCartas(); 
            MostrarPantalla(pantallaPrefabs);
        };
        if (btnCrear != null) btnCrear.clicked += () => MostrarPantalla(pantallaCrear);

        if (btnAtrasSeleccion != null) btnAtrasSeleccion.clicked += () => MostrarPantalla(pantallaInicio);
        if (btnAtrasPrefabs != null) btnAtrasPrefabs.clicked += () => MostrarPantalla(pantallaSeleccion);
        if (btnAtrasCrear != null) btnAtrasCrear.clicked += () => MostrarPantalla(pantallaSeleccion);

        contenedorCartas = root.Q<VisualElement>("Cartas");
        contenedorFormacion = root.Q<VisualElement>("Formacion");
        btnBorrarFormacion = root.Q<Button>("BtnBorrar");
        
        if (btnBorrarFormacion != null) btnBorrarFormacion.clicked += BorrarFormacion;

        sliderVida = root.Q<Slider>("SliderVida");
        sliderDanio = root.Q<Slider>("SliderDanio");
        
        var controles = pantallaCrear?.Query<CustomControl>().ToList();
        if (controles != null && controles.Count >= 2)
        {
            UIcrear_vidaControl = controles[0];
            UIcrear_danioControl = controles[1];
        }

        btnGuardar = root.Q<Button>("BtnGuardar");
        btnCrearCarta = root.Q<Button>("BtnCrearCarta");
        previewImagen = root.Q<VisualElement>("Preview");

        gato1 = root.Q<VisualElement>("gato1");
        gato2 = root.Q<VisualElement>("gato2");
        gato3 = root.Q<VisualElement>("gato3");
        gato4 = root.Q<VisualElement>("gato4");
        gato5 = root.Q<VisualElement>("gato5");

        if (gato1 != null) gato1.RegisterCallback<ClickEvent>(e => SeleccionarFotoDesdeUI(gato1));
        if (gato2 != null) gato2.RegisterCallback<ClickEvent>(e => SeleccionarFotoDesdeUI(gato2));
        if (gato3 != null) gato3.RegisterCallback<ClickEvent>(e => SeleccionarFotoDesdeUI(gato3));
        if (gato4 != null) gato4.RegisterCallback<ClickEvent>(e => SeleccionarFotoDesdeUI(gato4));
        if (gato5 != null) gato5.RegisterCallback<ClickEvent>(e => SeleccionarFotoDesdeUI(gato5));

        if (btnCrearCarta != null) btnCrearCarta.clicked += CrearCartaManual;
        if (btnGuardar != null) btnGuardar.clicked += GuardarJSON;

        ConfigurarPreviewEnVivo();
    }

    void SeleccionarFotoDesdeUI(VisualElement img)
    {
        StyleBackground bg = img.resolvedStyle.backgroundImage;

        if (bg.value.texture != null)
        {
            imagenSeleccionada = bg.value.texture;

            if (cartaPreview != null)
            {
                cartaPreview.Imagen = imagenSeleccionada;
                // Asignamos el nombre exacto de la textura para que se guarde en el JSON
                cartaPreview.ImagenID = imagenSeleccionada.name; 
            }
        }
    }

    void BorrarFormacion()
    {
        if (contenedorFormacion != null)
        {
            contenedorFormacion.Clear();
        }
    }

    void ConfigurarPreviewEnVivo()
    {
        if (sliderVida != null && sliderDanio != null && previewImagen != null)
        {
            cartaPreview = new Carta("Nueva", (int)sliderVida.value, (int)sliderDanio.value);
            
            cartaPreview.ImagenID = imagenSeleccionada != null ? imagenSeleccionada.name : "default";
            if (imagenSeleccionada != null) cartaPreview.Imagen = imagenSeleccionada;

            previewImagen.Clear();
            VisualElement previewElement = tarjetaTemplate.Instantiate();
            new TarjetaCarta(previewElement, cartaPreview); 
            previewImagen.Add(previewElement);

            sliderVida.RegisterValueChangedCallback(evt =>
            {
                if (cartaPreview != null) cartaPreview.Vida = (int)evt.newValue;
                if (UIcrear_vidaControl != null) UIcrear_vidaControl.Estado = (int)evt.newValue;
            });

            sliderDanio.RegisterValueChangedCallback(evt =>
            {
                if (cartaPreview != null) cartaPreview.Danio = (int)evt.newValue;
                if (UIcrear_danioControl != null) UIcrear_danioControl.Estado = (int)evt.newValue;
            });
        }
    }

    void GenerarCartas()
    {
        if (contenedorCartas == null) return;
        
        contenedorCartas.Clear();

        foreach (var carta in baseDatos)
        {
            VisualElement cardElement = tarjetaTemplate.Instantiate();
            new TarjetaCarta(cardElement, carta);

            cardElement.RegisterCallback<ClickEvent>(_ =>
            {
                VisualElement nuevaCardElement = tarjetaTemplate.Instantiate();
                new TarjetaCarta(nuevaCardElement, carta);
                contenedorFormacion.Add(nuevaCardElement);
            });

            contenedorCartas.Add(cardElement);
        }
    }

    void CrearCartaManual()
    {
        // Usamos los valores actuales (que ya son los de la cartaPreview)
        Carta nueva = new Carta("Nueva", (int)sliderVida.value, (int)sliderDanio.value);
        nueva.ImagenID = imagenSeleccionada != null ? imagenSeleccionada.name : "default";

        baseDatos.Add(nueva);
        Debug.Log("Carta añadida a la lista temporal.");
    }

    void GuardarJSON()
    {
        BaseDatosCartas.SaveData(baseDatos);
        Debug.Log("Json guardado en: " + Application.persistentDataPath);
    }
}