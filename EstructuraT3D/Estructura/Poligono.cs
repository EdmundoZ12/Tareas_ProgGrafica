using Newtonsoft.Json;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Collections.Generic;
using T_3D;

namespace EstructuraT3D.Estructura
{
    public class Poligono
    {
        [JsonIgnore]
        private int _vao;

        [JsonIgnore]
        private int _vbo;

        [JsonIgnore]
        private int _colorVbo;

        [JsonIgnore]
        private int _vertexCount;

        [JsonProperty("puntos")]
        public List<Punto> Puntos { get; set; }

        [JsonProperty("color")]
        public Vector3DTO Color { get; set; }  // Color del polígono

        [JsonIgnore]
        public Vector3DTO centroDeMasa { get; set; } // Usar Vector3DTO para serialización

        // Constructor con color predeterminado rojo
        public Poligono(List<Punto> puntos, Vector3DTO? color = null)
        {
            this.Puntos = puntos;
            this.centroDeMasa = new Vector3DTO(0, 0, 0);
            this.Color = color ?? new Vector3DTO(1.0f, 0.0f, 0.0f); // Color rojo por defecto
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
                vertexData[i * 3] = Puntos[i].X + centroDeMasa.X;
                vertexData[i * 3 + 1] = Puntos[i].Y + centroDeMasa.Y;
                vertexData[i * 3 + 2] = Puntos[i].Z + centroDeMasa.Z;
            }
            GL.BufferData(BufferTarget.ArrayBuffer, vertexData.Length * sizeof(float), vertexData, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Buffer de colores
            _colorVbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _colorVbo);

            float[] colorData = new float[_vertexCount * 3];
            for (int i = 0; i < _vertexCount; i++)
            {
                colorData[i * 3] = (float)Color.X;
                colorData[i * 3 + 1] = (float)Color.Y;
                colorData[i * 3 + 2] = (float)Color.Z;
            }
            GL.BufferData(BufferTarget.ArrayBuffer, colorData.Length * sizeof(float), colorData, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);

            GL.BindVertexArray(0);
        }

        public void UpdateBuffers()
        {
            SetupBuffers();
        }

        public void Dibujar(Vector3DTO centroMasa)
        {
            this.centroDeMasa = centroMasa;
            SetupBuffers();
            GL.BindVertexArray(_vao);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, _vertexCount);
            GL.BindVertexArray(0);
        }

        public void Rotar(float angulo, Vector3DTO eje)
        {
            Matrix4 rotacion = Matrix4.CreateFromAxisAngle(new Vector3(eje.X, eje.Y, eje.Z), angulo);

            foreach (Punto punto in Puntos)
            {
                Vector4 p = new Vector4(punto.X, punto.Y, punto.Z, 1.0f);
                p = rotacion * p;
                punto.X = p.X;
                punto.Y = p.Y;
                punto.Z = p.Z;
            }

            UpdateBuffers();
        }

        public void Trasladar(Vector3DTO desplazamiento)
        {
            foreach (Punto punto in Puntos)
            {
                punto.X += desplazamiento.X;
                punto.Y += desplazamiento.Y;
                punto.Z += desplazamiento.Z;
            }

            centroDeMasa.X += desplazamiento.X;
            centroDeMasa.Y += desplazamiento.Y;
            centroDeMasa.Z += desplazamiento.Z;

            UpdateBuffers();
        }

    }
}
