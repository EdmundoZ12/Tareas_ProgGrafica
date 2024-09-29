﻿using OPTK_Transformaciones_3D.Estructura;
using OPTK_Transformaciones_3D.Estructura.DTO;

namespace OPTK_Transformaciones_3D.Figuras
{
    public class LetraT
    {
        public Escenario _escenario { get; set; }

        public LetraT()
        {
            _escenario = new Escenario(new Vector3DTO(1f, 0f, 0f));

            // Crear dos letras "T" en diferentes posiciones
            CrearLetraT(new Vector3DTO(0f, 0f, 0f), "LetraT1");
            CrearLetraT(new Vector3DTO(3f, 0f, 0f), "LetraT2");
            CrearLetraT(new Vector3DTO(3f, -2f, 0f), "LetraT3");
            crearCubo(new Vector3DTO(-3f, 0f, 0f), "Cubo");
        }

        private void CrearLetraT(Vector3DTO desplazamiento, string nombre)
        {
            Objeto objeto = new Objeto();

            // Crear parte vertical de la letra T
            List<Punto> puntosVerticalFrontal = new List<Punto>
            {
                new Punto(-0.5f + desplazamiento.X, 1.0f + desplazamiento.Y, 0.5f + desplazamiento.Z),
                new Punto(0.5f + desplazamiento.X, 1.0f + desplazamiento.Y, 0.5f + desplazamiento.Z),
                new Punto(0.5f + desplazamiento.X, -1.0f + desplazamiento.Y, 0.5f + desplazamiento.Z),
                new Punto(-0.5f + desplazamiento.X, -1.0f + desplazamiento.Y, 0.5f + desplazamiento.Z)
            };

            List<Punto> puntosVerticalTrasero = new List<Punto>
            {
                new Punto(-0.5f + desplazamiento.X, 1.0f + desplazamiento.Y, -0.5f + desplazamiento.Z),
                new Punto(0.5f + desplazamiento.X, 1.0f + desplazamiento.Y, -0.5f + desplazamiento.Z),
                new Punto(0.5f + desplazamiento.X, -1.0f + desplazamiento.Y, -0.5f + desplazamiento.Z),
                new Punto(-0.5f + desplazamiento.X, -1.0f + desplazamiento.Y, -0.5f + desplazamiento.Z)
            };

            Poligono frontalVertical = new Poligono(puntosVerticalFrontal);
            Poligono traseroVertical = new Poligono(puntosVerticalTrasero, new Vector3DTO(0.0f, 1.0f, 0.0f));
            Poligono izquierdaVertical = new Poligono(new List<Punto> { puntosVerticalFrontal[0], puntosVerticalTrasero[0], puntosVerticalTrasero[3], puntosVerticalFrontal[3] }, new Vector3DTO(1.0f, 1.0f, 0.0f));
            Poligono derechaVertical = new Poligono(new List<Punto> { puntosVerticalFrontal[1], puntosVerticalTrasero[1], puntosVerticalTrasero[2], puntosVerticalFrontal[2] }, new Vector3DTO(1.0f, 1.0f, 0.0f));
            Poligono arribaVertical = new Poligono(new List<Punto> { puntosVerticalFrontal[0], puntosVerticalFrontal[1], puntosVerticalTrasero[1], puntosVerticalTrasero[0] }, new Vector3DTO(1.0f, 1.0f, 0.0f));
            Poligono abajoVertical = new Poligono(new List<Punto> { puntosVerticalFrontal[3], puntosVerticalFrontal[2], puntosVerticalTrasero[2], puntosVerticalTrasero[3] }, new Vector3DTO(1.0f, 1.0f, 0.0f));

            Parte parteVertical = new Parte();
            parteVertical.AgregarPoligono("frontal", frontalVertical);
            parteVertical.AgregarPoligono("trasero", traseroVertical);
            parteVertical.AgregarPoligono("izquierda", izquierdaVertical);
            parteVertical.AgregarPoligono("derecha", derechaVertical);
            parteVertical.AgregarPoligono("arriba", arribaVertical);
            parteVertical.AgregarPoligono("abajo", abajoVertical);

            objeto.AgregarParte("parteVertical", parteVertical);

            // Crear parte horizontal de la letra T
            List<Punto> puntosHorizontalFrontal = new List<Punto>
            {
                new Punto(-1.0f + desplazamiento.X, 1.5f + desplazamiento.Y, 0.5f + desplazamiento.Z),
                new Punto(1.0f + desplazamiento.X, 1.5f + desplazamiento.Y, 0.5f + desplazamiento.Z),
                new Punto(1.0f + desplazamiento.X, 1.0f + desplazamiento.Y, 0.5f + desplazamiento.Z),
                new Punto(-1.0f + desplazamiento.X, 1.0f + desplazamiento.Y, 0.5f + desplazamiento.Z)
            };

            List<Punto> puntosHorizontalTrasero = new List<Punto>
            {
                new Punto(-1.0f + desplazamiento.X, 1.5f + desplazamiento.Y, -0.5f + desplazamiento.Z),
                new Punto(1.0f + desplazamiento.X, 1.5f + desplazamiento.Y, -0.5f + desplazamiento.Z),
                new Punto(1.0f + desplazamiento.X, 1.0f + desplazamiento.Y, -0.5f + desplazamiento.Z),
                new Punto(-1.0f + desplazamiento.X, 1.0f + desplazamiento.Y, -0.5f + desplazamiento.Z)
            };

            Poligono frontalHorizontal = new Poligono(puntosHorizontalFrontal);
            Poligono traseroHorizontal = new Poligono(puntosHorizontalTrasero, new Vector3DTO(0.0f, 1.0f, 0.0f));
            Poligono izquierdaHorizontal = new Poligono(new List<Punto> { puntosHorizontalFrontal[0], puntosHorizontalTrasero[0], puntosHorizontalTrasero[3], puntosHorizontalFrontal[3] }, new Vector3DTO(1.0f, 1.0f, 0.0f));
            Poligono derechaHorizontal = new Poligono(new List<Punto> { puntosHorizontalFrontal[1], puntosHorizontalTrasero[1], puntosHorizontalTrasero[2], puntosHorizontalFrontal[2] }, new Vector3DTO(1.0f, 1.0f, 0.0f));
            Poligono arribaHorizontal = new Poligono(new List<Punto> { puntosHorizontalFrontal[0], puntosHorizontalFrontal[1], puntosHorizontalTrasero[1], puntosHorizontalTrasero[0] }, new Vector3DTO(1.0f, 1.0f, 0.0f));
            Poligono abajoHorizontal = new Poligono(new List<Punto> { puntosHorizontalFrontal[3], puntosHorizontalFrontal[2], puntosHorizontalTrasero[2], puntosHorizontalTrasero[3] }, new Vector3DTO(1.0f, 1.0f, 0.0f));

            Parte parteHorizontal = new Parte();
            parteHorizontal.AgregarPoligono("frontal", frontalHorizontal);
            parteHorizontal.AgregarPoligono("trasero", traseroHorizontal);
            parteHorizontal.AgregarPoligono("izquierda", izquierdaHorizontal);
            parteHorizontal.AgregarPoligono("derecha", derechaHorizontal);
            parteHorizontal.AgregarPoligono("arriba", arribaHorizontal);
            parteHorizontal.AgregarPoligono("abajo", abajoHorizontal);

            objeto.AgregarParte("parteHorizontal", parteHorizontal);

            _escenario.AgregarObjeto(nombre, objeto);
        }

        private void crearCubo(Vector3DTO desplazamiento, string nombre)
        {
            Objeto objeto = new Objeto();

            // Crear parte horizontal de la letra T
            List<Punto> puntosHorizontalFrontal = new List<Punto>
            {
                new Punto(-0.5f + desplazamiento.X, 1.5f + desplazamiento.Y, 0.5f + desplazamiento.Z),
                new Punto(0.5f + desplazamiento.X, 1.5f + desplazamiento.Y, 0.5f + desplazamiento.Z),
                new Punto(0.5f + desplazamiento.X, 0.5f + desplazamiento.Y, 0.5f + desplazamiento.Z),
                new Punto(-0.5f + desplazamiento.X, 0.5f + desplazamiento.Y, 0.5f + desplazamiento.Z)
            };

            List<Punto> puntosHorizontalTrasero = new List<Punto>
            {
                new Punto(-0.5f + desplazamiento.X, 1.5f + desplazamiento.Y, -0.5f + desplazamiento.Z),
                new Punto(0.5f + desplazamiento.X, 1.5f + desplazamiento.Y, -0.5f + desplazamiento.Z),
                new Punto(0.5f + desplazamiento.X, 0.5f + desplazamiento.Y, -0.5f + desplazamiento.Z),
                new Punto(-0.5f + desplazamiento.X, 0.5f + desplazamiento.Y, -0.5f + desplazamiento.Z)
            };

            Poligono frontalHorizontal = new Poligono(puntosHorizontalFrontal);
            Poligono traseroHorizontal = new Poligono(puntosHorizontalTrasero, new Vector3DTO(0.0f, 1.0f, 0.0f));
            Poligono izquierdaHorizontal = new Poligono(new List<Punto> { puntosHorizontalFrontal[0], puntosHorizontalTrasero[0], puntosHorizontalTrasero[3], puntosHorizontalFrontal[3] }, new Vector3DTO(1.0f, 1.0f, 0.0f));
            Poligono derechaHorizontal = new Poligono(new List<Punto> { puntosHorizontalFrontal[1], puntosHorizontalTrasero[1], puntosHorizontalTrasero[2], puntosHorizontalFrontal[2] }, new Vector3DTO(1.0f, 1.0f, 0.0f));
            Poligono arribaHorizontal = new Poligono(new List<Punto> { puntosHorizontalFrontal[0], puntosHorizontalFrontal[1], puntosHorizontalTrasero[1], puntosHorizontalTrasero[0] }, new Vector3DTO(1.0f, 1.0f, 0.0f));
            Poligono abajoHorizontal = new Poligono(new List<Punto> { puntosHorizontalFrontal[3], puntosHorizontalFrontal[2], puntosHorizontalTrasero[2], puntosHorizontalTrasero[3] }, new Vector3DTO(1.0f, 1.0f, 0.0f));

            Parte parteHorizontal = new Parte();
            parteHorizontal.AgregarPoligono("frontal", frontalHorizontal);
            parteHorizontal.AgregarPoligono("trasero", traseroHorizontal);
            parteHorizontal.AgregarPoligono("izquierda", izquierdaHorizontal);
            parteHorizontal.AgregarPoligono("derecha", derechaHorizontal);
            parteHorizontal.AgregarPoligono("arriba", arribaHorizontal);
            parteHorizontal.AgregarPoligono("abajo", abajoHorizontal);

            objeto.AgregarParte("parte1", parteHorizontal);
            _escenario.AgregarObjeto(nombre, objeto);
        }
    }
}
