using System;
using System.Collections.Generic;

namespace QACIglesia.Model
{
    public interface IHorarioRepositorio : IDisposable
    {
        void Agregar(Horario horario);
        void Eliminar(Horario horario);
        void Actualizar(Horario horario);
        Horario TraerPorID(int id);
        IList<Horario> TraerPreHorarioPorNucleoID(int id);
        void Guardar();
    }
}
