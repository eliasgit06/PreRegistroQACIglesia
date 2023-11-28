using QACIglesia.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EBF.EA.DomainModel.Repository
{
    public class ParticipanteOyenteRepositorio : IParticipanteOyenteRepositorio, IDisposable
    {
        private QACDataContext context;

        public ParticipanteOyenteRepositorio(QACDataContext cont)
        {
            context = cont;
        }


        public void Agregar(ParticipantesOyente oyente)
        {
            context.ParticipantesOyentes.InsertOnSubmit(oyente);
        }


        public void Guardar()
        {
            context.SubmitChanges();

        }

        public IEnumerable<ParticipantesOyente> TraerPorNucleoID(int id)
        {
            var buscar = from a in context.ParticipantesOyentes
                         where a.Nucleo_Participante == id
                         select a;

            return buscar.Any() ? buscar.ToList() : null;
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
