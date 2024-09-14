using EstructuraT3D.Estructura;
using EstructuraT3D.Figuras;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using T_3D;

namespace EstructuraT3D
{
    public class MainWindow : GameWindow
    {
        private Escenario _escenario;
        private LetraT letraT;
        private int _shaderProgram;
        private float _rotationAngle; // Ángulo de rotación
        private Vector3DTO _desplazamiento = new Vector3DTO(0, 0, 0);
        private float _velocidadTraslacion = 0.5f; // Velocidad de traslación


        public MainWindow() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.CenterWindow(new Vector2i(1280, 768));

        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color4.CornflowerBlue);
            GL.Enable(EnableCap.DepthTest);

            // Crear y compilar shaders
            string vertexShaderSource = @"
                #version 330 core
                layout(location = 0) in vec3 aPos;
                layout(location = 1) in vec4 aColor; // Color input
                out vec4 vertexColor; // Output color

                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 projection;

                void main()
                {
                    gl_Position = projection * view * model * vec4(aPos, 1.5);
                    vertexColor = aColor; // Pass color to fragment shader
                }
            ";

            string fragmentShaderSource = @"
                #version 330 core
                in vec4 vertexColor; // Color from vertex shader
                out vec4 FragColor;

                void main()
                {
                    FragColor = vertexColor;
                }
            ";

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);
            CheckShaderCompilation(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);
            CheckShaderCompilation(fragmentShader);

            _shaderProgram = GL.CreateProgram();
            GL.AttachShader(_shaderProgram, vertexShader);
            GL.AttachShader(_shaderProgram, fragmentShader);
            GL.LinkProgram(_shaderProgram);
            CheckProgramLinking(_shaderProgram);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            letraT = new LetraT();
            _escenario = letraT._escenario;
            _desplazamiento=_escenario.centroDeMasa;

            ////// Guardar el escenario en un archivo JSON manualmente
            //JsonSerializar.GuardarComoJson(_escenario, "prueba1");

            string rutaArchivo = Path.Combine(AppContext.BaseDirectory, "prueba1.json");
            _escenario = JsonSerializar.CargarDesdeJson<Escenario>(rutaArchivo);

            if (_escenario != null)
            {
                System.Console.WriteLine("Escenario cargado correctamente.");
            }
            else
            {
                System.Console.WriteLine("Error al cargar el escenario.");
            }
        }



        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(_shaderProgram);

            // Configurar matrices de transformación (ejemplo)
            Matrix4 model = Matrix4.CreateRotationY(_rotationAngle);
            Matrix4 view = Matrix4.LookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.UnitY);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), Size.X / (float)Size.Y, 0.1f, 100f);

            GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "model"), false, ref model);
            GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "view"), false, ref view);
            GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "projection"), false, ref projection);

            _escenario.Dibujar();

            //_rotationAngle += (float)e.Time;

            SwapBuffers();
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            float deltaTime = (float)e.Time; // Tiempo transcurrido desde el último frame

            // Manejar entrada de teclado para trasladar el escenario
            if (KeyboardState.IsKeyDown(Keys.W))
            {
                // Mueve el escenario hacia adelante en la dirección positiva del eje Z
                _desplazamiento.Z += deltaTime * _velocidadTraslacion;
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                // Mueve el escenario hacia atrás en la dirección negativa del eje Z
                _desplazamiento.Z -= deltaTime * _velocidadTraslacion;
            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                // Mueve el escenario hacia la izquierda en la dirección negativa del eje X
                _desplazamiento.X -= deltaTime * _velocidadTraslacion;
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                // Mueve el escenario hacia la derecha en la dirección positiva del eje X
                _desplazamiento.X += deltaTime * _velocidadTraslacion;
            }
            if (KeyboardState.IsKeyDown(Keys.Space))
            {
                // Mueve el escenario hacia arriba en la dirección positiva del eje Y
                _desplazamiento.Y += deltaTime * _velocidadTraslacion;
            }
            if (KeyboardState.IsKeyDown(Keys.LeftShift))
            {
                // Mueve el escenario hacia abajo en la dirección negativa del eje Y
                _desplazamiento.Y -= deltaTime * _velocidadTraslacion;
            }

            // Aplicar la traslación al escenario
            _escenario.TrasladarEscenario(_desplazamiento);
            //_desplazamiento = _escenario.centroDeMasa;


            // Resetear desplazamiento para el siguiente frame
            _desplazamiento = new Vector3DTO(0,0,0);

            // Manejar entrada de teclado para rotar el escenario
            if (KeyboardState.IsKeyDown(Keys.Left))
            {
                // Rota el escenario alrededor del eje Y en sentido antihorario
                _escenario.RotarEscenario(deltaTime * 1.0f, new Vector3DTO(0, 1, 0));
            }
            if (KeyboardState.IsKeyDown(Keys.Right))
            {
                // Rota el escenario alrededor del eje Y en sentido horario
                _escenario.RotarEscenario(deltaTime * -1.0f, new Vector3DTO(0, 1, 0));
            }
            if (KeyboardState.IsKeyDown(Keys.Up))
            {
                // Rota el escenario alrededor del eje X en sentido antihorario
                _escenario.RotarEscenario(deltaTime * 1.0f, new Vector3DTO(1, 0, 0));
            }
            if (KeyboardState.IsKeyDown(Keys.Down))
            {
                // Rota el escenario alrededor del eje X en sentido horario
                _escenario.RotarEscenario(deltaTime * -1.0f, new Vector3DTO(1, 0, 0));
            }

            // Opcional: Restablecer el desplazamiento para el siguiente frame si solo deseas mover una vez por tecla
            // _desplazamiento = Vector3DTO.Zero;
        }







        private void CheckShaderCompilation(int shader)
        {
            GL.GetShaderInfoLog(shader, out var log);
            if (log != string.Empty)
            {
                throw new System.Exception($"Shader compilation failed: {log}");
            }
        }

        private void CheckProgramLinking(int program)
        {
            GL.GetProgramInfoLog(program, out var log);
            if (log != string.Empty)
            {
                throw new System.Exception($"Program linking failed: {log}");
            }
        }
    }
}
