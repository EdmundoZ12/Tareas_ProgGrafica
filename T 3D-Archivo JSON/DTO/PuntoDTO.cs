using Newtonsoft.Json;



namespace T_3D.DTO
{
    // DTO para Punto
    public class PuntoDTO
    {
        [JsonProperty("x")]
        public float X { get; set; }

        [JsonProperty("y")]
        public float Y { get; set; }

        [JsonProperty("z")]
        public float Z { get; set; }

        public PuntoDTO() { }

        public PuntoDTO(Punto punto)
        {
            X = punto.X;
            Y = punto.Y;
            Z = punto.Z;
        }

        public Punto ToPunto()
        {
            return new Punto(X, Y, Z);
        }
    }
}
