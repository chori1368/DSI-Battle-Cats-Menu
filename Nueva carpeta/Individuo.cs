using Lab6_namespace;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Lab6_namespace
{
    [System.Serializable]
    public class Individuo
    {
        public event Action Cambio;

        [SerializeField] private string nombre;
        [SerializeField] private string apellido;
        [SerializeField] private string fotoID;

        [NonSerialized] private Texture2D foto;

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

        public string Apellido
        {
            get => apellido;
            set
            {
                if (value != apellido)
                {
                    apellido = value;
                    Cambio?.Invoke();
                }
            }
        }

        public Texture2D Foto
        {
            get => foto;
            set
            {
                if (value != foto)
                {
                    foto = value;

                    if (value != null)
                        fotoID = value.name;

                    Cambio?.Invoke();
                }
            }
        }

        public string FotoID
        {
            get => fotoID;
            set => fotoID = value;
        }

        public Individuo(string nombre, string apellido)
        {
            this.nombre = nombre;
            this.apellido = apellido;
        }
    }
}