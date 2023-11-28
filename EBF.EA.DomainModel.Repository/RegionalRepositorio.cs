using QACIglesia.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QACIglesia.Repository
{
    public class RegionalRepositorio : IRegionalRepositorio, IDisposable
    {
        private EBFDataContext context;

        public RegionalRepositorio(EBFDataContext cont)
        {
            this.context = cont;

        }

        public IList<Regional> TraerTodos()
        {
            var buscar = from n in context.Regionals
                         select n;

            return buscar.Any() ? buscar.ToList() : null;
        }

        public Regional TraerRegionalPorID(string codigoRegional)
        {
            var buscar = from n in context.Regionals
                         where n.codigoregional == codigoRegional
                         select n;

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
