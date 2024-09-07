using Newtonsoft.Json;

namespace T_3D
{
    public class Punto
    {
        [JsonProperty("x")]
        public float X { get; set; }

        [JsonProperty("y")]
        public float Y { get; set; }

        [JsonProperty("z")]
        public float Z { get; set; }

        public Punto() { }

        public Punto(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
