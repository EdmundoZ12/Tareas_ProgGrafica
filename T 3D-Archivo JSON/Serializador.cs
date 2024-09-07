using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Linq;
using OpenTK;
using System.Dynamic;

namespace T_3D
{
    public static class Serializador
    {
        public static void GuardarEscenarioManual(Escenario escenario)
        {
            // Configurar la cultura para usar punto como separador decimal
            var culture = new CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            // Ruta fija
            var path = @"D:\E.J.Z.R\Universidad\8.-Octavo Semestre\Programacion Grafica\C#\T 3D-Archivo JSON\data\escenario.json";

            // Crear un StringBuilder para construir el JSON manualmente
            var sb = new StringBuilder();
            sb.Append("{\n");

            // Recorrer las partes del escenario
            sb.Append("  \"partes\": {\n");
            var partes = escenario.GetPartes();
            foreach (var parteEntry in partes)
            {
                var parteNombre = parteEntry.Key;
                var parte = parteEntry.Value;

                sb.Append($"    \"{parteNombre}\": {{\n");

                var poligonos = parte.GetPoligonos();
                foreach (var poligonoEntry in poligonos)
                {
                    var poligonoNombre = poligonoEntry.Key;
                    var poligono = poligonoEntry.Value;

                    sb.Append($"      \"{poligonoNombre}\": {{\n");

                    // Recorrer los puntos de cada polígono
                    sb.Append("        \"puntos\": [\n");
                    var puntos = poligono.Puntos;
                    for (int i = 0; i < puntos.Count; i++)
                    {
                        var punto = puntos[i];
                        sb.Append($"          {{\"x\": {punto.X.ToString(culture)}, \"y\": {punto.Y.ToString(culture)}, \"z\": {punto.Z.ToString(culture)}}}");
                        if (i < puntos.Count - 1)
                            sb.Append(",\n");
                        else
                            sb.Append("\n");
                    }
                    sb.Append("        ],\n");

                    // Agregar el centro de masa
                    sb.Append($"        \"centroDeMasa\": {{\"x\": {poligono.CentroDeMasa.X.ToString(culture)}, \"y\": {poligono.CentroDeMasa.Y.ToString(culture)}, \"z\": {poligono.CentroDeMasa.Z.ToString(culture)}}}\n");

                    sb.Append("      }");
                    // Si no es el último polígono, agregar una coma
                    if (poligonoEntry.Key != poligonos.Keys.Last())
                        sb.Append(",\n");
                    else
                        sb.Append("\n");
                }
                sb.Append("    }");
                // Si no es la última parte, agregar una coma
                if (parteNombre != partes.Keys.Last())
                    sb.Append(",\n");
                else
                    sb.Append("\n");
            }
            sb.Append("  }\n");
            sb.Append('}');

            // Escribir el JSON al archivo
            File.WriteAllText(path, sb.ToString());

            // Mensaje de confirmación
            Console.WriteLine($"El archivo ha sido guardado correctamente en: {path}");
        }

        public static Escenario CargarEscenarioDesdeArchivo(string archivoJson)
        {
            // Leer el archivo JSON
            string json = File.ReadAllText(archivoJson);

            // Deserializar el JSON en un objeto dinámico
            dynamic data = JsonConvert.DeserializeObject<ExpandoObject>(json);

            // Crear el escenario
            Escenario escenario = new Escenario();

            // Recorrer las partes
            foreach (var parteEntry in data.partes)
            {
                string parteNombre = parteEntry.Key;
                Parte parte = new Parte();

                // Recorrer los polígonos de cada parte
                foreach (var poligonoEntry in parteEntry.Value)
                {
                    string poligonoNombre = poligonoEntry.Key;

                    // Crear los puntos del polígono
                    List<Punto> puntos = new List<Punto>();
                    foreach (var puntoEntry in poligonoEntry.Value.puntos)
                    {
                        float x = (float)puntoEntry.x;
                        float y = (float)puntoEntry.y;
                        float z = (float)puntoEntry.z;
                        puntos.Add(new Punto(x, y, z));
                    }

                    // Crear el centro de masa
                    float centroX = (float)poligonoEntry.Value.centroDeMasa.x;
                    float centroY = (float)poligonoEntry.Value.centroDeMasa.y;
                    float centroZ = (float)poligonoEntry.Value.centroDeMasa.z;
                    Vector3 centroDeMasa = new Vector3(centroX, centroY, centroZ);

                    Poligono poligono = new Poligono(puntos, centroDeMasa);
                    parte.AgregarPoligono(poligonoNombre, poligono);
                }

                // Agregar la parte al escenario
                escenario.AgregarParte(parteNombre, parte);
            }

            return escenario;
        }
    }
}
