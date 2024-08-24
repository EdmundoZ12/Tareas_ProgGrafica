using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;

namespace T_3D
{
    public class Ventana : GameWindow
    {
        private int shaderProgramHandle;
        private Objeto objeto3D;
        private Matrix4 model;
        private Matrix4 view;
        private Matrix4 projection;
        private float rotationAngle = 0f;
        float X;
        float Y;
        float Z;

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

        public void setCoordenas(float x,float y,float z) {
            this.X = x; this.Y = y; this.Z = z;
        }

        protected override void OnLoad()
        {
            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);

            objeto3D = new Objeto();

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

            shaderProgramHandle = GL.CreateProgram();
            GL.AttachShader(shaderProgramHandle, vertexShaderHandle);
            GL.AttachShader(shaderProgramHandle, fragmentShaderHandle);
            GL.LinkProgram(shaderProgramHandle);
            GL.DetachShader(shaderProgramHandle, vertexShaderHandle);
            GL.DetachShader(shaderProgramHandle, fragmentShaderHandle);
            GL.DeleteShader(vertexShaderHandle);
            GL.DeleteShader(fragmentShaderHandle);

            GL.UseProgram(shaderProgramHandle);

            view = Matrix4.CreateTranslation(this.X, this.Y, this.Z);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), 1280f / 768f, 0.1f, 100f);

            base.OnLoad();
        }


        protected override void OnUnload()
        {
            GL.DeleteProgram(shaderProgramHandle);
            objeto3D.Dispose();
            base.OnUnload();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            float rotationSpeed = 100f; // Ajusta la velocidad si es necesario
            rotationAngle += rotationSpeed * (float)args.Time;
            var rotationMatrix = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotationAngle));

            // Actualiza la matriz de transformación en el objeto
            objeto3D.SetModelMatrix(rotationMatrix);

            base.OnUpdateFrame(args);
        }



        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(shaderProgramHandle);

            // Obtener y enviar las matrices de transformación
            int modelLocation = GL.GetUniformLocation(shaderProgramHandle, "model");
            GL.UniformMatrix4(modelLocation, false, ref model);

            int viewLocation = GL.GetUniformLocation(shaderProgramHandle, "view");
            GL.UniformMatrix4(viewLocation, false, ref view);

            int projectionLocation = GL.GetUniformLocation(shaderProgramHandle, "projection");
            GL.UniformMatrix4(projectionLocation, false, ref projection);

            // Dibujar el objeto
            objeto3D.Draw(shaderProgramHandle);

            this.Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
