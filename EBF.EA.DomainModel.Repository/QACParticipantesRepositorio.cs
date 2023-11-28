using QACIglesia.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QACIglesia.Respository
{
    public class QACParticipantesRepositorio : IQACParticipantesRepositorio, IDisposable
    {
        private QACDataContext context;

        public QACParticipantesRepositorio(QACDataContext cont)
        {
            context = cont;
        }



        public QACParticipante TraerPor(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
                return null;

            var buscar = from a in context.QACParticipantes
                         where a.DocumentoIdentidad.Trim().Replace("-", "") == cedula.Trim().Replace("-", "") & a.DocumentoPlain.Trim().Replace("-", "") == cedula.Trim().Replace("-", "")
                         orderby a.Id descending
                         select a;

            return buscar.Any() ? buscar.First() : null;

        }

        public QACParticipante TraerPorCedulaEstudiando(string cedula)
        {

            if (string.IsNullOrEmpty(cedula))
                return null;

            var buscar = from a in context.QACParticipantes
                         where a.DocumentoIdentidad.Replace("-", "") == cedula.Replace("-", "") & a.DocumentoPlain.Replace("-", "") == cedula.Replace("-", "") && a.Estatus == "E"
                         orderby a.Id descending
                         select a;

            return buscar.Any() ? buscar.First() : null;

        }

        public IEnumerable<QACParticipante> TraerPorNucleoID(int id)
        {

            var buscar = from a in context.QACParticipantes
                         where a.Nucleo_Participante == id
                         orderby a.Id descending
                         select a;

            return buscar.Any() ? buscar.ToList() : null;

        }

        public bool Existe(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
                return false;

            var buscar = from a in context.QACParticipantes
                         where a.DocumentoIdentidad.Replace("-", "") == cedula.Replace("-", "") & a.DocumentoPlain.Replace("-", "") == cedula.Replace("-", "")
                         select a;

            return buscar.Any();
        }

        public bool EsCertificable(string cedula)
        {

            if (string.IsNullOrEmpty(cedula))
                return false;

            var buscar = from a in context.QACParticipantes
                         where a.DocumentoIdentidad.Replace("-", "") == cedula.Replace("-", "") & a.DocumentoPlain.Replace("-", "") == cedula.Replace("-", "") && (a.certificable.HasValue && a.certificable.Value)
                         select a;

            return buscar.Any();
        }

        public bool EstaEstudiandoEnNucleoNoActivoQAC(string cedula)
        {

            if (string.IsNullOrEmpty(cedula))
                return false;

            var buscar = from a in context.QACParticipantes
                         where a.DocumentoIdentidad.Replace("-", "") == cedula.Replace("-", "") & a.DocumentoPlain.Replace("-", "") == cedula.Replace("-", "") && a.Estatus == "E"
                         orderby a.Id descending
                         select a;

            if (buscar.Any())
            {
                var nucleoBuscar = from a in context.Nucleos
                                   where a.Id == buscar.First().Nucleo_Participante && a.EstatusNucleo != 1
                                   select a;

                return nucleoBuscar.Any();
            }
            return false;

        }



        public void Agregar(QACParticipante participante)
        {
            context.QACParticipantes.InsertOnSubmit(participante);
        }

        public void Actualizar(QACParticipante participante)
        {
            var buscar = from a in context.QACParticipantes
                         where a.Id == participante.Id
                         select a;

            QACParticipante p = buscar.First();
            p.Estatus = participante.Estatus;
            p.Nombre = participante.Nombre;
            p.Apellido = participante.Apellido;
            p.DocumentoIdentidad = participante.DocumentoIdentidad;
            p.DocumentoPlain = participante.DocumentoPlain;
            p.TipoDocumento = participante.TipoDocumento;
            p.FechaNacimiento = participante.FechaNacimiento;
            p.Direccion = participante.Direccion;
            p.Edad = participante.Edad;
            p.Sexo = participante.Sexo;
            p.Telefono = participante.Telefono;
            p.Discapacidad = participante.Discapacidad;
            p.Nacionalidad = participante.Nacionalidad;
            p.Nucleo_Participante = participante.Nucleo_Participante;
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
