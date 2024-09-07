using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace T_3D
    //CREAR LOS METODOS DIBUJAR Y TRASLADAR OSEA UNO PARA DIBUJAR Y OTRO PARA MOVER LOS PUNTOS
{
    public class Objeto
    {
        private int vertexBufferHandle;
        private int indexBufferHandle;
        private int vertexArrayHandle;
        private float[] vertices;
        private int[] indices;

        public Matrix4 model;

        public Objeto()
        {
            // Define los vértices y los índices de la "T"
            vertices = new float[]
            {
                // Parte superior de la "T" (front face)
                -0.5f,  0.5f,  0.1f,
                 0.5f,  0.5f,  0.1f,
                 0.5f,  0.3f,  0.1f,
                -0.5f,  0.3f,  0.1f,  

                // Parte vertical de la "T" (front face)
                -0.1f,  0.3f,  0.1f,
                 0.1f,  0.3f,  0.1f,
                 0.1f, -0.5f,  0.1f,
                -0.1f, -0.5f,  0.1f,  

                // Parte superior de la "T" (back face)
                -0.5f,  0.5f, -0.1f,
                 0.5f,  0.5f, -0.1f,
                 0.5f,  0.3f, -0.1f,
                -0.5f,  0.3f, -0.1f,  

                // Parte vertical de la "T" (back face)
                -0.1f,  0.3f, -0.1f,
                 0.1f,  0.3f, -0.1f,
                 0.1f, -0.5f, -0.1f,
                -0.1f, -0.5f, -0.1f
            };

            indices = new int[]
            {
                //cara frontal
                0, 1, 2,
                2, 3, 0,
                4, 5, 6,
                6, 7, 4,

                //cara trasera
                8, 9, 10,
                10, 11, 8,
                12, 13, 14,
                14, 15, 12,

                //cara alta
                0, 1, 9,
                9, 8, 0,
                // Bottom face (base of the vertical part)
                //parte de la cara media de la parte vertical
                6, 7, 15,
                15, 14, 6,

                //cara izquiera del lado izquierdo de la T
                0, 3, 11,
                11, 8, 0,
                3, 2, 10,
                10, 11, 3,

                //cara derecha del lado derecho de la T
                1, 5, 13,
                13, 9, 1,
                2, 5, 13,
                13, 10, 2,

                //conectar parte frontal y trasera de la parte vertical
                4, 7, 15,
                15, 12, 4,
                6, 5, 13,
                13, 14, 6
            };

            // Crear y configurar buffers
            vertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayHandle);

            vertexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            indexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBufferHandle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

        }

        public void Draw(int shaderProgramHandle)
        {
            GL.BindVertexArray(vertexArrayHandle);

            // Obtener la ubicación del uniforme 'model' y enviar la matriz
            int modelLocation = GL.GetUniformLocation(shaderProgramHandle, "model");
            GL.UniformMatrix4(modelLocation, false, ref model);


            // Dibujar los elementos
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }


        public void Dispose()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(vertexBufferHandle);
            GL.DeleteBuffer(indexBufferHandle);
            GL.DeleteVertexArray(vertexArrayHandle);
        }

        public void SetModelMatrix(Matrix4 newModel)
        {
            model = newModel;
        }

    }
}
