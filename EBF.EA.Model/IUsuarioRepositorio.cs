using System;

namespace QACIglesia.Model
{
    public interface IUsuarioRepositorio : IDisposable
    {
        Usuario Autenticar(string usuario, string contrasena);
        Usuario TraerUsuarioPorUserName(string userName);
    }
}