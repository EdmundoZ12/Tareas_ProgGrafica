using Newtonsoft.Json;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OPTK_Transformaciones_3D.Estructura.DTO;
using System.Collections.Generic;

namespace OPTK_Transformaciones_3D.Estructura
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
        public Vector3DTO centroDeMasa { get; set; } // Centro de masa del polígono

        // Constructor con color predeterminado rojo
        public Poligono(List<Punto> puntos, Vector3DTO? color = null)
        {
            Puntos = puntos;
            centroDeMasa = new Vector3DTO(0, 0, 0);
            Color = color ?? new Vector3DTO(1.0f, 0.0f, 0.0f); // Color rojo por defecto
        }

        private void SetupBuffers()
        {
            _vertexCount = Puntos.Count;

            // Generar y vincular el VAO
            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);

            // Configurar el VBO para los vértices
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

            // Configurar el VBO para los colores
            _colorVbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _colorVbo);

            float[] colorData = new float[_vertexCount * 3];
            for (int i = 0; i < _vertexCount; i++)
            {
                colorData[i * 3] = Color.X;
                colorData[i * 3 + 1] = Color.Y;
                colorData[i * 3 + 2] = Color.Z;
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

        public void Dibujar()
        {
            SetupBuffers();
            GL.BindVertexArray(_vao);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, _vertexCount);
            GL.BindVertexArray(0);
        }

        public void DibujarLineas()
        {
            SetupBuffers();
            GL.BindVertexArray(_vao);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, _vertexCount);
            GL.BindVertexArray(0);
        }

        public void Rotar(float angulo, Vector3DTO eje)
        {
            Matrix4 rotacion = eje switch
            {
                { X: 1 } => Matrix4.CreateRotationX(angulo),
                { Y: 1 } => Matrix4.CreateRotationY(angulo),
                { Z: 1 } => Matrix4.CreateRotationZ(angulo),
                _ => Matrix4.Identity
            };

            if (rotacion == Matrix4.Identity) return; // No hay rotación si el eje no es válido

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

        public void EscalarPoligono(Vector3DTO factorEscala)
        {
            Matrix4 escala = Matrix4.CreateScale(factorEscala.X, factorEscala.Y, factorEscala.Z);

            foreach (Punto punto in Puntos)
            {
                Vector4 p = new Vector4(punto.X, punto.Y, punto.Z, 1.0f);
                p = escala * p; // Aplica la transformación de escala
                punto.X = p.X;
                punto.Y = p.Y;
                punto.Z = p.Z;
            }

            UpdateBuffers(); // Actualiza los buffers para reflejar los cambios
        }

        public void ActualizarCentroDeMasa(Vector3DTO desplazamiento)
        {
            centroDeMasa.X += desplazamiento.X;
            centroDeMasa.Y += desplazamiento.Y;
            centroDeMasa.Z += desplazamiento.Z;
        }

        public void Trasladar(Vector3DTO desplazamiento)
        {
            foreach (Punto punto in Puntos)
            {
                punto.X += desplazamiento.X;
                punto.Y += desplazamiento.Y;
                punto.Z += desplazamiento.Z;
            }

            ActualizarCentroDeMasa(desplazamiento); // Llama al método para actualizar el centro de masa

            UpdateBuffers();
        }

    }
}
