using Newtonsoft.Json;

namespace OPTK_Transformaciones_3D.Estructura
{
    public class JsonSerializar
    {
        // Método para serializar un objeto y guardarlo en un archivo en la misma ubicación que esta clase
        public static void GuardarComoJson<T>(T objeto, string nombreArchivo)
        {
            try
            {
                // Asegúrate de que el nombre del archivo tenga la extensión .json
                if (!nombreArchivo.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                {
                    nombreArchivo += ".json";
                }

                string json = JsonConvert.SerializeObject(objeto, Formatting.Indented);
                string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nombreArchivo);

                File.WriteAllText(ruta, json);
                Console.WriteLine($"Archivo guardado en: {ruta}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el archivo: {ex.Message}");
            }
        }

        // Método para leer un archivo JSON desde una ruta específica y deserializarlo en un objeto
        public static T CargarDesdeJson<T>(string rutaArchivo)
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    Console.WriteLine("El archivo no existe en la ruta especificada.");
                    return default;
                }

                string json = File.ReadAllText(rutaArchivo);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el archivo: {ex.Message}");
                return default;
            }
        }
    }
}
