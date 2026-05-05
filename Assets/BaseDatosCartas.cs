using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BaseDatosCartas
{
    public static List<Carta> GetData()
    {
        string ruta = Path.Combine(Application.persistentDataPath, "cartas.json");

        if (File.Exists(ruta))
        {
            string json = File.ReadAllText(ruta);
            List<Carta> datos = JsonHelperCarta.FromJson<Carta>(json);
            Debug.Log("Datos de cartas cargados desde JSON");
            return datos;
        }

        List<Carta> datosDefault = new List<Carta>();
        // Crear algunos datos por defecto si no existe el archivo
        Carta gatoBasico = new Carta("Gato Básico", 3, 2) { ImagenID = "gato_basico" };
        datosDefault.Add(gatoBasico);

        Debug.Log("Usando datos de cartas por defecto");
        return datosDefault;
    }

    public static void SaveData(List<Carta> cartas)
    {
        string ruta = Path.Combine(Application.persistentDataPath, "cartas.json");
        string json = JsonHelperCarta.ToJson(cartas, true);
        File.WriteAllText(ruta, json);
        Debug.Log("Cartas guardadas en: " + ruta);
    }
}