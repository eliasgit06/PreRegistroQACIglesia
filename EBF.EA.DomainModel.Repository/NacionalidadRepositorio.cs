using QACIglesia.Model;
using System;
using System.Linq;

namespace QACIglesia.Respository
{
    public class NacionalidadRepositorio : INacionalidadRepositorio, IDisposable
    {
        private QACDataContext context;

        public NacionalidadRepositorio(QACDataContext cont)
        {
            context = cont;
        }

        public string TraerNombre(int id)
        {
            var buscar = from a in context.Nacionalidades
                         where a.Id == id
                         select a;

            return buscar.Any() ? buscar.First().Nacionalidad : null;
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
