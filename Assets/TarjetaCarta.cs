using UnityEngine;
using UnityEngine.UIElements;

public class TarjetaCarta
{
    Carta miCarta;
    VisualElement tarjetaRoot;

    Label nombreLabel;
    
    CustomControl vidaControl;
    CustomControl danioControl;
    
    VisualElement imagen;

    public TarjetaCarta(VisualElement tarjetaRoot, Carta carta)
    {
        this.tarjetaRoot = tarjetaRoot;
        this.miCarta = carta;

        nombreLabel = tarjetaRoot.Q<Label>("Nombre");
        
        var controles = tarjetaRoot.Query<CustomControl>().ToList();
        if (controles.Count >= 2)
        {
            vidaControl = controles[0];
            danioControl = controles[1];
        }
        else
        {
            vidaControl = tarjetaRoot.Q<CustomControl>("VidaControl");
            danioControl = tarjetaRoot.Q<CustomControl>("DanioControl");
        }

        imagen = tarjetaRoot.Q<VisualElement>("Imagen");
        
        tarjetaRoot.userData = miCarta;

        // Se suscribe para reaccionar los cambios en la lógica
        miCarta.Cambio += UpdateUI;

        // Intenta cargar la textura desde Resources inmediatamente
        CargarTexturaBase();

        UpdateUI();
    }

    void CargarTexturaBase()
    {
        // Si no tiene textura asignada, pero sí tiene ID, la cargamos
        if (miCarta.Imagen == null && !string.IsNullOrEmpty(miCarta.ImagenID))
        {
            // Intentar cargar la textura por nombre desde una carpeta Resources.
            // NOTA: Asegúrate de que tus imágenes están en la carpeta "Resources/imagenes_gatos" de tu proyecto Unity.
            Texture2D tex = Resources.Load<Texture2D>("imagenes_gatos/" + miCarta.ImagenID);
            
            // Fallback por si la imagen está en la pura raíz de la carpeta "Resources"
            if (tex == null) tex = Resources.Load<Texture2D>(miCarta.ImagenID);

            if (tex != null)
            {
                // Asignarlo sin invocar UpdateUI otra vez de inmediato, o se entra en loop
                miCarta.Imagen = tex; 
            }
            else
            {
                Debug.LogWarning($"No se encontró la imagen '{miCarta.ImagenID}' en la carpeta Resources.");
            }
        }
    }

    void UpdateUI()
    {
        if (nombreLabel != null) nombreLabel.text = miCarta.Nombre;
        
        if (vidaControl != null) vidaControl.Estado = miCarta.Vida;
        if (danioControl != null) danioControl.Estado = miCarta.Danio;
        
        // Aplica el Background
        if (miCarta.Imagen != null && imagen != null)
        {
            imagen.style.backgroundImage = new StyleBackground(miCarta.Imagen);
        }
    }
}