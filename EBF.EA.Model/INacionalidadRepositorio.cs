using System;

namespace QACIglesia.Model
{
    public interface INacionalidadRepositorio : IDisposable
    {
        string TraerNombre(int id);
    }
}