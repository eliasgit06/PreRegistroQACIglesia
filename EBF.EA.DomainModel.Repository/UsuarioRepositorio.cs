using QACIglesia.Model;
using System;
using System.Linq;

namespace QACIglesia.Respository
{
    public class UsuarioRepositorio : IUsuarioRepositorio, IDisposable
    {
        private QACDataContext context;

        public UsuarioRepositorio(QACDataContext cont)
        {
            context = cont;
        }

        public Usuario Autenticar(string usuario, string contrasena)
        {
            var buscar = from a in context.Usuarios
                         where a.UserName.ToLower().Trim() == usuario.ToLower().Trim() && a.UserClave.ToLower().Trim() == contrasena.ToLower().Trim()
                         select a;

            return buscar.Any() ? buscar.First() : null;
        }

        public Usuario TraerUsuarioPorUserName(string userName)
        {
            var buscar = from a in context.Usuarios
                         where a.UserName.Trim().ToLower() == userName.Trim().ToLower()
                         select a;

            return buscar.Any() ? buscar.First() : null;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
