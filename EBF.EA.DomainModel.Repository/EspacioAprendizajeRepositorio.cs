using QACIglesia.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QACIglesia.Repository
{
    public class EspacioAprendizajeRepositorio : IEspacioAprendizajeRepositorio, IDisposable
    {
        private EBFDataContext context;

        public EspacioAprendizajeRepositorio(EBFDataContext cont)
        {
            context = cont;
        }

        public void Actualizar(EspacioAprendizaje espacioAprendizaje)
        {
            Guardar();
        }

        public void Agregar(EspacioAprendizaje espacioAprendizaje)
        {
            context.EspacioAprendizajes.InsertOnSubmit(espacioAprendizaje);

        }



        public IList<EspacioAprendizaje> TraerEAPorCentro(int caID)
        {
            var buscar = from e in context.EspacioAprendizajes
                         where e.Centroid == caID
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
