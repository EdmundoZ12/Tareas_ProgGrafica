using Newtonsoft.Json;
using System.Collections.Generic;

namespace T_3D
{
    public class Parte
    {
        [JsonProperty("poligonos")]
        private Dictionary<string, Poligono> _poligonos = new Dictionary<string, Poligono>();

        public Parte()
        {
            _poligonos = new Dictionary<string, Poligono>();
        }

        public void AgregarPoligono(string nombre, Poligono poligono)
        {
            _poligonos[nombre] = poligono;
        }

        public Poligono ObtenerPoligono(string nombre)
        {
            return _poligonos.TryGetValue(nombre, out var poligono) ? poligono : null;
        }

        public Dictionary<string, Poligono> GetPoligonos()
        {
            return _poligonos;
        }

        public void Dibujar()
        {
            foreach (var poligono in _poligonos.Values)
            {
                poligono.Dibujar();
            }
        }
    }
}
