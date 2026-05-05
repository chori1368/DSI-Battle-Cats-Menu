using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Lab8 : MonoBehaviour
{
    VisualElement contenido_azul;
    VisualElement contenido_verde;
    VisualElement contenido_ambar;
    VisualElement pestanya_azul;
    VisualElement pestanya_verde;
    VisualElement pestanya_ambar;
    Label textoazul;
    Label textoverde;
    Label textoambar;
    List<VisualElement> pestanyasList;
    List<VisualElement> contenidosList;

    private void MostrarContenido(int index)
    {
        for (int i = 0; i < contenidosList.Count; i++)
        {
            contenidosList[i].style.display =
                (i == index) ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }

    private void OnEnable()
    {
        UIDocument uidoc = GetComponent<UIDocument>();
        VisualElement rootve = uidoc.rootVisualElement;

        VisualElement contenido = rootve.Q("Contenido");
        VisualElement pestanyas = rootve.Q("Pestanyas");

        pestanya_azul = pestanyas.Q("Azul");
        pestanya_verde = pestanyas.Q("Verde");
        pestanya_ambar = pestanyas.Q("Ambar");

        contenido_azul = contenido.Q("Azul");
        contenido_verde = contenido.Q("Verde");
        contenido_ambar = contenido.Q("Ambar");

        pestanyasList = new List<VisualElement>(){
        pestanya_azul,
        pestanya_verde,
        pestanya_ambar
        };

        contenidosList = new List<VisualElement>(){
        contenido_azul,
        contenido_verde,
        contenido_ambar
        };

        for (int i = 0; i < pestanyasList.Count; i++)
        {
            int index = i; // importante (closure)
            pestanyasList[i].RegisterCallback<MouseDownEvent>(_ =>
            {
                Debug.Log("Pestaña " + index);
                MostrarContenido(index);
            });
        }

        textoazul = rootve.Q<Label>("textoazul");
        textoverde = rootve.Q<Label>("textoverde");
        textoambar = rootve.Q<Label>("textoambar");
        textoazul.text = @"<b><color=""green"">Cosas Azules importantes</b>";
        textoverde.text = @"<b><color=""blue"">Cosas Verdes importantes</b>";
        textoambar.text = @"<i>Cosas Ámbar importantes";
    }
}
