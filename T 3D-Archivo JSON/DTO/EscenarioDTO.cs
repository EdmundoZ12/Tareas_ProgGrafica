using Newtonsoft.Json;
using System.Collections.Generic;

namespace T_3D
{
    // DTO para Escenario
    public class EscenarioDTO
    {
        [JsonProperty("partes")]
        public Dictionary<string, ParteDTO> Partes { get; set; }

        public EscenarioDTO() { }

        public EscenarioDTO(Escenario escenario)
        {
            Partes = new Dictionary<string, ParteDTO>();
            foreach (var parte in escenario.GetPartes())
            {
                Partes.Add(parte.Key, new ParteDTO(parte.Value));
            }
        }

        public Escenario ToEscenario()
        {
            var escenario = new Escenario();
            foreach (var parteDTO in Partes)
            {
                escenario.AgregarParte(parteDTO.Key, parteDTO.Value.ToParte());
            }
            return escenario;
        }
    }
}
