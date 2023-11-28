using System;

namespace QACIglesia.Model
{
    public interface IEducadoresRepositorio : IDisposable
    {
        void Agregar(Educadores educadores);
        Educadores TraerPor(string cedula);
        bool Existe(string cedula);
        void Eliminar(int id);
        void Guardar();
    }
}
