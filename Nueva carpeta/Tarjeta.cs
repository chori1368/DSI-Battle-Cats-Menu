using Lab6_namespace;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.VisualScripting.LudiqRootObjectEditor;

namespace Lab6_namespace
{
    public class Tarjeta
    {
        Individuo miIndividuo;
        VisualElement tarjetaRoot;

        Label nombreLabel;
        Label apellidoLabel;
        VisualElement imagen;

        public Tarjeta(VisualElement tarjetaRoot, Individuo individuo)
        {
            this.tarjetaRoot = tarjetaRoot;
            this.miIndividuo = individuo;

            nombreLabel = tarjetaRoot.Q<Label>("Nombre");
            apellidoLabel = tarjetaRoot.Q<Label>("Apellido");
            imagen = tarjetaRoot.Q<VisualElement>("cabeza");
            tarjetaRoot.userData = miIndividuo;

            UpdateUI();

            miIndividuo.Cambio += UpdateUI;
        }

        void UpdateUI()
        {
            nombreLabel.text = miIndividuo.Nombre;
            apellidoLabel.text = miIndividuo.Apellido;
            if (miIndividuo.Foto != null)
            {
                imagen.style.backgroundImage = new StyleBackground(miIndividuo.Foto);
            }
        }
    }
}   