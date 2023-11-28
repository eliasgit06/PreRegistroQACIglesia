using QACIglesia.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QACIglesia.Repository
{
    public class CentroAprendizajeRepositorio : ICentroAprendizajeRepositorio, IDisposable
    {
        private EBFDataContext context;

        public CentroAprendizajeRepositorio(EBFDataContext cont)
        {
            this.context = cont;

        }

        public CentroAprendizaje TraerCentroPorID(int id)
        {
            var buscar = from n in context.CentroAprendizajes
                         where n.Id == id
                         select n;

            return buscar.Any() ? buscar.First() : null;
        }

        public IList<CentroAprendizaje> TraerPorRegionalDistrito(int regionalID, int distritoID)
        {
            var buscar = from n in context.CentroAprendizajes
                         where n.RegionalId == regionalID && n.DistritoId == distritoID
                         select n;

            return buscar.Any() ? buscar.ToList() : null;
        }


        public IList<int> TraerIDsDeCentrosPorRegionalDistrito(int regionalID, int distritoID)
        {
            var buscar = from n in context.CentroAprendizajes
                         where n.RegionalId == regionalID && n.DistritoId == distritoID
                         select n.Id;

            return buscar.Any() ? buscar.ToList() : null;
        }

        public IList<int> TraerIDsDeCentrosPorRegionalID(int regionalID)
        {
            var buscar = from n in context.CentroAprendizajes
                         where n.RegionalId == regionalID
                         select n.Id;

            return buscar.Any() ? buscar.ToList() : null;
        }

        public IList<int> TraerIDsDeCentrosPorTodoLosIDs()
        {
            var buscar = from n in context.CentroAprendizajes
                         select n.Id;

            return buscar.Any() ? buscar.ToList() : null;
        }

        public CentroAprendizaje TraerPorCodigoCentro(string codigoCentro)
        {
            var buscar = from n in context.CentroAprendizajes
                         where n.CodigoCentro == codigoCentro
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
