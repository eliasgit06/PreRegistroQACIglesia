using QACIglesia.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EBF.EA.DomainModel.Repository
{
    public class HorarioRepositorio : IHorarioRepositorio, IDisposable
    {

        private QACDataContext context;

        public HorarioRepositorio(QACDataContext cont)
        {
            this.context = cont;
        }

        public void Agregar(Horario horario)
        {

            context.Horarios.InsertOnSubmit(horario);

        }

        public void Actualizar(Horario horario)
        {
            var buscar = from a in context.Horarios
                         where a.Id == horario.Id
                         select a;

            Horario h = buscar.First();

            h.Dia = horario.Dia.ToUpper();
            h.HoraInicio = horario.HoraInicio;
            h.HoraFin = horario.HoraFin;
            h.Lugar = horario.Lugar;
            h.Direccion = horario.Direccion;

            context.SubmitChanges();
        }

        public void Eliminar(Horario horario)
        {
            var buscar = from a in context.Horarios
                         where a.Id == horario.Id
                         select a;

            context.Horarios.DeleteOnSubmit(buscar.First());
        }

        public Horario TraerPorID(int id)
        {
            var buscar = from a in context.Horarios
                         where a.Id == id
                         select a;

            return buscar.Any() ? buscar.First() : null;
        }

        public IList<Horario> TraerMuchosPorNucleoID(int id)
        {
            var buscar = from a in context.Horarios
                         where a.Nucleo_Horario == id
                         select a;

            return buscar.Any() ? buscar.ToList() : null;
        }

        public IList<Horario> TraerPreHorarioPorNucleoID(int id)
        {
            var buscar = from a in context.Horarios
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
