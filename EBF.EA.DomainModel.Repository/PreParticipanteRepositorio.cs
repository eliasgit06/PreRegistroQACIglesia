using QACIglesia.Infrastructure;
using QACIglesia.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EBF.EA.DomainModel.Repository
{
    public class PreParticipanteRepositorio : IPreParticipanteRepositorio, IDisposable
    {
        private QACDataContext context;

        public PreParticipanteRepositorio(QACDataContext cont)
        {
            context = cont;
        }


        public void Agregar(PreParticipante preParticipante)
        {
            context.PreParticipantes.InsertOnSubmit(preParticipante);
        }

        public PreParticipante TraerPorID(int id)
        {
            var buscar = from a in context.PreParticipantes
                         where a.Id == id
                         select a;

            return buscar.Any() ? buscar.First() : null;
        }

        public IList<PreParticipante> TraerMuchosPorOrganizacionID(int id)
        {
            var buscar = from a in context.PreParticipantes
                         where a.OrganizacionId == id
                         select a;

            return buscar.Any() ? buscar.ToList() : null;
        }

        public PreParticipante TraerPorCedula(string cedula)
        {

            if (string.IsNullOrEmpty(cedula))
                return null;


            var buscar = from a in context.PreParticipantes
                         where a.DocumentoIdentidad == Utility.FormatearCedulaConGuiones(cedula) || a.DocumentoPlain == cedula.Replace("-", "").Trim()
                         orderby a.Id descending
                         select a;

            return buscar.Any() ? buscar.First() : null;
        }

        public PreParticipante TraerPorCedulaNucleoNoCancelado(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
                return null;


            var buscarPreParticipante = from a in context.PreParticipantes
                                        where a.DocumentoIdentidad.Trim().Replace("-", "") == cedula.Trim().Replace("-", "") || a.DocumentoPlain.Trim().Replace("-", "") == cedula.Trim().Replace("-", "")
                                        orderby a.Id descending
                                        select a;

            if (buscarPreParticipante.Any())
            {
                var buscarPreNucleo = from a in context.PreNucleos
                                      where a.EstatusNucleo != 4 && a.Id == buscarPreParticipante.First().Nucleo_Participante
                                      orderby a.Id descending
                                      select a;
                //Si el núcleo existe, entonces devuelve el participante
                return buscarPreNucleo.Any() ? buscarPreParticipante.First() : null;
            }
            else
            {
                return null;
            }

        }

        public void Eliminar(PreParticipante preParticipante)
        {
            var buscar = from a in context.PreParticipantes
                         where a.Id == preParticipante.Id
                         select a;



            context.PreParticipantes.DeleteOnSubmit(buscar.First());
        }

        public void Actualizar(PreParticipante preParticipante)
        {
            var buscar = from a in context.PreParticipantes
                         where a.Id == preParticipante.Id
                         select a;

            PreParticipante p = buscar.First();
            p.fecharegistro = preParticipante.fecharegistro;
            p.Created = preParticipante.Created;
            p.CreatedBy = preParticipante.CreatedBy;
            p.Modified = preParticipante.Modified;
            p.ModifiedBy = preParticipante.ModifiedBy;
            p.niveleduc = preParticipante.niveleduc;
            p.Nombre = preParticipante.Nombre;
            p.Apellido = preParticipante.Apellido;
            p.DocumentoIdentidad = preParticipante.DocumentoIdentidad;
            p.DocumentoPlain = preParticipante.DocumentoPlain;
            p.TipoDocumento = preParticipante.TipoDocumento;
            p.FechaNacimiento = preParticipante.FechaNacimiento;
            p.Direccion = preParticipante.Direccion;
            p.Edad = preParticipante.Edad;
            p.Sexo = preParticipante.Sexo;
            p.Telefono = preParticipante.Telefono;
            p.Discapacidad = preParticipante.Discapacidad;
            p.Nacionalidad = preParticipante.Nacionalidad;
            p.ObsCallCenter = preParticipante.ObsCallCenter;
            p.SabeLeerEscribir = preParticipante.SabeLeerEscribir;
            p.ConcienteDeInscripcion = preParticipante.ConcienteDeInscripcion;
            p.OrganizacionId = preParticipante.OrganizacionId;
            p.Provinciaid = preParticipante.Provinciaid;
            p.Provincia = preParticipante.Provincia;
            p.MunicipioId = preParticipante.MunicipioId;
            p.Municipio = preParticipante.Municipio;
            p.DistritoMunicipalId = preParticipante.DistritoMunicipalId;
            p.DistritoMunicipal = preParticipante.DistritoMunicipal;
            p.SeccionId = preParticipante.SeccionId;
            p.BarrioId = preParticipante.BarrioId;
            p.Barrio = preParticipante.Barrio;
            p.SubBarrioId = preParticipante.SubBarrioId;
            p.Nucleo_Participante = preParticipante.Nucleo_Participante;

            context.SubmitChanges();
        }

        public IList<PreParticipante> TraerMuchosPorNucleoID(int id)
        {
            var buscar = from a in context.PreParticipantes
                         where a.Nucleo_Participante == id
                         select a;

            return buscar.Any() ? buscar.ToList() : null;
        }

        public IList<PreParticipante> TraerMuchosPorProvMuncOrg(string provinciaID, string municipioID, string distritoMunicipalID, string seccionID, string barrioID, string subBarrioID, int organizacionID)
        {
            if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00" && !string.IsNullOrEmpty(seccionID) && seccionID != "00" && !string.IsNullOrEmpty(barrioID) && barrioID != "00" && !string.IsNullOrEmpty(subBarrioID) && subBarrioID != "00" && organizacionID > 0)
            {
                var buscar = from a in context.PreParticipantes
                             where a.Provinciaid == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.SeccionId == seccionID && a.BarrioId == barrioID && a.SubBarrioId == subBarrioID && a.OrganizacionId == organizacionID && a.Nucleo_Participante == null
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00" && !string.IsNullOrEmpty(seccionID) && seccionID != "00" && !string.IsNullOrEmpty(barrioID) && barrioID != "00" && organizacionID > 0)
            {

                var buscar = from a in context.PreParticipantes
                             where a.Provinciaid == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.SeccionId == seccionID && a.BarrioId == barrioID && a.OrganizacionId == organizacionID && a.Nucleo_Participante == null
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00" && !string.IsNullOrEmpty(seccionID) && seccionID != "00" && organizacionID > 0)
            {

                var buscar = from a in context.PreParticipantes
                             where a.Provinciaid == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.SeccionId == seccionID && a.OrganizacionId == organizacionID && a.Nucleo_Participante == null
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00" && organizacionID > 0)
            {

                var buscar = from a in context.PreParticipantes
                             where a.Provinciaid == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.OrganizacionId == organizacionID && a.Nucleo_Participante == null
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && organizacionID > 0)
            {
                var buscar = from a in context.PreParticipantes
                             where a.Provinciaid == provinciaID && a.MunicipioId == municipioID && a.OrganizacionId == organizacionID && a.Nucleo_Participante == null
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && organizacionID > 0)
            {

                var buscar = from a in context.PreParticipantes
                             where a.Provinciaid == provinciaID && a.OrganizacionId == organizacionID && a.Nucleo_Participante == null
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (organizacionID > 0)
            {
                var buscar = from a in context.PreParticipantes
                             where a.OrganizacionId == organizacionID && a.Nucleo_Participante == null
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00" && !string.IsNullOrEmpty(seccionID) && seccionID != "00" && !string.IsNullOrEmpty(barrioID) && barrioID != "00" && !string.IsNullOrEmpty(subBarrioID) && subBarrioID != "00")
            {
                var buscar = from a in context.PreParticipantes
                             where a.Provinciaid == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.SeccionId == seccionID && a.BarrioId == barrioID && a.SubBarrioId == subBarrioID && a.Nucleo_Participante == null
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00" && !string.IsNullOrEmpty(seccionID) && seccionID != "00" && !string.IsNullOrEmpty(barrioID) && barrioID != "00")
            {
                var buscar = from a in context.PreParticipantes
                             where a.Provinciaid == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.SeccionId == seccionID && a.BarrioId == barrioID && a.Nucleo_Participante == null
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00" && !string.IsNullOrEmpty(seccionID) && seccionID != "00")
            {
                var buscar = from a in context.PreParticipantes
                             where a.Provinciaid == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.SeccionId == seccionID && a.Nucleo_Participante == null
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00")
            {
                var buscar = from a in context.PreParticipantes
                             where a.Provinciaid == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.Nucleo_Participante == null
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00")
            {
                var buscar = from a in context.PreParticipantes
                             where a.Provinciaid == provinciaID && a.MunicipioId == municipioID && a.Nucleo_Participante == null
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00")
            {
                var buscar = from a in context.PreParticipantes
                             where a.Provinciaid == provinciaID && a.Nucleo_Participante == null
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else
            {
                var buscar = from a in context.PreParticipantes
                             where a.Nucleo_Participante == null
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }

        }

        public bool Existe(string cedula)
        {
            var buscar = from a in context.PreParticipantes
                         where a.DocumentoPlain == cedula.Replace("-", "")
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
