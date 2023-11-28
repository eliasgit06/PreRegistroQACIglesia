using QACIglesia.Model;
using System;
using System.Linq;

namespace QACIglesia.Repository
{
    public class ProvinciaQACRepositorio : IProvinciaQACRepositorio, IDisposable
    {
        private QACDataContext context;

        public ProvinciaQACRepositorio(QACDataContext cont)
        {
            this.context = cont;

        }

        public bool TienePrioridad(string provinciaid)
        {
            var buscar = from a in context.ProvinciaQACs
                         where a.ProvinciaId == provinciaid && a.priorizado == true
                         select a;

            return buscar.Any();
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
