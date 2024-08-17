namespace Introduccion
{
    public class Programa
    {
        public static void Main(string[] args)
        {
            using (Ventana ventana = new Ventana())
            {
                ventana.Run();
            }
        }

    }
}
