using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Lab6_namespace
{
    public class Basedatos
    {
        public static List<Individuo> getData()
        {
            string ruta = Application.persistentDataPath + "/individuos.json";

            if (File.Exists(ruta))
            {
                string json = File.ReadAllText(ruta);
                List<Individuo> datos = JsonHelperIndividuo.FromJson<Individuo>(json);

                Debug.Log("Datos cargados desde JSON");
                return datos;
            }

            List<Individuo> datosDefault = new List<Individuo>();

            Individuo perico = new Individuo("Perico", "Palotes");
            Individuo tornasol = new Individuo("Tornasol", "Tornasolado");
            Individuo luca = new Individuo("Luca", "Lucatelli");
            Individuo ivan = new Individuo("Ivan", "Ivanovich");

            datosDefault.Add(perico);
            datosDefault.Add(tornasol);
            datosDefault.Add(luca);
            datosDefault.Add(ivan);

            Debug.Log("Usando datos por defecto");
            return datosDefault;
        }
    }
}