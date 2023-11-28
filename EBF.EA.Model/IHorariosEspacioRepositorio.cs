using System;

namespace QACIglesia.Model
{
    public interface IHorariosEspacioRepositorio : IDisposable
    {
        void Agregar(HorariosEspacio horariosEspacio);
        void Guardar();
    }
}
