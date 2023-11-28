using QACIglesia.Model;
using System;
using System.Linq;

namespace QACIglesia.Repository
{
    public class EducadoresRepositorio : IEducadoresRepositorio, IDisposable
    {
        private EBFDataContext context;

        public EducadoresRepositorio(EBFDataContext cont)
        {
            context = cont;
        }

        public void Eliminar(int id)
        {

            var buscar = from e in context.Educadores
                         where e.Id == id
                         select e;


            if (buscar.Any())
                context.Educadores.DeleteOnSubmit(buscar.First());
        }
        public void Actualizar()
        {
            Guardar();
        }

        public void Agregar(Educadores educadores)
        {
            context.Educadores.InsertOnSubmit(educadores);
        }

        public Educadores TraerPorId(int id)
        {
            var buscar = from e in context.Educadores
                         where e.Id == id
                         select e;

            return buscar.Any() ? buscar.First() : null;
        }

        public Educadores TraerPor(string cedula)
        {
            var buscar = from e in context.Educadores
                         where e.DocumentoIdentidad.Replace("-", "") == cedula.Replace("-", "")
                         select e;

            return buscar.Any() ? buscar.First() : null;
        }

        public bool Existe(string cedula)
        {
            var buscar = from e in context.Educadores
                         where e.DocumentoIdentidad.Replace("-", "") == cedula.Replace("-", "")
                         select e;
            return buscar.Any();
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
