using QACIglesia.Model;
using System;
using System.Linq;

namespace QACIglesia.Respository
{
    public class QACDBUtilidadRepositorio : IQACDBUtilidadRepositorio, IDisposable
    {
        private QACDataContext context;

        public QACDBUtilidadRepositorio(QACDataContext cont)
        {
            context = cont;
        }

        public int GenerarSeguimientoParticipante(int nucleoID, int participanteID, ref int? resultado)
        {
            context.stp_Generar_Seguimiento_Participante(nucleoID, participanteID, ref resultado);

            return resultado.Value;
        }
        public vw_nucleos_participante TraerAsociacionNucleoParticipantePorCedula(string cedula)
        {

            if (string.IsNullOrEmpty(cedula))
                return null;


            var buscar = from a in context.vw_nucleos_participantes
                         where a.documento_part.Replace("-", "") == cedula.Replace("-", "")
                         orderby a.fecharegistro descending
                         select a;

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
