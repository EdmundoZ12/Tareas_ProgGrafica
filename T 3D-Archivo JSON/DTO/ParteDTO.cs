using Newtonsoft.Json;
using System.Collections.Generic;
using T_3D.DTO;

namespace T_3D
{
    // DTO para Parte
    public class ParteDTO
    {
        [JsonProperty("poligonos")]
        public Dictionary<string, PoligonoDTO> Poligonos { get; set; }

        public ParteDTO() { }

        public ParteDTO(Parte parte)
        {
            Poligonos = new Dictionary<string, PoligonoDTO>();
            foreach (var poligono in parte.GetPoligonos())
            {
                Poligonos.Add(poligono.Key, new PoligonoDTO(poligono.Value));
            }
        }

        public Parte ToParte()
        {
            var parte = new Parte();
            foreach (var poligonoDTO in Poligonos)
            {
                parte.AgregarPoligono(poligonoDTO.Key, poligonoDTO.Value.ToPoligono());
            }
            return parte;
        }
    }
}
