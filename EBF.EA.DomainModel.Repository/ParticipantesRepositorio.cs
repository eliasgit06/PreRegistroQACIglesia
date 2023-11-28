using QACIglesia.Model;
using System;
using System.Linq;

namespace QACIglesia.Respository
{
    public class ParticipantesRepositorio : IParticipantesRepositorio, IDisposable
    {
        private EBFDataContext context;

        public ParticipantesRepositorio(EBFDataContext cont)
        {
            context = cont;
        }

        public void Actualizar(Participantes participantes)
        {
            context.SubmitChanges();

        }

        public void Agregar(Participantes participantes)
        {
            context.Participantes.InsertOnSubmit(participantes);
        }

        public Participantes TraerPor(string cedula)
        {
            var buscar = from a in context.Participantes
                         where a.DocumentoIdentidad.Replace("-", "") == cedula.Replace("-", "")
                         select a;

            return buscar.Any() ? buscar.First() : null;

        }

        public bool Existe(string cedula)
        {
            var buscar = from a in context.Participantes
                         where a.DocumentoIdentidad.Replace("-", "") == cedula.Replace("-", "")
                         select a;

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
