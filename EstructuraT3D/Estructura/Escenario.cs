using Newtonsoft.Json;
using System.Collections.Generic;
using T_3D;

namespace EstructuraT3D.Estructura
{
    public class Escenario
    {
        [JsonProperty("objetos")]
        public Dictionary<string, Objeto> _Objetos;

        [JsonProperty("centroDeMasa")]
        public Vector3DTO centroDeMasa { get; set; }

        public Escenario(Vector3DTO centroMasa)
        {
            _Objetos = new Dictionary<string, Objeto>();
            this.centroDeMasa = centroMasa;
        }

        public Escenario()
        {
            _Objetos = new Dictionary<string, Objeto>();
        }

        public Escenario(Dictionary<string, Objeto> Objetos)
        {
            _Objetos = Objetos;
        }

        public void AgregarObjeto(string nombre, Objeto objeto)
        {
            _Objetos[nombre] = objeto;
        }

        public Dictionary<string, Objeto> GetObjetos()
        {
            return _Objetos;
        }

        public void Dibujar()
        {
            foreach (Objeto objeto in _Objetos.Values)
            {
                objeto.Dibujar(centroDeMasa);
            }
        }

        public void RotarEscenario(float angulo, Vector3DTO eje)
        {

            foreach (Objeto objeto in _Objetos.Values)
            {
                objeto.RotarObjeto(angulo, eje);

            }

        }

        public void TrasladarEscenario(Vector3DTO desplazamiento)
        {
            // Trasladar cada objeto en el escenario
            foreach (Objeto objeto in _Objetos.Values)
            {
                objeto.TrasladarObjeto(desplazamiento);
            }

            // Actualizar el centro de masa del escenario
            centroDeMasa.X += desplazamiento.X;
            centroDeMasa.Y += desplazamiento.Y;
            centroDeMasa.Z += desplazamiento.Z;
        }


    }
}
