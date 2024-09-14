using Newtonsoft.Json;
using System.Collections.Generic;
using T_3D;

namespace EstructuraT3D.Estructura
{
    public class Parte
    {
        [JsonProperty("poligonos")]
        public Dictionary<string, Poligono> _poligonos = new Dictionary<string, Poligono>();

        [JsonProperty("centroDeMasa")]
        public Vector3DTO centroDeMasa { get; set; }

        public Parte(Vector3DTO centroMasa)
        {
            _poligonos = new Dictionary<string, Poligono>();
            this.centroDeMasa = centroMasa;
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

        public void Dibujar(Vector3DTO centroMasa)
        {
            foreach (var poligono in _poligonos.Values)
            {
                poligono.Dibujar(centroMasa);
            }
        }

        public void RotarParte(float angulo, Vector3DTO eje)
        {
            foreach (Poligono poligono in _poligonos.Values)
            {
                poligono.Rotar(angulo, eje);

            }
        }

        public void TrasladarParte(Vector3DTO desplazamiento)
        {
            // Trasladar cada polígono
            foreach (Poligono poligono in _poligonos.Values)
            {
                poligono.Trasladar(desplazamiento);
            }
            centroDeMasa.X += desplazamiento.X;
            centroDeMasa.Y += desplazamiento.Y;
            centroDeMasa.Z += desplazamiento.Z;
        }


    }
}
