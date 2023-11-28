using System;
using System.Collections.Generic;

namespace QACIglesia.Model
{
    public interface IEspacioAprendizajeRepositorio : IDisposable
    {
        void Actualizar(EspacioAprendizaje espacioAprendizaje);
        void Agregar(EspacioAprendizaje espacioAprendizaje);
        IList<EspacioAprendizaje> TraerEAPorCentro(int caID);
        void Guardar();
    }
}