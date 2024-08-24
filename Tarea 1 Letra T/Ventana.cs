using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;

namespace Introduccion
{
    public class Ventana : GameWindow
    {
        private int vertexBufferHandle;
        private int indexBufferHandle;
        private int shaderProgramHandle;
        private int vertexArrayHandle;
        private Matrix4 model;
        private Matrix4 view;
        private Matrix4 projection;
        private float rotationAngle = 0f;

        public Ventana() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.CenterWindow(new Vector2i(1280, 768));
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), (float)e.Width / e.Height, 0.1f, 100f);
            base.OnResize(e);
        }

        protected override void OnLoad()
        {
            GL.ClearColor(Color4.Black);

            float[] vertices = new float[]
            {
                // Parte superior de la "T" (front face)
                -0.5f,  0.5f,  0.1f,  // Vértice 0
                 0.5f,  0.5f,  0.1f,  // Vértice 1
                 0.5f,  0.3f,  0.1f,  // Vértice 2
                -0.5f,  0.3f,  0.1f,  // Vértice 3

                // Parte vertical de la "T" (front face)
                -0.1f,  0.3f,  0.1f,  // Vértice 4
                 0.1f,  0.3f,  0.1f,  // Vértice 5
                 0.1f, -0.5f,  0.1f,  // Vértice 6
                -0.1f, -0.5f,  0.1f,  // Vértice 7

                // Parte superior de la "T" (back face)
                -0.5f,  0.5f, -0.1f,  // Vértice 8
                 0.5f,  0.5f, -0.1f,  // Vértice 9
                 0.5f,  0.3f, -0.1f,  // Vértice 10
                -0.5f,  0.3f, -0.1f,  // Vértice 11

                // Parte vertical de la "T" (back face)
                -0.1f,  0.3f, -0.1f,  // Vértice 12
                 0.1f,  0.3f, -0.1f,  // Vértice 13
                 0.1f, -0.5f, -0.1f,  // Vértice 14
                -0.1f, -0.5f, -0.1f   // Vértice 15
            };

            int[] indices = new int[]
            {
                // Front face
                0, 1, 2,
                2, 3, 0,
                4, 5, 6,
                6, 7, 4,

                // Back face
                8, 9, 10,
                10, 11, 8,
                12, 13, 14,
                14, 15, 12,

                // Top face
                0, 1, 9,
                9, 8, 0,
                // Bottom face (base of the vertical part)
                6, 7, 15,
                15, 14, 6,

                // Left face (left side of the "T")
                0, 3, 11,
                11, 8, 0,
                3, 2, 10,
                10, 11, 3,

                // Right face (right side of the "T")
                1, 5, 13,
                13, 9, 1,
                2, 5, 13,
                13, 10, 2,

                // Connecting the front and back of the vertical part
                4, 7, 15,
                15, 12, 4,
                6, 5, 13,
                13, 14, 6,
                        };


            this.vertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(this.vertexArrayHandle);

            this.vertexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            this.indexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.indexBufferHandle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            string vertexShaderCode =
                @"
                #version 330 core

                layout (location = 0) in vec3 aPosition;
                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 projection;

                void main()
                {
                    gl_Position = projection * view * model * vec4(aPosition, 1.0);
                }
                ";

            string fragmentShaderCode =
                @"
                #version 330 core

                out vec4 pixelColor;

                void main()
                {
                    pixelColor = vec4(0.8f, 0.4f, 0.1f, 1.0f);
                }
                ";

            int vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderHandle, vertexShaderCode);
            GL.CompileShader(vertexShaderHandle);

            int fragmentShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderHandle, fragmentShaderCode);
            GL.CompileShader(fragmentShaderHandle);

            this.shaderProgramHandle = GL.CreateProgram();

            GL.AttachShader(this.shaderProgramHandle, vertexShaderHandle);
            GL.AttachShader(this.shaderProgramHandle, fragmentShaderHandle);

            GL.LinkProgram(this.shaderProgramHandle);

            GL.DetachShader(this.shaderProgramHandle, vertexShaderHandle);
            GL.DetachShader(this.shaderProgramHandle, fragmentShaderHandle);

            GL.DeleteShader(vertexShaderHandle);
            GL.DeleteShader(fragmentShaderHandle);

            GL.UseProgram(this.shaderProgramHandle);

            // Initialize model, view, and projection matrices
            model = Matrix4.Identity;
            view = Matrix4.CreateTranslation(0f, 0f, -3f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), 1280f / 768f, 0.1f, 100f);

            base.OnLoad();
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(this.vertexBufferHandle);
            GL.DeleteBuffer(this.indexBufferHandle);

            GL.UseProgram(0);
            GL.DeleteProgram(this.shaderProgramHandle);

            base.OnUnload();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            float rotationSpeed = 100f; // Incrementa este valor para rotar más rápido
            rotationAngle += rotationSpeed * (float)args.Time;
            model = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotationAngle));
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(this.shaderProgramHandle);

            int modelLocation = GL.GetUniformLocation(this.shaderProgramHandle, "model");
            GL.UniformMatrix4(modelLocation, false, ref model);

            int viewLocation = GL.GetUniformLocation(this.shaderProgramHandle, "view");
            GL.UniformMatrix4(viewLocation, false, ref view);

            int projectionLocation = GL.GetUniformLocation(this.shaderProgramHandle, "projection");
            GL.UniformMatrix4(projectionLocation, false, ref projection);

            GL.BindVertexArray(this.vertexArrayHandle);
            GL.DrawElements(PrimitiveType.Triangles, 72, DrawElementsType.UnsignedInt, 0);

            this.Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
