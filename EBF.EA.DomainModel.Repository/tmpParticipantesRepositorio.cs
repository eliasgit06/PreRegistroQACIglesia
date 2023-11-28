using QACIglesia.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QACIglesia.Respository
{
    public class tmpParticipantesRepositorio : ItmpParticipantesRepositorio, IDisposable
    {
        private EBFDataContext context;

        public tmpParticipantesRepositorio(EBFDataContext cont)
        {
            context = cont;
        }
        public void Eliminar(int id)
        {
            var buscar = from a in context.tmpParticipantes
                         where a.Id == id
                         select a;

            if (buscar.Any())
                context.tmpParticipantes.DeleteOnSubmit(buscar.First());
        }


        public void Agregar(tmpParticipantes participantes)
        {
            context.tmpParticipantes.InsertOnSubmit(participantes);
        }

        public tmpParticipantes TraerPorID(int id)
        {
            var buscar = from a in context.tmpParticipantes
                         where a.Id == id
                         select a;

            return buscar.Any() ? buscar.First() : null;

        }


        public tmpParticipantes TraerPor(string cedula)
        {
            var buscar = from a in context.tmpParticipantes
                         where a.DocumentoIdentidad.Replace("-", "") == cedula.Replace("-", "")
                         select a;

            return buscar.Any() ? buscar.First() : null;

        }

        public IList<tmpParticipantes> TraerPorCentroID(int centroID)
        {
            var buscar = from a in context.tmpParticipantes
                         where a.centroid == centroID
                         select a;

            return buscar.Any() ? buscar.ToList() : null;

        }


        public bool EstaPreRegistrada(string cedula)
        {
            var buscar = from a in context.tmpParticipantes
                         where (a.DocumentoIdentidad.Replace("-", "") == cedula.Replace("-", "")) //&& a.Estatus.Value == 'R'
                         select a;

            return buscar.Any();
        }
        public bool Existe(string cedula)
        {
            var buscar = from a in context.tmpParticipantes
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
