using System.Collections.Generic;
using OPTK_Transformaciones_3D.Estructura;
using OPTK_Transformaciones_3D.Estructura.DTO;

namespace OPTK_Transformaciones_3D.Figuras
{
    public class Ejes
    {
        public Objeto _objetoEjes { get; set; }

        public Ejes()
        {
            _objetoEjes = new Objeto();
            _objetoEjes.centroDeMasa = new Vector3DTO(0, 0, 0);
            CrearEjes();
        }

        private void CrearEjes()
        {
            // Crear puntos para los ejes
            List<Punto> puntosEjeX = new List<Punto>
    {
        new Punto(-5f, 0f, 0f), // Inicio eje X
        new Punto(5f, 0f, 0f)   // Fin eje X
    };

            List<Punto> puntosEjeY = new List<Punto>
    {
        new Punto(0f, -5f, 0f), // Inicio eje Y
        new Punto(0f, 5f, 0f)   // Fin eje Y
    };

            List<Punto> puntosEjeZ = new List<Punto>
    {
        new Punto(0f, 0f, -6f), // Inicio eje Z
        new Punto(0f, 0f, 6f)   // Fin eje Z
    };

            // Crear polígonos para representar los ejes
            Poligono ejeX = new Poligono(puntosEjeX, new Vector3DTO(1f, 0f, 0f)); // Rojo
            Poligono ejeY = new Poligono(puntosEjeY, new Vector3DTO(0f, 1f, 0f)); // Verde
            Poligono ejeZ = new Poligono(puntosEjeZ, new Vector3DTO(0f, 0f, 1f)); // Azul

            Parte parte = new Parte();
            parte.AgregarPoligono("ejex", ejeX);
            parte.AgregarPoligono("ejey", ejeY);
            parte.AgregarPoligono("ejez", ejeZ);

            // Agregar los ejes al objeto
            _objetoEjes.AgregarParte("Ejes", parte);
        }

    }
}
