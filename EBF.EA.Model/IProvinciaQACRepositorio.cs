using System;

namespace QACIglesia.Model
{
    public interface IProvinciaQACRepositorio : IDisposable
    {
        bool TienePrioridad(string provinciaid);
    }
}
