using QACIglesia.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EBF.EA.DomainModel.Repository
{
    public class PreHorarioRepositorio : IPreHorarioRepositorio, IDisposable
    {

        private QACDataContext context;

        public PreHorarioRepositorio(QACDataContext cont)
        {
            this.context = cont;
        }

        public void Agregar(PreHorario preHorario)
        {

            context.PreHorarios.InsertOnSubmit(preHorario);

        }

        public void Actualizar(PreHorario preHorario)
        {
            var buscar = from a in context.PreHorarios
                         where a.Id == preHorario.Id
                         select a;

            PreHorario part = buscar.First();

            part.Dia = preHorario.Dia.ToUpper();
            part.HoraInicio = preHorario.HoraInicio;
            part.HoraFin = preHorario.HoraFin;
            part.Lugar = preHorario.Lugar;
            part.Direccion = preHorario.Direccion;

            context.SubmitChanges();
        }

        public void Eliminar(PreHorario preHorario)
        {
            var buscar = from a in context.PreHorarios
                         where a.Id == preHorario.Id
                         select a;

            context.PreHorarios.DeleteOnSubmit(buscar.First());
        }

        public PreHorario TraerPorID(int id)
        {
            var buscar = from a in context.PreHorarios
                         where a.Id == id
                         select a;

            return buscar.Any() ? buscar.First() : null;
        }

        public IList<PreHorario> TraerMuchosPorNucleoID(int id)
        {
            var buscar = from a in context.PreHorarios
                         where a.Nucleo_Horario == id
                         select a;

            return buscar.Any() ? buscar.ToList() : null;
        }

        public IList<PreHorario> TraerPreHorarioPorNucleoID(int id)
        {
            var buscar = from a in context.PreHorarios
                         where a.Nucleo_Horario == id
                         select a;

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
