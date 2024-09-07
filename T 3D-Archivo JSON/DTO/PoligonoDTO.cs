using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Collections.Generic;

namespace T_3D.DTO
{
    // DTO para Poligono
    public class PoligonoDTO
    {
        [JsonProperty("puntos")]
        public List<PuntoDTO> Puntos { get; set; }

        [JsonProperty("centroDeMasa")]
        public Vector3 CentroDeMasa { get; set; }

        public PoligonoDTO() { }

        public PoligonoDTO(Poligono poligono)
        {
            Puntos = new List<PuntoDTO>();
            foreach (var punto in poligono.Puntos)
            {
                Puntos.Add(new PuntoDTO(punto));
            }
            CentroDeMasa = poligono.CentroDeMasa;
        }

        public Poligono ToPoligono()
        {
            var puntos = new List<Punto>();
            foreach (var puntoDTO in Puntos)
            {
                puntos.Add(puntoDTO.ToPunto());
            }
            return new Poligono(puntos, CentroDeMasa);
        }
    }
}
