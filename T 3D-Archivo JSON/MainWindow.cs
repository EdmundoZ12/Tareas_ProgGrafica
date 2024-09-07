using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace T_3D
{
    public class MainWindow : GameWindow
    {
        private Escenario _escenario;
        private int _shaderProgram;
        private float _rotationAngle;

        public MainWindow() : base(1100, 768, GraphicsMode.Default, "Dibujar Letra T en 3D", GameWindowFlags.Default, DisplayDevice.Default, 3, 3, GraphicsContextFlags.ForwardCompatible)
        {
            Load += OnLoad;
            RenderFrame += OnRenderFrame;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            GL.ClearColor(Color4.CornflowerBlue);
            GL.Enable(EnableCap.DepthTest);

            // Crear y compilar shaders
            string vertexShaderSource = @"
                #version 330 core
                layout(location = 0) in vec3 aPos;
                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 projection;

                void main()
                {
                    gl_Position = projection * view * model * vec4(aPos, 1.0);
                }
            ";

            string fragmentShaderSource = @"
                #version 330 core
                out vec4 FragColor;

                void main()
                {
                    FragColor = vec4(1.0, 0.0, 0.0, 1.0); // Color rojo
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

            //// Crear parte vertical de la letra T
            //Vector3 centroDeMasa = new Vector3(0.5f, 0, 0); // Definir el centro de masa para la parte vertical
            //List<Punto> puntosVerticalFrontal = new List<Punto>
            //{
            //    new Punto(-0.5f, 1.0f, 0.5f),
            //    new Punto(0.5f, 1.0f, 0.5f),
            //    new Punto(0.5f, -1.0f, 0.5f),
            //    new Punto(-0.5f, -1.0f, 0.5f)
            //};

            //List<Punto> puntosVerticalTrasero = new List<Punto>
            //{
            //    new Punto(-0.5f, 1.0f, -0.5f),
            //    new Punto(0.5f, 1.0f, -0.5f),
            //    new Punto(0.5f, -1.0f, -0.5f),
            //    new Punto(-0.5f, -1.0f, -0.5f)
            //};

            //Poligono frontalVertical = new Poligono(puntosVerticalFrontal, centroDeMasa);
            //Poligono traseroVertical = new Poligono(puntosVerticalTrasero, centroDeMasa);
            //Poligono izquierdaVertical = new Poligono(new List<Punto> { puntosVerticalFrontal[0], puntosVerticalTrasero[0], puntosVerticalTrasero[3], puntosVerticalFrontal[3] }, centroDeMasa);
            //Poligono derechaVertical = new Poligono(new List<Punto> { puntosVerticalFrontal[1], puntosVerticalTrasero[1], puntosVerticalTrasero[2], puntosVerticalFrontal[2] }, centroDeMasa);
            //Poligono arribaVertical = new Poligono(new List<Punto> { puntosVerticalFrontal[0], puntosVerticalFrontal[1], puntosVerticalTrasero[1], puntosVerticalTrasero[0] }, centroDeMasa);
            //Poligono abajoVertical = new Poligono(new List<Punto> { puntosVerticalFrontal[3], puntosVerticalFrontal[2], puntosVerticalTrasero[2], puntosVerticalTrasero[3] }, centroDeMasa);

            //Parte parteVertical = new Parte();
            //parteVertical.AgregarPoligono("frontal", frontalVertical);
            //parteVertical.AgregarPoligono("trasero", traseroVertical);
            //parteVertical.AgregarPoligono("izquierda", izquierdaVertical);
            //parteVertical.AgregarPoligono("derecha", derechaVertical);
            //parteVertical.AgregarPoligono("arriba", arribaVertical);
            //parteVertical.AgregarPoligono("abajo", abajoVertical);

            //_escenario = new Escenario();
            //_escenario.AgregarParte("parteVertical", parteVertical);

            //// Crear parte horizontal de la letra T
            //List<Punto> puntosHorizontalFrontal = new List<Punto>
            //{
            //    new Punto(-1.0f, 1.5f, 0.5f),
            //    new Punto(1.0f, 1.5f, 0.5f),
            //    new Punto(1.0f, 1.0f, 0.5f),
            //    new Punto(-1.0f, 1.0f, 0.5f)
            //};

            //List<Punto> puntosHorizontalTrasero = new List<Punto>
            //{
            //    new Punto(-1.0f, 1.5f, -0.5f),
            //    new Punto(1.0f, 1.5f, -0.5f),
            //    new Punto(1.0f, 1.0f, -0.5f),
            //    new Punto(-1.0f, 1.0f, -0.5f)
            //};

            //Poligono frontalHorizontal = new Poligono(puntosHorizontalFrontal, centroDeMasa);
            //Poligono traseroHorizontal = new Poligono(puntosHorizontalTrasero, centroDeMasa);
            //Poligono izquierdaHorizontal = new Poligono(new List<Punto> { puntosHorizontalFrontal[0], puntosHorizontalTrasero[0], puntosHorizontalTrasero[3], puntosHorizontalFrontal[3] }, centroDeMasa);
            //Poligono derechaHorizontal = new Poligono(new List<Punto> { puntosHorizontalFrontal[1], puntosHorizontalTrasero[1], puntosHorizontalTrasero[2], puntosHorizontalFrontal[2] }, centroDeMasa);
            //Poligono arribaHorizontal = new Poligono(new List<Punto> { puntosHorizontalFrontal[0], puntosHorizontalFrontal[1], puntosHorizontalTrasero[1], puntosHorizontalTrasero[0] }, centroDeMasa);
            //Poligono abajoHorizontal = new Poligono(new List<Punto> { puntosHorizontalFrontal[3], puntosHorizontalFrontal[2], puntosHorizontalTrasero[2], puntosHorizontalTrasero[3] }, centroDeMasa);

            //Parte parteHorizontal = new Parte();
            //parteHorizontal.AgregarPoligono("frontal", frontalHorizontal);
            //parteHorizontal.AgregarPoligono("trasero", traseroHorizontal);
            //parteHorizontal.AgregarPoligono("izquierda", izquierdaHorizontal);
            //parteHorizontal.AgregarPoligono("derecha", derechaHorizontal);
            //parteHorizontal.AgregarPoligono("arriba", arribaHorizontal);
            //parteHorizontal.AgregarPoligono("abajo", abajoHorizontal);

            //_escenario.AgregarParte("parteHorizontal", parteHorizontal);
            //// Guardar el escenario en un archivo JSON manualmente


            //// Guardar el escenario en un archivo JSON manualmente
            //Serializador.GuardarEscenarioManual( _escenario);
            // Cargar el escenario desde el archivo JSON
            _escenario = Serializador.CargarEscenarioDesdeArchivo("D:\\E.J.Z.R\\Universidad\\8.-Octavo Semestre\\Programacion Grafica\\C#\\T 3D\\data\\escenario.json");
            Console.WriteLine(_escenario.GetPartes().Count);
            // Asegúrate de que el escenario se cargue correctamente
            if (_escenario == null)
            {
                throw new Exception("No se pudo cargar el escenario desde el archivo.");
            }
        }

        private void OnRenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(_shaderProgram);

            // Configurar matrices de transformación (ejemplo)
            Matrix4 model = Matrix4.CreateRotationY(_rotationAngle);
            Matrix4 view = Matrix4.LookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.UnitY);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), Width / (float)Height, 0.1f, 100f);

            GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "model"), false, ref model);
            GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "view"), false, ref view);
            GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "projection"), false, ref projection);

            _escenario.Dibujar();

            _rotationAngle += (float)e.Time;

            SwapBuffers();
        }

        private void CheckShaderCompilation(int shader)
        {
            GL.GetShaderInfoLog(shader, out var log);
            if (log != string.Empty)
            {
                throw new Exception($"Shader compilation failed: {log}");
            }
        }

        private void CheckProgramLinking(int program)
        {
            GL.GetProgramInfoLog(program, out var log);
            if (log != string.Empty)
            {
                throw new Exception($"Program linking failed: {log}");
            }
        }
    }
}
