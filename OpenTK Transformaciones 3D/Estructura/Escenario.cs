using Newtonsoft.Json;
using OPTK_Transformaciones_3D.Estructura.DTO;
using System.Collections.Generic;

namespace OPTK_Transformaciones_3D.Estructura
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
            centroDeMasa = centroMasa;
        }

        public Escenario()
        {
            _Objetos = new Dictionary<string, Objeto>();
        }

        public Escenario(Dictionary<string, Objeto> objetos)
        {
            _Objetos = objetos;
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
                objeto.centroDeMasa = centroDeMasa;
                objeto.Dibujar();
            }
        }

        public void DibujarLineas()
        {
            foreach (Objeto objeto in _Objetos.Values)
            {
                objeto.centroDeMasa = centroDeMasa;
                objeto.DibujarLineas();
            }
        }

        public void RotarEscenario(float angulo, Vector3DTO eje)
        {
            foreach (Objeto objeto in _Objetos.Values)
            {
                objeto.RotarObjeto(angulo, eje, true);
            }
        }

        public void TrasladarEscenario(Vector3DTO desplazamiento)
        {
            // Trasladar cada objeto en el escenario
            foreach (Objeto objeto in _Objetos.Values)
            {
                objeto.TrasladarObjeto(desplazamiento);
            }

            ActualizarCentroDeMasa(desplazamiento);
        }

        public void EscalarEscenario(Vector3DTO factorEscala)
        {
            foreach (Objeto objeto in _Objetos.Values)
            {
                objeto.EscalarObjeto(factorEscala);
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
