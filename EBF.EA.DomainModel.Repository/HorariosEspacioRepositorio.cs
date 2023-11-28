using QACIglesia.Model;
using System;

namespace QACIglesia.Repository
{
    public class HorariosEspacioRepositorio : IHorariosEspacioRepositorio, IDisposable
    {
        private EBFDataContext context;

        public HorariosEspacioRepositorio(EBFDataContext cont)
        {
            this.context = cont;

        }

        public void Agregar(HorariosEspacio horariosEspacio)
        {
            context.HorariosEspacios.InsertOnSubmit(horariosEspacio);

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
