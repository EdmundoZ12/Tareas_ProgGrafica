using Newtonsoft.Json;
using OpenTK.Mathematics;

namespace T_3D
{
    public class Vector3DTO
    {
        [JsonProperty("x")]
        public float X { get; set; }

        [JsonProperty("y")]
        public float Y { get; set; }

        [JsonProperty("z")]
        public float Z { get; set; }

        public Vector3DTO() { }

        public Vector3DTO(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Constructor que convierte desde Vector3
        public Vector3DTO(Vector3 vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        // Método para convertir de vuelta a Vector3
        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }
    }

}
