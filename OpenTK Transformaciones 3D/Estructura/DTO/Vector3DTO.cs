using Newtonsoft.Json;
using OpenTK.Mathematics;

namespace OPTK_Transformaciones_3D.Estructura.DTO
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

        public static Vector3DTO Add(Vector3DTO a, Vector3DTO b)
        {
            return new Vector3DTO(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        // Método para convertir de vuelta a Vector3
        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }
    }

}
