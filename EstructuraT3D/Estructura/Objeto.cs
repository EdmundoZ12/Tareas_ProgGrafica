using Newtonsoft.Json;
using System.Collections.Generic;
using T_3D;

namespace EstructuraT3D.Estructura
{
    public class Objeto
    {
        [JsonProperty("partes")]
        public Dictionary<string, Parte> _partes;

        [JsonProperty("centroDeMasa")]
        public Vector3DTO centroDeMasa { get; set; }

        public Objeto(Vector3DTO centroDeMasa)
        {
            _partes = new Dictionary<string, Parte>();
            this.centroDeMasa = centroDeMasa;
        }

        public Objeto()
        {
            _partes = new Dictionary<string, Parte>();
        }

        public Objeto(Dictionary<string, Parte> partes)
        {
            _partes = partes;
        }

        public void AgregarParte(string nombre, Parte parte)
        {
            _partes[nombre] = parte;
        }

        public Dictionary<string, Parte> GetPartes()
        {
            return _partes;
        }

        public void Dibujar(Vector3DTO centroMasa)
        {
            foreach (var parte in _partes.Values)
            {
                parte.Dibujar(centroMasa);
            }
        }

        public void RotarObjeto(float angulo, Vector3DTO eje)
        {
            foreach (Parte parte in _partes.Values)
            {
                parte.RotarParte(angulo, eje);

            }
        }

        public void TrasladarObjeto(Vector3DTO desplazamiento)
        {
            // Trasladar cada parte del objeto
            foreach (Parte parte in _partes.Values)
            {
                parte.TrasladarParte(desplazamiento);
            }

            // Actualizar el centro de masa del objeto
            centroDeMasa.X += desplazamiento.X;
            centroDeMasa.Y += desplazamiento.Y;
            centroDeMasa.Z += desplazamiento.Z;
        }


    }
}
