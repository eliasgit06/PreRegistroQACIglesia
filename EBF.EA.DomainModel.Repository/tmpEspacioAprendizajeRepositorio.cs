using QACIglesia.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QACIglesia.Repository
{
    public class tmpEspacioAprendizajeRepositorio : ItmpEspacioAprendizajeRepositorio, IDisposable
    {
        private EBFDataContext context;

        public tmpEspacioAprendizajeRepositorio(EBFDataContext cont)
        {
            context = cont;
        }

        public void Agregar(tmpEspacioAprendizaje espacioAprendizaje)
        {
            context.tmpEspacioAprendizajes.InsertOnSubmit(espacioAprendizaje);

        }

        public tmpEspacioAprendizaje TraerEAPorID(int id)
        {
            var buscar = from e in context.tmpEspacioAprendizajes
                         where e.Id == id
                         select e;

            return buscar.Any() ? buscar.First() : null;
        }

        public IList<tmpEspacioAprendizaje> TraerEAPorCentro(int caID)
        {
            var buscar = from e in context.tmpEspacioAprendizajes
                         where e.Centroid == caID
                         select e;

            return buscar.Any() ? buscar.ToList() : null;
        }

        public IList<tmpEspacioAprendizaje> TraerEADeDiferenteCentros(List<int> listaCentro)
        {
            var buscar = from e in context.tmpEspacioAprendizajes
                         where listaCentro.Contains(e.Centroid.Value)
                         select e;

            return buscar.Any() ? buscar.ToList() : null;
        }

        public void Guardar()
        {
            context.SubmitChanges();
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
