using System;
using System.Collections.Generic;

namespace QACIglesia.Model
{
    public interface IPreHorarioRepositorio : IDisposable
    {
        void Agregar(PreHorario preHorario);
        void Eliminar(PreHorario preHorario);
        void Actualizar(PreHorario preHorario);
        PreHorario TraerPorID(int id);
        IList<PreHorario> TraerPreHorarioPorNucleoID(int id);
        void Guardar();
    }
}
