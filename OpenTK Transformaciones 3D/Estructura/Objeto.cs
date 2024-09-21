using Newtonsoft.Json;
using OPTK_Transformaciones_3D.Estructura.DTO;
using System.Collections.Generic;

namespace OPTK_Transformaciones_3D.Estructura
{
    public class Objeto
    {
        [JsonProperty("partes")]
        public Dictionary<string, Parte> _partes;

        [JsonProperty("centroDeMasa")]
        public Vector3DTO centroDeMasa { get; set; }

        public Objeto()
        {
            _partes = new Dictionary<string, Parte>();
            centroDeMasa = new Vector3DTO(0, 0, 0);
        }

        public Objeto(Dictionary<string, Parte> partes)
        {
            _partes = partes;
            centroDeMasa = new Vector3DTO(0, 0, 0); // Inicializa el centro de masa si es necesario
        }

        public void AgregarParte(string nombre, Parte parte)
        {
            _partes[nombre] = parte;
        }

        public Dictionary<string, Parte> GetPartes()
        {
            return _partes;
        }

        public void Dibujar()
        {
            foreach (var parte in _partes.Values)
            {
                parte.centroDeMasa = centroDeMasa;
                parte.Dibujar();
            }
        }

        public void DibujarLineas()
        {
            foreach (var parte in _partes.Values)
            {
                parte.centroDeMasa = centroDeMasa;
                parte.DibujarLineas();
            }
        }

        public void RotarObjeto(float angulo, Vector3DTO eje, bool trasladarAlOrigen = false)
        {
            Vector3DTO centroDeMasaObjeto = CalcularCentroDeMasaObjeto();

            foreach (Parte parte in _partes.Values)
            {
                foreach (Poligono poligono in parte.GetPoligonos().Values)
                {
                    if (!trasladarAlOrigen)
                    {
                        poligono.Trasladar(new Vector3DTO(-centroDeMasaObjeto.X, -centroDeMasaObjeto.Y, -centroDeMasaObjeto.Z));
                    }

                    poligono.Rotar(angulo, eje);

                    if (!trasladarAlOrigen)
                    {
                        poligono.Trasladar(centroDeMasaObjeto);
                    }
                }
            }
        }

        private Vector3DTO CalcularCentroDeMasaObjeto()
        {
            float sumaX = 0, sumaY = 0, sumaZ = 0;
            int totalPuntos = 0;

            foreach (Parte parte in _partes.Values)
            {
                foreach (var poligono in parte.GetPoligonos().Values)
                {
                    foreach (var punto in poligono.Puntos)
                    {
                        sumaX += punto.X;
                        sumaY += punto.Y;
                        sumaZ += punto.Z;
                        totalPuntos++;
                    }
                }
            }

            if (totalPuntos == 0) return new Vector3DTO(0, 0, 0);

            return new Vector3DTO(sumaX / totalPuntos, sumaY / totalPuntos, sumaZ / totalPuntos);
        }

        public void TrasladarObjeto(Vector3DTO desplazamiento)
        {
            foreach (Parte parte in _partes.Values)
            {
                parte.TrasladarParte(desplazamiento);
            }

            ActualizarCentroDeMasa(desplazamiento);
        }

        public void EscalarObjeto(Vector3DTO factorEscala)
        {
            foreach (Parte parte in _partes.Values)
            {
                parte.EscalarParte(factorEscala);
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
