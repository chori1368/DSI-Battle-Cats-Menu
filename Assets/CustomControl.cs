using UnityEngine;
using UnityEngine.UIElements;

public class CustomControl : VisualElement
{
    VisualElement mana1 = new VisualElement();
    VisualElement mana2 = new VisualElement();
    VisualElement mana3 = new VisualElement();
    VisualElement mana4 = new VisualElement();
    VisualElement mana5 = new VisualElement();

    int estado;
    string archivo;

    public int Estado
    {
        get => estado;
        set
        {
            estado = value;
            encenderColor();
        }
    }

    public string Imagen
    {
        get => archivo;
        set
        {
            archivo = value;
            cargarImagen();
        }
    }

    void encenderColor()
    {
        mana1.style.opacity = 0.0f; 
        mana2.style.opacity = 0.0f;
        mana3.style.opacity = 0.0f;
        mana4.style.opacity = 0.0f;
        mana5.style.opacity = 0.0f;

        if (Estado >= 1) mana1.style.opacity = 1.0f;
        if (Estado >= 2) mana2.style.opacity = 1.0f;
        if (Estado >= 3) mana3.style.opacity = 1.0f;
        if (Estado >= 4) mana4.style.opacity = 1.0f;
        if (Estado >= 5) mana5.style.opacity = 1.0f;
    }

    void cargarImagen()
    {
        Texture2D imagen = Resources.Load<Texture2D>(archivo);
        StyleBackground mana = new StyleBackground(imagen);

        mana1.style.backgroundImage = mana;
        mana2.style.backgroundImage = mana;
        mana3.style.backgroundImage = mana;
        mana4.style.backgroundImage = mana;
        mana5.style.backgroundImage = mana;
    }
    public new class UxmlFactory : UxmlFactory<CustomControl, UxmlTraits> { };

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlIntAttributeDescription myEstado = new UxmlIntAttributeDescription { name = "estado", defaultValue = 3};
        UxmlStringAttributeDescription myImage = new UxmlStringAttributeDescription { name = "imagen", defaultValue = "mana"};

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var manabar = ve as CustomControl;
            manabar.Estado = myEstado.GetValueFromBag(bag, cc);
            manabar.Imagen = myImage.GetValueFromBag(bag, cc);
        }

    }
    public CustomControl()
    {

        mana1.style.width = 100;
        mana1.style.height = 50;

        mana2.style.width = 100;
        mana2.style.height = 50;

        mana3.style.width = 100;
        mana3.style.height = 50;

        mana4.style.width = 100;
        mana4.style.height = 50;

        mana5.style.width = 100;
        mana5.style.height = 50;

        hierarchy.Add(mana1);
        hierarchy.Add(mana2);
        hierarchy.Add(mana3);
        hierarchy.Add(mana4);
        hierarchy.Add(mana5);
    }

}
