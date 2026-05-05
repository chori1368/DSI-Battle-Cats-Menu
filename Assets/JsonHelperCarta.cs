using System;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelperCarta
{
    public static List<T> FromJson<T>(string json)
    {
        ListaWrapper<T> wrapper = JsonUtility.FromJson<ListaWrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(List<T> lista, bool prettyPrint = false)
    {
        ListaWrapper<T> wrapper = new ListaWrapper<T> { Items = lista };
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class ListaWrapper<T>
    {
        public List<T> Items;
    }
}