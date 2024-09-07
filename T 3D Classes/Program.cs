using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace T_3D
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var ventana = new Ventana(0.6f, 0.5f, -3f))
            {
                Console.WriteLine("HOLA MUNDO!");
                ventana.Run();
            }
        }
    }
}
