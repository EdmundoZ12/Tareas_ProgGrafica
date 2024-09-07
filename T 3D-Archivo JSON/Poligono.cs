using Newtonsoft.Json;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace T_3D
{
    public class Poligono
    {
        [JsonIgnore]
        private int _vao;

        [JsonIgnore]
        private int _vbo;

        [JsonIgnore]
        private int _vertexCount;

        [JsonProperty("puntos")]
        public List<Punto> Puntos { get; set; }

        [JsonProperty("centroDeMasa")]
        public Vector3 CentroDeMasa { get; set; }

        // Constructor vacío requerido para la deserialización
        public Poligono() { }

        public Poligono(List<Punto> puntos, Vector3 centroDeMasa)
        {
            Puntos = puntos;
            CentroDeMasa = centroDeMasa;
            SetupBuffers();
        }

        private void SetupBuffers()
        {
            _vertexCount = Puntos.Count;

            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);

            _vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);

            float[] vertexData = new float[_vertexCount * 3];
            for (int i = 0; i < _vertexCount; i++)
            {
                vertexData[i * 3] = Puntos[i].X - CentroDeMasa.X;
                vertexData[i * 3 + 1] = Puntos[i].Y - CentroDeMasa.Y;
                vertexData[i * 3 + 2] = Puntos[i].Z - CentroDeMasa.Z;
            }
            GL.BufferData(BufferTarget.ArrayBuffer, vertexData.Length * sizeof(float), vertexData, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindVertexArray(0);
        }

        public void UpdateBuffers()
        {
            SetupBuffers();
        }

        public void Dibujar()
        {
            GL.BindVertexArray(_vao);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, _vertexCount);
            GL.BindVertexArray(0);
        }
    }
}
