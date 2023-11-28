using QACIglesia.Model;
using System;
using System.Linq;

namespace QACIglesia.Respository
{
    public class NucleoAuditRepositorio : INucleoAuditRepositorio, IDisposable
    {
        private QACDataContext context;

        public NucleoAuditRepositorio(QACDataContext cont)
        {
            context = cont;
        }

        public NucleosAudit TraerPorID(int id)
        {
            var buscar = from a in context.NucleosAudits
                         where a.Id == id
                         select a;

            return buscar.Any() ? buscar.First() : null;

        }

        public NucleosAudit TraerPorNumero(int numero)
        {
            var buscar = from a in context.NucleosAudits
                         where a.Numero == numero
                         select a;

            return buscar.Any() ? buscar.First() : null;

        }

        public void Actualizar(NucleosAudit nucleoAudit)
        {
            var buscar = from a in context.NucleosAudits
                         where a.Id == nucleoAudit.Id
                         select a;

            NucleosAudit n = buscar.First();

            n.Estatus = nucleoAudit.Estatus;
            n.FechaVerificacion = nucleoAudit.FechaVerificacion;
            n.usuarioVerifica = nucleoAudit.usuarioVerifica;
            n.Observaciones = nucleoAudit.Observaciones;

            context.SubmitChanges();

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
