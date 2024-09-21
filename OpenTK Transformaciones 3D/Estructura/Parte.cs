using Newtonsoft.Json;
using OPTK_Transformaciones_3D.Estructura.DTO;
using System.Collections.Generic;

namespace OPTK_Transformaciones_3D.Estructura
{
    public class Parte
    {
        [JsonProperty("poligonos")]
        public Dictionary<string, Poligono> _poligonos = new Dictionary<string, Poligono>();

        [JsonProperty("centroDeMasa")]
        public Vector3DTO centroDeMasa { get; set; }

        public Parte()
        {
            _poligonos = new Dictionary<string, Poligono>();
            centroDeMasa = new Vector3DTO();
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
                poligono.centroDeMasa = centroDeMasa;
                poligono.Dibujar();
            }
        }

        public void DibujarLineas()
        {
            foreach (var poligono in _poligonos.Values)
            {
                poligono.centroDeMasa = centroDeMasa;
                poligono.DibujarLineas();
            }
        }

        public void RotarParte(float angulo, Vector3DTO eje, bool trasladarAlOrigen = false)
        {
            Vector3DTO centroDeMasaParte = CalcularCentroDeMasaParte();

            foreach (Poligono poligono in _poligonos.Values)
            {
                if (!trasladarAlOrigen)
                {
                    poligono.Trasladar(new Vector3DTO(-centroDeMasaParte.X, -centroDeMasaParte.Y, -centroDeMasaParte.Z));
                }

                poligono.Rotar(angulo, eje);

                if (!trasladarAlOrigen)
                {
                    poligono.Trasladar(centroDeMasaParte);
                }
            }
        }

        private Vector3DTO CalcularCentroDeMasaParte()
        {
            float sumaX = 0, sumaY = 0, sumaZ = 0;
            int totalPuntos = 0;

            foreach (var poligono in _poligonos.Values)
            {
                foreach (var punto in poligono.Puntos)
                {
                    sumaX += punto.X;
                    sumaY += punto.Y;
                    sumaZ += punto.Z;
                    totalPuntos++;
                }
            }

            if (totalPuntos == 0) return new Vector3DTO(0, 0, 0);

            return new Vector3DTO(sumaX / totalPuntos, sumaY / totalPuntos, sumaZ / totalPuntos);
        }

        public void TrasladarParte(Vector3DTO desplazamiento)
        {
            foreach (Poligono poligono in _poligonos.Values)
            {
                poligono.Trasladar(desplazamiento);
            }
            ActualizarCentroDeMasa(desplazamiento);
        }

        public void EscalarParte(Vector3DTO factorEscala)
        {
            foreach (Poligono poligono in _poligonos.Values)
            {
                poligono.EscalarPoligono(factorEscala);
            }
        }

        private void ActualizarCentroDeMasa(Vector3DTO desplazamiento)
        {
            centroDeMasa.X += desplazamiento.X;
            centroDeMasa.Y += desplazamiento.Y;
            centroDeMasa.Z += desplazamiento.Z;
        }
    }
}
