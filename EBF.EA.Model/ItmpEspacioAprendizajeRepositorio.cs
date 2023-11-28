using System;
using System.Collections.Generic;

namespace QACIglesia.Model
{
    public interface ItmpEspacioAprendizajeRepositorio : IDisposable
    {
        void Agregar(tmpEspacioAprendizaje espacioAprendizaje);
        IList<tmpEspacioAprendizaje> TraerEAPorCentro(int caID);
        void Guardar();
    }
}