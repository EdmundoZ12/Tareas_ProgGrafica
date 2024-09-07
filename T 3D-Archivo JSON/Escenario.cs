using Newtonsoft.Json;
using System.Collections.Generic;

namespace T_3D
{
    public class Escenario
    {
        [JsonProperty("partes")]
        private Dictionary<string, Parte> _partes;

        // Constructor sin parámetros
        public Escenario()
        {
            _partes = new Dictionary<string, Parte>();
        }

        // Constructor con un diccionario de partes
        public Escenario(Dictionary<string, Parte> partes)
        {
            _partes = partes;
        }

        // Método para agregar una parte al escenario
        public void AgregarParte(string nombre, Parte parte)
        {
            _partes[nombre] = parte;
        }

        public Dictionary<string, Parte> GetPartes()
        {
            return _partes;
        }

        // Método para dibujar todas las partes del escenario
        public void Dibujar()
        {
            foreach (var parte in _partes.Values)
            {
                parte.Dibujar();
            }
        }
    }
}
