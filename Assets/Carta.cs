using System;
using UnityEngine;

[Serializable]
public class Carta
{
    public event Action Cambio;

    [SerializeField] private string nombre;
    [SerializeField] private int vida;
    [SerializeField] private int danio;
    [SerializeField] private string imagenID;

    [NonSerialized] private Texture2D imagen;

    public string Nombre
    {
        get => nombre;
        set
        {
            if (value != nombre)
            {
                nombre = value;
                Cambio?.Invoke();
            }
        }
    }

    public int Vida
    {
        get => vida;
        set
        {
            if (value != vida)
            {
                vida = value;
                Cambio?.Invoke();
            }
        }
    }

    public int Danio
    {
        get => danio;
        set
        {
            if (value != danio)
            {
                danio = value;
                Cambio?.Invoke();
            }
        }
    }

    public Texture2D Imagen
    {
        get => imagen;
        set
        {
            if (value != imagen)
            {
                imagen = value;
                if (value != null)
                    imagenID = value.name;
                Cambio?.Invoke();
            }
        }
    }

    public string ImagenID
    {
        get => imagenID;
        set => imagenID = value;
    }

    public Carta(string nombre, int vida, int danio)
    {
        this.nombre = nombre;
        this.vida = vida;
        this.danio = danio;
    }
}