using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace T_3D
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var ventana = new Ventana())
            {
                Console.WriteLine("HOLA MUNDO!");
                ventana.setCoordenas(1f,0f,-3f);
                ventana.Run();
            }
        }
    }
}
