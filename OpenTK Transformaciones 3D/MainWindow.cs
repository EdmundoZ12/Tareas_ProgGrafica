using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OPTK_Transformaciones_3D.Estructura;
using OPTK_Transformaciones_3D.Estructura.DTO;
using OPTK_Transformaciones_3D.Figuras;

namespace OPTK_Transformaciones_3D
{
    public class MainWindow : GameWindow
    {
        private Escenario _escenario;
        private Ejes ejes;
        private LetraT letraT;
        private int _shaderProgram;
        private float _velocidadTraslacion = 0.5f; // Velocidad de traslación
        private Vector3DTO _desplazamiento = new Vector3DTO(0, 0, 0);

        public MainWindow() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            CenterWindow(new Vector2i(1280, 768));
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);

            // Crear y compilar shaders
            _shaderProgram = CrearShaders();

            letraT = new LetraT();
            ejes = new Ejes();
            _escenario = letraT._escenario;

            // Guardar el escenario en un archivo JSON manualmente
            //JsonSerializar.GuardarComoJson(_escenario, "prueba1");

            // Cargar el escenario desde el archivo JSON
             CargarEscenario();
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

            // Configurar matrices de transformación
            ConfigurarMatrices();

            // Dibujar la letra T y ejes
            _escenario.Dibujar();
            ejes._objetoEjes.DibujarLineas();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            float deltaTime = (float)e.Time; // Tiempo transcurrido desde el último frame

            // Rotación continua de la parte horizontal de LetraT1
            _escenario._Objetos["LetraT1"]._partes["parteHorizontal"].RotarParte(deltaTime * -1.0f, new Vector3DTO(1, 0, 0));

            // Manejar entrada de teclado para trasladar el escenario
            ManejarTraslacion(deltaTime);
            // Aplicar la traslación al escenario
            _escenario.TrasladarEscenario(_desplazamiento);
            _desplazamiento = new Vector3DTO(0, 0, 0); // Resetear desplazamiento

            // Manejar rotación del escenario
            ManejarRotacion(deltaTime);
        }

        private int CrearShaders()
        {
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
                    gl_Position = projection * view * model * vec4(aPos, 2.5);
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

            int shaderProgram = GL.CreateProgram();
            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);
            CheckProgramLinking(shaderProgram);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            return shaderProgram;
        }

        private void ConfigurarMatrices()
        {
            Matrix4 model = Matrix4.CreateRotationY(0); // Ángulo de rotación
            Matrix4 view = Matrix4.LookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.UnitY);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), Size.X / (float)Size.Y, 0.1f, 100f);

            GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "model"), false, ref model);
            GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "view"), false, ref view);
            GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "projection"), false, ref projection);
        }

        private void ManejarTraslacion(float deltaTime)
        {
            if (KeyboardState.IsKeyDown(Keys.W))
                _desplazamiento.Z += deltaTime * _velocidadTraslacion;

            if (KeyboardState.IsKeyDown(Keys.S))
                _desplazamiento.Z -= deltaTime * _velocidadTraslacion;

            if (KeyboardState.IsKeyDown(Keys.A))
                _desplazamiento.X -= deltaTime * _velocidadTraslacion;

            if (KeyboardState.IsKeyDown(Keys.D))
                _desplazamiento.X += deltaTime * _velocidadTraslacion;

            if (KeyboardState.IsKeyDown(Keys.Space))
                _desplazamiento.Y += deltaTime * _velocidadTraslacion;

            if (KeyboardState.IsKeyDown(Keys.LeftShift))
                _desplazamiento.Y -= deltaTime * _velocidadTraslacion;
        }

        private void ManejarRotacion(float deltaTime)
        {
            if (KeyboardState.IsKeyDown(Keys.Left))
                _escenario.RotarEscenario(deltaTime * 1.5f, new Vector3DTO(0, 1, 0));

            if (KeyboardState.IsKeyDown(Keys.Right))
                _escenario.RotarEscenario(deltaTime * -1.0f, new Vector3DTO(0, 1, 0));

            if (KeyboardState.IsKeyDown(Keys.Up))
            {
                _escenario._Objetos["LetraT1"]._partes["parteHorizontal"].RotarParte(deltaTime * -1.0f, new Vector3DTO(1, 0, 0));
                _escenario._Objetos["LetraT2"]._partes["parteHorizontal"].RotarParte(deltaTime * -1.0f, new Vector3DTO(1, 0, 0));
            }

            if (KeyboardState.IsKeyDown(Keys.Down))
                _escenario._Objetos["LetraT1"].RotarObjeto(deltaTime * -1.0f, new Vector3DTO(1, 0, 0));

            // Manejo de escalado
            if (KeyboardState.IsKeyDown(Keys.E))
                _escenario.EscalarEscenario(new Vector3DTO(1.01f, 1.01f, 1.01f)); // Aumenta en un 1%

            if (KeyboardState.IsKeyDown(Keys.Q))
                _escenario.EscalarEscenario(new Vector3DTO(0.99f, 0.99f, 0.99f)); // Disminuye en un 1%
        }

        private void CargarEscenario()
        {
            string rutaArchivo = Path.Combine(AppContext.BaseDirectory, "prueba1.json");
            _escenario = JsonSerializar.CargarDesdeJson<Escenario>(rutaArchivo);

            if (_escenario != null)
            {
                Console.WriteLine("Escenario cargado correctamente.");
            }
            else
            {
                Console.WriteLine("Error al cargar el escenario.");
            }
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
