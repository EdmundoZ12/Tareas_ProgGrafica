
namespace T_3D
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var window = new MainWindow())
            {
                window.Run(60.0);
            }
        }
    }
}
