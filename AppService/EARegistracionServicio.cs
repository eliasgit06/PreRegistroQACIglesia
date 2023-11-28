using EBF.EA.DomainModel.Repository;
using QACIglesia.Infrastructure;
using QACIglesia.Model;
using QACIglesia.Repository;
using QACIglesia.Respository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppService
{
    public class EARegistracionServicio
    {
        private EBFDataContext dataContext;
        private QACDataContext qacDataContext;
        private GEODBDataContext geodbDataContext;
        private QACParticipantesRepositorio qacParticipanteRepositorio;
        private ParticipantesRepositorio repositorioParticipantes;
        private NacionalidadRepositorio repositorioNacionalidad;
        private EducadoresRepositorio repositorioEducadores;
        private EspacioAprendizajeRepositorio repositorioEspacioAprendizaje;
        private tmpEspacioAprendizajeRepositorio tmprepositorioEspacioAprendizaje;
        private tmpParticipantesRepositorio tmprepositorioParticipantes;
        private CentroAprendizajeRepositorio centroAprendizajeRepositorio;
        private PreHorarioRepositorio preHorarioRepositorio;
        private HorarioRepositorio horarioRepositorio;
        private PreNucleoRepositorio preNucleoRepositorio;
        private NucleoQACRepositorio nucleoRepositorio;
        private PreParticipanteRepositorio preParticipanteRepositorio;
        private AlfabetizadorRepositorio alfabetizadorRepositorio;
        private ParticipanteOyenteRepositorio participanteOyenteRepositorio;
        private QACDBUtilidadRepositorio qacDBUtilidadRepositorio;
        private ProvinciaQACRepositorio provinciaQACRepositorio;
        private OrganizacionRepositorio organizacionRepositorio;
        private OrganizacionPriorizadaRepositorio organizacionPriorizadaRepositorio;
        private NucleoAuditRepositorio nucleoAuditRepositorio;

        public EARegistracionServicio(string qacConnectionString, string geodbConnectionString)
        {
            qacDataContext = new QACDataContext(qacConnectionString);
            geodbDataContext = new GEODBDataContext(geodbConnectionString);
            qacDBUtilidadRepositorio = new QACDBUtilidadRepositorio(qacDataContext);
            qacParticipanteRepositorio = new QACParticipantesRepositorio(qacDataContext);
            preHorarioRepositorio = new PreHorarioRepositorio(qacDataContext);
            horarioRepositorio = new HorarioRepositorio(qacDataContext);
            preNucleoRepositorio = new PreNucleoRepositorio(qacDataContext);
            preParticipanteRepositorio = new PreParticipanteRepositorio(qacDataContext);
            alfabetizadorRepositorio = new AlfabetizadorRepositorio(qacDataContext);
            participanteOyenteRepositorio = new ParticipanteOyenteRepositorio(qacDataContext);
            nucleoRepositorio = new NucleoQACRepositorio(qacDataContext);
            provinciaQACRepositorio = new ProvinciaQACRepositorio(qacDataContext);
            repositorioNacionalidad = new NacionalidadRepositorio(qacDataContext);
            organizacionRepositorio = new OrganizacionRepositorio(qacDataContext);
            organizacionPriorizadaRepositorio = new OrganizacionPriorizadaRepositorio(qacDataContext);
            nucleoAuditRepositorio = new NucleoAuditRepositorio(qacDataContext);
        }

        public EARegistracionServicio(string ebfConnectionString, string qacConnectionString, string geodbConnectionString)
        {
            dataContext = new EBFDataContext(ebfConnectionString);
            qacDataContext = new QACDataContext(qacConnectionString);
            geodbDataContext = new GEODBDataContext(geodbConnectionString);
            qacDBUtilidadRepositorio = new QACDBUtilidadRepositorio(qacDataContext);
            repositorioParticipantes = new ParticipantesRepositorio(dataContext);
            repositorioEducadores = new EducadoresRepositorio(dataContext);
            repositorioEspacioAprendizaje = new EspacioAprendizajeRepositorio(dataContext);
            tmprepositorioParticipantes = new tmpParticipantesRepositorio(dataContext);
            tmprepositorioEspacioAprendizaje = new tmpEspacioAprendizajeRepositorio(dataContext);
            centroAprendizajeRepositorio = new CentroAprendizajeRepositorio(dataContext);
            qacParticipanteRepositorio = new QACParticipantesRepositorio(qacDataContext);
            preHorarioRepositorio = new PreHorarioRepositorio(qacDataContext);
            horarioRepositorio = new HorarioRepositorio(qacDataContext);
            preNucleoRepositorio = new PreNucleoRepositorio(qacDataContext);
            preParticipanteRepositorio = new PreParticipanteRepositorio(qacDataContext);
            alfabetizadorRepositorio = new AlfabetizadorRepositorio(qacDataContext);
            provinciaQACRepositorio = new ProvinciaQACRepositorio(qacDataContext);
            repositorioNacionalidad = new NacionalidadRepositorio(qacDataContext);
            organizacionRepositorio = new OrganizacionRepositorio(qacDataContext);
            organizacionPriorizadaRepositorio = new OrganizacionPriorizadaRepositorio(qacDataContext);
            nucleoAuditRepositorio = new NucleoAuditRepositorio(qacDataContext);
        }

        public void GuardarCambios(Dictionary<string, object> parametros)
        {
            //Pidiendo connection string de data conetxt del servicio
            using (var context = qacDataContext)
            {
                try
                {
                    context.Connection.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                using (context.Transaction = context.Connection.BeginTransaction())
                {
                    try
                    {
                        PreNucleo preNucleo = parametros["preNucleoARegistrar"] == null ? null : (PreNucleo)parametros["preNucleoARegistrar"];

                        string user = parametros["usuario"] == null ? null : parametros["usuario"].ToString();

                        if (preNucleo == null)
                            throw new Exception("preNucleo vacío o invalido");


                        Alfabetizador alfaARegistrar = !parametros.ContainsKey("alfabetizadorARegistrar") ? null : (Alfabetizador)parametros["alfabetizadorARegistrar"];
                        bool alfaAEliminar = !parametros.ContainsKey("alfabetizadorAEliminar") ? false : (bool)parametros["alfabetizadorAEliminar"];

                        List<PreParticipante> preParticipantesAEliminar = !parametros.ContainsKey("preParticipantesAEliminar") ? null : (List<PreParticipante>)parametros["preParticipantesAEliminar"];
                        List<PreParticipante> preParticipantesARegistrar = !parametros.ContainsKey("preParticipantesARegistrar") ? null : (List<PreParticipante>)parametros["preParticipantesARegistrar"];

                        List<PreHorario> preHorariosAEliminar = !parametros.ContainsKey("preHorariosAEliminar") ? null : (List<PreHorario>)parametros["preHorariosAEliminar"];
                        List<PreHorario> preHorariosARegistrar = !parametros.ContainsKey("preHorariosARegistrar") ? null : (List<PreHorario>)parametros["preHorariosARegistrar"];



                        int estatusNucleo = !parametros.ContainsKey("estatusParaPreNucleo") ? 1 : (int)parametros["estatusParaPreNucleo"];

                        if (preNucleo.Id == 0)
                        {

                            preNucleo.EstatusNucleo = estatusNucleo;
                            context.PreNucleos.InsertOnSubmit(preNucleo);
                        }
                        else
                        {
                            var buscar = from a in context.PreNucleos
                                         where a.Id == preNucleo.Id
                                         select a;

                            PreNucleo esp = buscar.First();
                            esp.OrganizacionConfiable = preNucleo.OrganizacionConfiable;
                            esp.nucleoConfiable = preNucleo.nucleoConfiable;
                            esp.fechaCallCenter = preNucleo.fechaCallCenter;
                            esp.NotasCallCenter = preNucleo.NotasCallCenter;
                            esp.UsuarioCallCenter = preNucleo.UsuarioCallCenter;
                            esp.EnRevisionCC = preNucleo.EnRevisionCC;
                            esp.TipoDocumento = preNucleo.TipoDocumento;
                            esp.Edad = preNucleo.Edad;
                            esp.Nombre = preNucleo.Nombre;
                            esp.EstatusNucleo = estatusNucleo;
                            esp.Nucleo_Organizacion = preNucleo.Nucleo_Organizacion;
                            //esp.Organizacion = preNucleo.Organizacion;
                            esp.FechaRegistro = preNucleo.FechaRegistro;
                            esp.ProvinciaId = preNucleo.ProvinciaId;
                            esp.Provincia = preNucleo.Provincia;
                            esp.MunicipioId = preNucleo.MunicipioId;
                            esp.Municipio = preNucleo.Municipio;
                            esp.DistritoMunicipalId = preNucleo.DistritoMunicipalId;
                            esp.DistritoMunicipal = preNucleo.DistritoMunicipal;
                            esp.SeccionId = preNucleo.SeccionId;
                            esp.Seccion = preNucleo.Seccion;
                            esp.BarrioId = preNucleo.BarrioId;
                            esp.Barrio = preNucleo.Barrio;
                            esp.SubBarrioId = preNucleo.SubBarrioId;
                            esp.RegistradoPor = preNucleo.RegistradoPor;
                            esp.Direccion = preNucleo.Direccion;
                            esp.Nucleo_NivelEducativo = preNucleo.Nucleo_NivelEducativo;
                            esp.AlfabetizadorNombre = preNucleo.AlfabetizadorNombre;
                            esp.AlfabetizadorApellido = preNucleo.AlfabetizadorApellido;
                            esp.DocumentoIdentidad = preNucleo.DocumentoIdentidad;
                            esp.DocumentoIdentidadPlain = preNucleo.DocumentoIdentidadPlain;

                            esp.Sexo = preNucleo.Sexo;
                            esp.Email = preNucleo.Email;
                            esp.Nacimiento = preNucleo.Nacimiento;
                            esp.Telefono = preNucleo.Telefono;

                            esp.Experiencia = string.IsNullOrEmpty(preNucleo.Experiencia) ? null : preNucleo.Experiencia.Substring(0, 1);
                            esp.TrabajoActual = preNucleo.TrabajoActual;
                            esp.Observaciones = preNucleo.Observaciones;
                            esp.Obs = preNucleo.Obs;

                            preNucleo = esp;

                        }

                        context.SubmitChanges();

                        if (alfaAEliminar)
                        {
                            preNucleo.AlfabetizadorNombre = null;
                            preNucleo.AlfabetizadorApellido = null;
                            preNucleo.DocumentoIdentidad = null;
                            preNucleo.DocumentoIdentidadPlain = null;

                            context.SubmitChanges();
                        }

                        if (alfaARegistrar != null)
                        {

                            if (alfaARegistrar.Id == 0)
                            {
                                context.Alfabetizadors.InsertOnSubmit(alfaARegistrar);
                            }
                            else
                            {
                                var buscar = from a in context.Alfabetizadors
                                             where a.Id == alfaARegistrar.Id
                                             select a;

                                Alfabetizador part = buscar.First();

                                part.Documento = alfaARegistrar.Documento;
                                part.DocumentoPlain = alfaARegistrar.DocumentoPlain;
                                part.Nombre = alfaARegistrar.Nombre;
                                part.Apellido = alfaARegistrar.Apellido;
                                part.Sexo = alfaARegistrar.Sexo;
                                part.FechaNacimiento = alfaARegistrar.FechaNacimiento;
                                part.ProvinciaId = alfaARegistrar.ProvinciaId;
                                part.Provincia = alfaARegistrar.Provincia;
                                part.MunicipioId = alfaARegistrar.MunicipioId;
                                part.Municipio = alfaARegistrar.Municipio;
                                part.DistritoMunicipalid = alfaARegistrar.DistritoMunicipalid;
                                part.DistritoMunicipal = alfaARegistrar.DistritoMunicipal;
                                part.Seccionid = alfaARegistrar.Seccionid;
                                part.Seccion = alfaARegistrar.Seccion;
                                part.BarrioId = alfaARegistrar.BarrioId;
                                part.Barrio = alfaARegistrar.Barrio;
                                part.SubBarrioId = alfaARegistrar.SubBarrioId;
                                part.SubBarrio = alfaARegistrar.SubBarrio;
                                part.Telefono = alfaARegistrar.Telefono;
                                part.Email = alfaARegistrar.Email;
                                part.Direccion = alfaARegistrar.Direccion;
                                part.Experiencia = alfaARegistrar.Experiencia;
                                part.TrabajoActual = alfaARegistrar.TrabajoActual;
                                part.ExperienciaEducacionAdultos = alfaARegistrar.ExperienciaEducacionAdultos;
                                part.Alfabetizador_NivelEducativo = alfaARegistrar.Alfabetizador_NivelEducativo;

                                alfaARegistrar = part;
                            }

                            context.SubmitChanges();

                            preNucleo.AlfabetizadorNombre = alfaARegistrar.Nombre;
                            preNucleo.AlfabetizadorApellido = alfaARegistrar.Apellido;
                            preNucleo.DocumentoIdentidad = alfaARegistrar.Documento;
                            preNucleo.DocumentoIdentidadPlain = alfaARegistrar.DocumentoPlain;

                            if (alfaARegistrar.FechaNacimiento.HasValue)
                                preNucleo.Edad = Utility.CalcularEdadBasadoEnCumpleanos(alfaARegistrar.FechaNacimiento.Value);
                            else
                                preNucleo.Edad = null;

                            preNucleo.EstatusCapacitacion = preNucleo.Experiencia == "S" ? 2 : 1;
                            preNucleo.TitularNucleo = "[" + (string.IsNullOrEmpty(preNucleo.DocumentoIdentidad) ? Utility.FormatearCedulaConGuiones(preNucleo.DocumentoIdentidadPlain) : (string.IsNullOrEmpty(preNucleo.DocumentoIdentidad) ? "" : Utility.FormatearCedulaConGuiones(preNucleo.DocumentoIdentidad))) + "]" + (string.IsNullOrEmpty(alfaARegistrar.Nombre) ? "" : " " + alfaARegistrar.Nombre) + (string.IsNullOrEmpty(alfaARegistrar.Apellido) ? "" : " " + alfaARegistrar.Apellido);
                            preNucleo.Sexo = alfaARegistrar.Sexo;
                            preNucleo.Email = alfaARegistrar.Email;
                            preNucleo.Nacimiento = alfaARegistrar.FechaNacimiento;
                            preNucleo.Telefono = alfaARegistrar.Telefono;

                            preNucleo.Experiencia = string.IsNullOrEmpty(alfaARegistrar.Experiencia) ? (AlfabetizadorEstaCapacitado(alfaARegistrar.Documento) ? "S" : "N") : alfaARegistrar.Experiencia.Substring(0, 1);
                            preNucleo.TrabajoActual = alfaARegistrar.TrabajoActual;

                            var buscarNucleo = from a in context.PreNucleos
                                               where a.Id == preNucleo.Id
                                               select a;

                            PreNucleo esp = buscarNucleo.First();
                            esp.OrganizacionConfiable = preNucleo.OrganizacionConfiable;
                            esp.nucleoConfiable = preNucleo.nucleoConfiable;
                            esp.fechaCallCenter = preNucleo.fechaCallCenter;
                            esp.NotasCallCenter = preNucleo.NotasCallCenter;
                            esp.UsuarioCallCenter = preNucleo.UsuarioCallCenter;
                            esp.EnRevisionCC = preNucleo.EnRevisionCC;
                            esp.TipoDocumento = preNucleo.TipoDocumento;
                            esp.Edad = preNucleo.Edad;
                            esp.Nombre = preNucleo.Nombre;
                            esp.EstatusNucleo = preNucleo.EstatusNucleo;
                            esp.Nucleo_Organizacion = preNucleo.Nucleo_Organizacion;
                            esp.FechaRegistro = preNucleo.FechaRegistro;
                            esp.ProvinciaId = preNucleo.ProvinciaId;
                            esp.Provincia = preNucleo.Provincia;
                            esp.MunicipioId = preNucleo.MunicipioId;
                            esp.Municipio = preNucleo.Municipio;
                            esp.DistritoMunicipalId = preNucleo.DistritoMunicipalId;
                            esp.DistritoMunicipal = preNucleo.DistritoMunicipal;
                            esp.SeccionId = preNucleo.SeccionId;
                            esp.Seccion = preNucleo.Seccion;
                            esp.BarrioId = preNucleo.BarrioId;
                            esp.Barrio = preNucleo.Barrio;
                            esp.SubBarrioId = preNucleo.SubBarrioId;
                            esp.RegistradoPor = preNucleo.RegistradoPor;
                            esp.Direccion = preNucleo.Direccion;
                            esp.Nucleo_NivelEducativo = preNucleo.Nucleo_NivelEducativo;
                            esp.AlfabetizadorNombre = preNucleo.AlfabetizadorNombre;
                            esp.AlfabetizadorApellido = preNucleo.AlfabetizadorApellido;
                            esp.DocumentoIdentidad = preNucleo.DocumentoIdentidad;
                            esp.DocumentoIdentidadPlain = preNucleo.DocumentoIdentidadPlain;

                            esp.Sexo = preNucleo.Sexo;
                            esp.Email = preNucleo.Email;
                            esp.Nacimiento = preNucleo.Nacimiento;
                            esp.Telefono = preNucleo.Telefono;

                            esp.Experiencia = preNucleo.Experiencia;
                            esp.TrabajoActual = preNucleo.TrabajoActual;
                            esp.Observaciones = preNucleo.Observaciones;
                            esp.Obs = preNucleo.Obs;
                            esp.EstatusCapacitacion = preNucleo.EstatusCapacitacion;
                            esp.TitularNucleo = preNucleo.TitularNucleo;

                            preNucleo = esp;

                            context.SubmitChanges();
                        }

                        if (preParticipantesAEliminar != null)
                        {

                            if (preNucleo.OrganizacionConfiable != null)
                            {
                                foreach (PreParticipante preParticipante in preParticipantesAEliminar)
                                {


                                    var buscar = from a in context.PreParticipantes
                                                 where a.Id == preParticipante.Id
                                                 select a;

                                    PreParticipante p = buscar.First();


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
                                    p.niveleduc = preParticipante.niveleduc;

                                    p.Provinciaid = preNucleo.ProvinciaId;
                                    p.Provincia = preNucleo.Provincia;
                                    p.MunicipioId = preNucleo.MunicipioId;
                                    p.Municipio = preNucleo.Municipio;
                                    p.DistritoMunicipalId = preNucleo.DistritoMunicipalId;
                                    p.DistritoMunicipal = preNucleo.DistritoMunicipal;
                                    p.SeccionId = preNucleo.SeccionId;
                                    p.BarrioId = preNucleo.BarrioId;
                                    p.Barrio = preNucleo.Barrio;
                                    p.SubBarrioId = preNucleo.SubBarrioId;

                                    if (p.Created == null || p.CreatedBy == null || p.fecharegistro == null)
                                    {
                                        p.Created = preNucleo.FechaRegistro;
                                        p.CreatedBy = preNucleo.RegistradoPor;
                                        p.fecharegistro = preNucleo.FechaRegistro;
                                    }
                                    else
                                    {
                                        p.Created = preParticipante.Created;
                                        p.CreatedBy = preParticipante.CreatedBy;
                                        p.fecharegistro = preParticipante.fecharegistro;
                                    }


                                    p.Modified = DateTime.Now;
                                    p.ModifiedBy = user;

                                    p.Nucleo_Participante = null;

                                    context.SubmitChanges();


                                }
                            }
                            else
                            {
                                foreach (PreParticipante p in preParticipantesAEliminar)
                                {

                                    var buscar = from a in context.PreParticipantes
                                                 where a.Id == p.Id
                                                 select a;

                                    context.PreParticipantes.DeleteOnSubmit(buscar.First());

                                }
                            }


                            context.SubmitChanges();
                        }

                        if (preParticipantesARegistrar != null)
                        {
                            foreach (PreParticipante t in preParticipantesARegistrar)
                            {
                                t.Nucleo_Participante = preNucleo.Id;
                                //t.PreNucleo = preNucleo;
                                if (t.Id == 0)
                                {
                                    t.CreatedBy = preNucleo.RegistradoPor;
                                    t.Created = DateTime.Now;
                                    t.fecharegistro = DateTime.Now;

                                    context.PreParticipantes.InsertOnSubmit(t);
                                }
                                else
                                {
                                    var buscar = from a in context.PreParticipantes
                                                 where a.Id == t.Id
                                                 select a;

                                    PreParticipante p = buscar.First();

                                    p.Nombre = t.Nombre;
                                    p.Apellido = t.Apellido;
                                    p.DocumentoIdentidad = t.DocumentoIdentidad;
                                    p.DocumentoPlain = t.DocumentoPlain;
                                    p.TipoDocumento = t.TipoDocumento;
                                    p.FechaNacimiento = t.FechaNacimiento;
                                    p.Direccion = t.Direccion;
                                    p.Edad = t.Edad;
                                    p.Sexo = t.Sexo;
                                    p.Telefono = t.Telefono;
                                    p.Discapacidad = t.Discapacidad;
                                    p.Nacionalidad = t.Nacionalidad;
                                    p.ObsCallCenter = t.ObsCallCenter;
                                    p.SabeLeerEscribir = t.SabeLeerEscribir;
                                    p.ConcienteDeInscripcion = t.ConcienteDeInscripcion;
                                    p.OrganizacionId = t.OrganizacionId;
                                    p.Provinciaid = t.Provinciaid;
                                    p.MunicipioId = t.MunicipioId;
                                    p.DistritoMunicipalId = t.DistritoMunicipalId;
                                    p.SeccionId = t.SeccionId;
                                    p.BarrioId = t.BarrioId;
                                    p.SubBarrioId = t.SubBarrioId;
                                    p.Nucleo_Participante = t.Nucleo_Participante;
                                    p.niveleduc = t.niveleduc;

                                    p.CreatedBy = t.CreatedBy;
                                    p.Created = t.Created;
                                    p.fecharegistro = t.fecharegistro;

                                    p.Modified = DateTime.Now;
                                    p.ModifiedBy = user;
                                }

                                context.SubmitChanges();
                            }
                        }

                        if (preHorariosAEliminar != null)
                        {
                            foreach (PreHorario h in preHorariosAEliminar)
                            {

                                var buscar = from a in context.PreHorarios
                                             where a.Id == h.Id
                                             select a;

                                context.PreHorarios.DeleteOnSubmit(buscar.First());
                            }

                            context.SubmitChanges();
                        }

                        if (preHorariosARegistrar != null)
                        {
                            foreach (PreHorario h in preHorariosARegistrar)
                            {
                                h.Nucleo_Horario = preNucleo.Id;
                                //h.PreNucleo = preNucleo;
                                if (h.Id == 0)
                                {
                                    context.PreHorarios.InsertOnSubmit(h);
                                }
                                else
                                {
                                    var buscar = from a in context.PreHorarios
                                                 where a.Id == h.Id
                                                 select a;

                                    PreHorario part = buscar.First();

                                    part.Dia = h.Dia.ToUpper();
                                    part.HoraInicio = h.HoraInicio;
                                    part.HoraFin = h.HoraFin;
                                    part.Lugar = h.Lugar;
                                    part.Direccion = h.Direccion;
                                }

                                context.SubmitChanges();
                            }
                        }

                        context.SubmitChanges();

                        //commit transaction  
                        context.Transaction.Commit();



                    }
                    catch (Exception ex)
                    {
                        //Rollback transaction if exception occurs  
                        context.Transaction.Rollback();
                        throw ex;
                    }
                }
            }


        }

        public vw_nucleos_participante TraerAsociacionNucleoParticipantePorCedula(string cedula)
        {

            return qacDBUtilidadRepositorio.TraerAsociacionNucleoParticipantePorCedula(cedula);
        }



        public EBFDataContext TraerDataContext()
        {
            return dataContext;
        }

        public GEODBDataContext TraerGEODBDataContext()
        {
            return geodbDataContext;
        }

        public QACDataContext TraerQACDataContext()
        {
            return qacDataContext;
        }


        public bool ProvinciaTienePrioridad(string proivinciaID)
        {
            return provinciaQACRepositorio.TienePrioridad(proivinciaID);
        }

        //Stored Procedure --- 

        //Generar Seguimiento Participante


        public int HabilitarParticipanteDeQACParaPoderRegistrar(int idQACNucleo, int idQACPartcipante)
        {
            int? resultado = -1;

            try
            {
                qacDBUtilidadRepositorio.GenerarSeguimientoParticipante(idQACNucleo, idQACPartcipante, ref resultado);
            }
            catch (Exception ex)
            {
                string hello = ex.Message;
            }

            return resultado.Value;

        }
        //AlfabetizadorConExperienciaEnNucleo
        //Vistas

        //Nucleo-Participante




        public IList<vw_nucleos_participante> TraerListaDeVistaParaTodosPreParticipantes(IList<PreParticipante> lstPre)
        {
            if (lstPre == null)
                return null;

            IList<vw_nucleos_participante> result = null;
            IList<string> lstCedula = new List<string>();
            foreach (PreParticipante p in lstPre)
            {
                if (!(p.TipoDocumento == null || p.TipoDocumento == "N"))
                {
                    if (!string.IsNullOrEmpty(p.DocumentoIdentidad))
                    {

                        lstCedula.Add(p.TipoDocumento == "C" ? Utility.FormatearCedulaConGuiones(p.DocumentoIdentidad) : p.DocumentoIdentidad);
                    }
                }

            }


            var buscar = from a in qacDataContext.vw_nucleos_participantes
                         where lstCedula.ToList().Contains(a.documento_part)
                         orderby a.fecharegistro descending
                         select a;

            result = buscar.Any() ? buscar.ToList() : null;


            return result;


        }
        public IEnumerable<vw_prenucleos_organizacione> TraerPreNucleosConOrganizaciones(string provinciaID, string municipioID, bool nucleoConfiable)
        {

            var buscar = from a in qacDataContext.vw_prenucleos_organizaciones
                         where a.ProvinciaID == provinciaID && a.MunicipioID == municipioID && a.NucleoConfiable == nucleoConfiable
                         orderby a.PreNucleoID descending
                         select a;


            return buscar.Any() ? buscar.ToList() : null;

        }

        public IEnumerable<vw_prenucleos_organizacione> TraerPreNucleosConOrganizaciones(string provinciaID, string municipioID, int organizacionID)
        {

            if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && organizacionID > 0)
            {
                var buscar = from a in qacDataContext.vw_prenucleos_organizaciones
                             where a.ProvinciaID == provinciaID && a.MunicipioID == municipioID && a.OrganizacionID == organizacionID
                             orderby a.PreNucleoID descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && organizacionID > 0)
            {
                var buscar = from a in qacDataContext.vw_prenucleos_organizaciones
                             where a.ProvinciaID == provinciaID && a.OrganizacionID == organizacionID
                             orderby a.PreNucleoID descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else
            {
                var buscar = from a in qacDataContext.vw_prenucleos_organizaciones
                             where a.OrganizacionID == organizacionID
                             orderby a.PreNucleoID descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }

        }

        //Buscar los prenucleos por id de la organizacion
        public IEnumerable<vw_prenucleos_organizacione> TraerPreNucleosConOrganizacionesYConfiabilidad(string provinciaID, string municipioID, int organizacionID, bool esConfiable)
        {

            if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && organizacionID > 0)
            {
                var buscar = from a in qacDataContext.vw_prenucleos_organizaciones
                             where a.ProvinciaID == provinciaID && a.MunicipioID == municipioID && a.OrganizacionID == organizacionID && a.NucleoConfiable == esConfiable && a.EstatusPreNucleo == 1 && (a.EnRevisionCC.HasValue ? !a.EnRevisionCC.Value : true)
                             orderby a.PreNucleoID descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if ((!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && organizacionID > 0))
            {
                var buscar = from a in qacDataContext.vw_prenucleos_organizaciones
                             where a.ProvinciaID == provinciaID && a.OrganizacionID == organizacionID && a.NucleoConfiable == esConfiable && a.EstatusPreNucleo == 1 && (a.EnRevisionCC.HasValue ? !a.EnRevisionCC.Value : true)
                             orderby a.PreNucleoID descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }

            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00")
            {
                var buscar = from a in qacDataContext.vw_prenucleos_organizaciones
                             where a.ProvinciaID == provinciaID && a.MunicipioID == municipioID && a.NucleoConfiable == esConfiable && a.EstatusPreNucleo == 1 && (a.EnRevisionCC.HasValue ? !a.EnRevisionCC.Value : true)
                             orderby a.PreNucleoID descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if ((!string.IsNullOrEmpty(provinciaID) && provinciaID != "00"))
            {
                var buscar = from a in qacDataContext.vw_prenucleos_organizaciones
                             where a.ProvinciaID == provinciaID && a.NucleoConfiable == esConfiable && a.EstatusPreNucleo == 1 && (a.EnRevisionCC.HasValue ? !a.EnRevisionCC.Value : true)
                             orderby a.PreNucleoID descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (organizacionID > 0)
            {
                var buscar = from a in qacDataContext.vw_prenucleos_organizaciones
                             where a.OrganizacionID == organizacionID && a.NucleoConfiable == esConfiable && a.EstatusPreNucleo == 1 && (a.EnRevisionCC.HasValue ? !a.EnRevisionCC.Value : true)
                             orderby a.PreNucleoID descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else
            {
                var buscar = from a in qacDataContext.vw_prenucleos_organizaciones
                             where a.NucleoConfiable == esConfiable && a.EstatusPreNucleo == 1 && (a.EnRevisionCC.HasValue ? !a.EnRevisionCC.Value : true)
                             orderby a.PreNucleoID descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }


        }



        public IEnumerable<vw_prenucleos_organizacione> TraerPreNucleosConOrganizacionesPorEstatus(string provinciaID, string municipioID, int estatus, bool nucleoConfiable)
        {
            if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) || municipioID != "00" && estatus > 0)
            {
                var buscar = from a in qacDataContext.vw_prenucleos_organizaciones
                             where a.ProvinciaID == provinciaID && a.MunicipioID == municipioID && a.EstatusPreNucleo.Value == estatus && a.NucleoConfiable == nucleoConfiable
                             orderby a.PreNucleoID descending
                             select a
                         ;


                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && estatus > 0)
            {
                var buscar = from a in qacDataContext.vw_prenucleos_organizaciones
                             where a.ProvinciaID == provinciaID && a.EstatusPreNucleo.Value == estatus && a.NucleoConfiable == nucleoConfiable
                             orderby a.PreNucleoID descending
                             select a
                        ;


                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (estatus > 0)
            {
                var buscar = from a in qacDataContext.vw_prenucleos_organizaciones
                             where a.EstatusPreNucleo.Value == estatus && a.NucleoConfiable == nucleoConfiable
                             orderby a.PreNucleoID descending
                             select a;


                return buscar.Any() ? buscar.ToList() : null;
            }
            else
            {
                var buscar = from a in qacDataContext.vw_prenucleos_organizaciones
                             where a.NucleoConfiable == nucleoConfiable
                             orderby a.PreNucleoID descending
                             select a;


                return buscar.Any() ? buscar.ToList() : null;
            }

        }


        public vw_nucleos_participante TraerAsociacionNucleoParticipantePorParticipanteEnMemoria(PreParticipante p, IList<vw_nucleos_participante> potencialParticipantes)
        {
            if (potencialParticipantes == null)
                return null;

            if (p.TipoDocumento == null || p.TipoDocumento == "N")
                return null;

            if (string.IsNullOrEmpty(p.DocumentoIdentidad))
                return null;

            var buscar = from a in potencialParticipantes
                         where a.documento_part == p.DocumentoIdentidad
                         orderby a.fecharegistro descending
                         select a;



            if (buscar.Any())
            {
                vw_nucleos_participante vwP = buscar.First();

                vwP.direccion = p.Direccion;
                vwP.telefono = p.Telefono;
                vwP.provinciaid = p.Provinciaid;
                vwP.provincia = p.Provincia;
                vwP.municipioid = p.Provincia;
                vwP.municipio = p.Municipio;


                return vwP;

            }
            else
            {
                return null;
            }

        }

        public vw_nucleos_participante TraerAsociacionNucleoParticipantePorCedulaEnMemoria(PreParticipante p, IList<vw_nucleos_participante> potencialParticipantes)
        {
            if (potencialParticipantes == null)
                return null;

            if (string.IsNullOrEmpty(p.DocumentoIdentidad))
                return null;

            string cedula = p.TipoDocumento == "C" ? Utility.FormatearCedulaConGuiones(p.DocumentoIdentidad) : p.DocumentoIdentidad;

            var buscar = from a in potencialParticipantes
                         where a.documento_part == cedula
                         orderby a.fecharegistro descending
                         select a;

            return buscar.Any() ? buscar.First() : null;

        }
        //
        public PreNucleo TrerPreNucleoPorID(int id)
        {
            return preNucleoRepositorio.TraerPorID(id);
        }


        public void PreRegistrarPreNucleo(PreNucleo preNucleo)
        {
            preNucleoRepositorio.Agregar(preNucleo);
            preNucleoRepositorio.Guardar();
        }

        public void ActualizarPreNucleo(PreNucleo preNucleo)
        {
            preNucleoRepositorio.Actualizar(preNucleo);
            preNucleoRepositorio.Guardar();
        }

        public IList<PreNucleo> TraerPreNucleoPorProvinciaYMunicipio(string provinciaID, string municipioID)
        {
            return preNucleoRepositorio.TraerPorProvinciaYMunicipio(provinciaID, municipioID);
        }

        public IList<PreNucleo> TraerPreNucleoPorProvinciaYMunicipioPorEstatus(string provinciaID, string municipioID, string estatus)
        {
            return preNucleoRepositorio.TraerPorProvinciaYMunicipioPorEstatus(provinciaID, municipioID, estatus);
        }

        public string TraerDescripcionDeEstatusPreNucleo(int codigo)
        {
            return preNucleoRepositorio.DescripcionDeEstatusPreNucleo(codigo);
        }

        public PreParticipante TraerPreParticipanteDeNucleoPorID(int id)
        {
            return preParticipanteRepositorio.TraerPorID(id);
        }


        public PreParticipante TraerPreParticipanteDeNucleoQueNoEsteCanceladoPorCedula(string cedula)
        {

            //A revisar
            return preParticipanteRepositorio.TraerPorCedulaNucleoNoCancelado(cedula);
        }



        public PreParticipante TraerPreParticipanteDeNucleoPorCedula(string cedula)
        {
            return preParticipanteRepositorio.TraerPorCedula(cedula);
        }

        public IList<PreParticipante> TrerPreParticipantesPorNucleoID(int id)
        {
            return preParticipanteRepositorio.TraerMuchosPorNucleoID(id);
        }

        public IList<PreParticipante> TrerPreParticipanteProvinciaMunicipiosPorOrganizacion(string provinciaID, string municipioID, string distritoMunicipalID, string seccionID, string barrioID, string subBarrioID, int organizacionID)
        {
            return preParticipanteRepositorio.TraerMuchosPorProvMuncOrg(provinciaID, municipioID, distritoMunicipalID, seccionID, barrioID, subBarrioID, organizacionID);
        }

        public void PreRegistrarParticipantesNucleo(PreParticipante preParticipante)
        {
            try
            {
                preParticipanteRepositorio.Agregar(preParticipante);
                preParticipanteRepositorio.Guardar();
            }
            catch (Exception ex)
            {

                Exception newEx = new Exception(ex.Message + "<br/><br/>" + preParticipante.ToString(), ex.InnerException);

                throw newEx;

            }

        }

        public void EliminarPreParticipante(PreParticipante p)
        {
            preParticipanteRepositorio.Eliminar(p);
            preParticipanteRepositorio.Guardar();
        }

        public void ActualizarPreParticipante(PreParticipante p)
        {
            preParticipanteRepositorio.Actualizar(p);
        }

        //PreHorario
        public PreHorario TrerPreHorarioPorID(int id)
        {
            return preHorarioRepositorio.TraerPorID(id);
        }

        public IList<PreHorario> TraerPreHorarioPorNucleoID(int id)
        {
            return preHorarioRepositorio.TraerMuchosPorNucleoID(id);
        }

        public void ActualizarHorario(PreHorario preHorario)
        {
            preHorarioRepositorio.Actualizar(preHorario);
            preHorarioRepositorio.Guardar();
        }


        public void EliminarHorario(PreHorario preHorario)
        {
            preHorarioRepositorio.Eliminar(preHorario);
            preHorarioRepositorio.Guardar();
        }


        public void PreRegistrarPreHorariosDeNucleo(PreHorario preHorario)
        {
            preHorarioRepositorio.Agregar(preHorario);
            preHorarioRepositorio.Guardar();
        }

        //Horario
        public Horario TrerHorarioOficialPorID(int id)
        {
            return horarioRepositorio.TraerPorID(id);
        }

        public IList<Horario> TraerHorarioOficialPorNucleoID(int id)
        {
            return horarioRepositorio.TraerMuchosPorNucleoID(id);
        }

        public void ActualizarHorarioOficial(Horario horario)
        {
            horarioRepositorio.Actualizar(horario);
            horarioRepositorio.Guardar();
        }


        public void EliminarHorarioOficial(Horario horario)
        {
            horarioRepositorio.Eliminar(horario);
            horarioRepositorio.Guardar();
        }


        public void RegistrarHorarioOficialDeNucleo(Horario horario)
        {
            horarioRepositorio.Agregar(horario);
            horarioRepositorio.Guardar();
        }


        //Registrado
        public Participantes TraerParticipante(string cedula)
        {
            return repositorioParticipantes.TraerPor(cedula);
        }

        //
        //Preregistrado
        public tmpParticipantes TraerPreParticipante(string cedula)
        {
            return tmprepositorioParticipantes.TraerPor(cedula);
        }

        public bool ChequearSiPreParticipanteEstaRegistrado(string cedula)
        {
            return tmprepositorioParticipantes.EstaPreRegistrada(cedula);
        }


        //
        //Nucleo QAC

        public IEnumerable<Nucleo> TraerNucleosIniciadosAunNoFuncionando(string provinciaID, string municipioID, string distritoMunicipalID, string seccionID, string barrioID, string subBarrioID, int organizacionID)
        {
            if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00" && !string.IsNullOrEmpty(seccionID) && seccionID != "00" && !string.IsNullOrEmpty(barrioID) && barrioID != "00" && !string.IsNullOrEmpty(subBarrioID) && subBarrioID != "00" && organizacionID > 0)
            {
                var buscar = from a in qacDataContext.Nucleos
                             join b in qacDataContext.NucleosAudits on a.Numero equals b.Numero
                             where b.Estatus == 2 && a.EstatusNucleo == 5 && a.ProvinciaId == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.SeccionId == seccionID && a.BarrioId == barrioID && a.SubBarrioId == subBarrioID && a.Nucleo_Organizacion == organizacionID
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00" && !string.IsNullOrEmpty(seccionID) && seccionID != "00" && !string.IsNullOrEmpty(barrioID) && barrioID != "00" && organizacionID > 0)
            {

                var buscar = from a in qacDataContext.Nucleos
                             join b in qacDataContext.NucleosAudits on a.Numero equals b.Numero
                             where b.Estatus == 2 && a.EstatusNucleo == 5 && a.ProvinciaId == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.SeccionId == seccionID && a.BarrioId == barrioID && a.Nucleo_Organizacion == organizacionID
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00" && !string.IsNullOrEmpty(seccionID) && seccionID != "00" && organizacionID > 0)
            {

                var buscar = from a in qacDataContext.Nucleos
                             join b in qacDataContext.NucleosAudits on a.Numero equals b.Numero
                             where b.Estatus == 2 && a.EstatusNucleo == 5 && a.ProvinciaId == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.SeccionId == seccionID && a.Nucleo_Organizacion == organizacionID
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00" && organizacionID > 0)
            {

                var buscar = from a in qacDataContext.Nucleos
                             join b in qacDataContext.NucleosAudits on a.Numero equals b.Numero
                             where b.Estatus == 2 && a.EstatusNucleo == 5 && a.ProvinciaId == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.Nucleo_Organizacion == organizacionID
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && organizacionID > 0)
            {
                var buscar = from a in qacDataContext.Nucleos
                             join b in qacDataContext.NucleosAudits on a.Numero equals b.Numero
                             where b.Estatus == 2 && a.EstatusNucleo == 5 && a.ProvinciaId == provinciaID && a.MunicipioId == municipioID && a.Nucleo_Organizacion == organizacionID
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && organizacionID > 0)
            {

                var buscar = from a in qacDataContext.Nucleos
                             join b in qacDataContext.NucleosAudits on a.Numero equals b.Numero
                             where b.Estatus == 2 && a.EstatusNucleo == 5 && a.ProvinciaId == provinciaID && a.Nucleo_Organizacion == organizacionID
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (organizacionID > 0)
            {
                var buscar = from a in qacDataContext.Nucleos
                             join b in qacDataContext.NucleosAudits on a.Numero equals b.Numero
                             where b.Estatus == 2 && a.EstatusNucleo == 5 && a.Nucleo_Organizacion == organizacionID
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00" && !string.IsNullOrEmpty(seccionID) && seccionID != "00" && !string.IsNullOrEmpty(barrioID) && barrioID != "00" && !string.IsNullOrEmpty(subBarrioID) && subBarrioID != "00")
            {
                var buscar = from a in qacDataContext.Nucleos
                             join b in qacDataContext.NucleosAudits on a.Numero equals b.Numero
                             where b.Estatus == 2 && a.EstatusNucleo == 5 && a.ProvinciaId == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.SeccionId == seccionID && a.BarrioId == barrioID && a.SubBarrioId == subBarrioID
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00" && !string.IsNullOrEmpty(seccionID) && seccionID != "00" && !string.IsNullOrEmpty(barrioID) && barrioID != "00")
            {
                var buscar = from a in qacDataContext.Nucleos
                             join b in qacDataContext.NucleosAudits on a.Numero equals b.Numero
                             where b.Estatus == 2 && a.EstatusNucleo == 5 && a.ProvinciaId == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.SeccionId == seccionID && a.BarrioId == barrioID
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00" && !string.IsNullOrEmpty(seccionID) && seccionID != "00")
            {
                var buscar = from a in qacDataContext.Nucleos
                             join b in qacDataContext.NucleosAudits on a.Numero equals b.Numero
                             where b.Estatus == 2 && a.EstatusNucleo == 5 && a.ProvinciaId == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID && a.SeccionId == seccionID
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00" && !string.IsNullOrEmpty(distritoMunicipalID) && distritoMunicipalID != "00")
            {
                var buscar = from a in qacDataContext.Nucleos
                             join b in qacDataContext.NucleosAudits on a.Numero equals b.Numero
                             where b.Estatus == 2 && a.EstatusNucleo == 5 && a.ProvinciaId == provinciaID && a.MunicipioId == municipioID && a.DistritoMunicipalId == distritoMunicipalID
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00" && !string.IsNullOrEmpty(municipioID) && municipioID != "00")
            {
                var buscar = from a in qacDataContext.Nucleos
                             join b in qacDataContext.NucleosAudits on a.Numero equals b.Numero
                             where b.Estatus == 2 && a.EstatusNucleo == 5 && a.ProvinciaId == provinciaID && a.MunicipioId == municipioID
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else if (!string.IsNullOrEmpty(provinciaID) && provinciaID != "00")
            {
                var buscar = from a in qacDataContext.Nucleos
                             join b in qacDataContext.NucleosAudits on a.Numero equals b.Numero
                             where b.Estatus == 2 && a.EstatusNucleo == 5 && a.ProvinciaId == provinciaID
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else
            {
                var buscar = from a in qacDataContext.Nucleos
                             join b in qacDataContext.NucleosAudits on a.Numero equals b.Numero
                             where b.Estatus == 2 && a.EstatusNucleo == 5
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }

        }

        public IEnumerable<Nucleo> TraerNucleosConPreNucleosPorFecha(IEnumerable<Nucleo> lst, string fecha)
        {
            if (lst != null)
            {

                var buscar = from a in lst
                             join b in qacDataContext.PreNucleos on a.PreNucleo_Nucleo equals b.Id
                             where b.OrganizacionConfiable.HasValue && b.Nucleo_Organizacion != 0 && b.FechaRegistro >= DateTime.Parse(fecha)
                             orderby a.Id descending
                             select a;

                return buscar.Any() ? buscar.ToList() : null;
            }
            else
            {
                return null;
            }
        }


        public bool AlfabetizadorEstaCapacitado(string cedula)
        {
            return nucleoRepositorio.AlfabetizadorConExperienciaEnNucleo(cedula);
        }

        //Nucleo Audit
        public void ActualizarNucleoAudit(NucleosAudit nucleoAudit)
        {
            nucleoAuditRepositorio.Actualizar(nucleoAudit);
            nucleoAuditRepositorio.Guardar();
        }

        public NucleosAudit TraerNucleoAuditPorNumero(int numero)
        {
            return nucleoAuditRepositorio.TraerPorNumero(numero);
        }


        //QAC Nucleo

        public void ActualizarNucleo(Nucleo nucleo)
        {
            nucleoRepositorio.Actualizar(nucleo);
            nucleoRepositorio.Guardar();
        }

        public Nucleo TraerNucleoPorID(int id)
        {
            return nucleoRepositorio.TraerNucleoPorID(id);
        }

        public void RegistrarNucleo(Nucleo nucleo)
        {
            nucleoRepositorio.Agregar(nucleo);
            nucleoRepositorio.Guardar();
        }


        public string TraerDescripcionDeEstatusNucleoOficial(int codigo)
        {
            return nucleoRepositorio.DescripcionDeEstatusNucleoOficial(codigo);
        }



        //
        //QAC Participante

        public void ActualizarParticipanteRegular(QACParticipante participante)
        {
            qacParticipanteRepositorio.Actualizar(participante);
        }

        public IEnumerable<QACParticipante> TraerParticipantesRegualresDeEsteNucleo(int nucleoID)
        {
            return qacParticipanteRepositorio.TraerPorNucleoID(nucleoID);
        }


        public QACParticipante TraerQACParticipantePorCedula(string cedula)
        {
            return qacParticipanteRepositorio.TraerPor(cedula);
        }

        public QACParticipante TraerQACParticipanteEstudiandoPorCedula(string cedula)
        {
            return qacParticipanteRepositorio.TraerPorCedulaEstudiando(cedula);
        }

        public void RegistrarQACParticipante(QACParticipante participante)
        {
            qacParticipanteRepositorio.Agregar(participante);
            qacParticipanteRepositorio.Guardar();
        }



        public IList<CentroAprendizaje> TraerCentros(int regionalID, int distritoID)
        {
            return centroAprendizajeRepositorio.TraerPorRegionalDistrito(regionalID, distritoID);
        }

        public void RegistrarAlfabetizador(Alfabetizador alfabetizador)
        {
            alfabetizadorRepositorio.Agregar(alfabetizador);
            alfabetizadorRepositorio.Guardar();
        }

        public void EliminarAlfabetizador(Alfabetizador alfabetizador)
        {
            alfabetizadorRepositorio.Eliminar(alfabetizador);
            alfabetizadorRepositorio.Guardar();
        }

        public void ActualizarAlfabetizador(Alfabetizador alfabetizador)
        {
            alfabetizadorRepositorio.Actualizar(alfabetizador);
            alfabetizadorRepositorio.Guardar();
        }




        public Alfabetizador BuscarAlfabetizadorPorCedula(string cedula)
        {
            return alfabetizadorRepositorio.TraerPorCedula(cedula);
        }

        public Alfabetizador BuscarAlfabetizadorPorID(int id)
        {
            return alfabetizadorRepositorio.TraerPorID(id);
        }


        public Educadores BuscarEducador(string cedula)
        {
            return repositorioEducadores.TraerPor(cedula);
        }

        public Educadores BuscarEducadorPorID(int id)
        {
            return repositorioEducadores.TraerPorId(id);
        }


        public void RegistrarEducador(Educadores educador)
        {
            repositorioEducadores.Agregar(educador);
            repositorioEducadores.Guardar();
        }

        public void ActualizarEducador()
        {
            repositorioEducadores.Guardar();
        }

        public IList<tmpParticipantes> BuscartmpParticipantes(int centroID)
        {
            return tmprepositorioParticipantes.TraerPorCentroID(centroID);
        }

        public void RegistrartmpEspacioAprendizaje(tmpEspacioAprendizaje newTmpEspacioAprendizaje)
        {
            tmprepositorioEspacioAprendizaje.Agregar(newTmpEspacioAprendizaje);
            tmprepositorioEspacioAprendizaje.Guardar();
        }


        public void ActualizartmpEspacioAprendizaje()
        {
            tmprepositorioEspacioAprendizaje.Guardar();
        }

        public tmpEspacioAprendizaje BuscarEspacioDeAprendizaje(int id)
        {
            return tmprepositorioEspacioAprendizaje.TraerEAPorID(id);
        }

        public IList<tmpEspacioAprendizaje> BuscartmpEspaciosAprendizajeDeDiferenteCentros(List<int> lstCentroID)
        {
            return tmprepositorioEspacioAprendizaje.TraerEADeDiferenteCentros(lstCentroID);
        }


        public IList<tmpEspacioAprendizaje> BuscartmpEspaciosAprendizaje(int centroID)
        {
            return tmprepositorioEspacioAprendizaje.TraerEAPorCentro(centroID);
        }

        public void RegistrarParticipante(tmpParticipantes newTmpParticipante)
        {
            tmprepositorioParticipantes.Agregar(newTmpParticipante);
            tmprepositorioParticipantes.Guardar();
        }
        public tmpParticipantes BuscartmpParticipantePorID(int id)
        {
            return tmprepositorioParticipantes.TraerPorID(id);
        }
        public void ActualizartmpParticipante()
        {   //
            //
            tmprepositorioParticipantes.Guardar();
        }

        public CentroAprendizaje TraerCentroAprendizajePorID(int id)
        {
            return centroAprendizajeRepositorio.TraerCentroPorID(id);
        }

        public CentroAprendizaje TraerCentroPorCodigo(string codigoCentro)
        {
            return centroAprendizajeRepositorio.TraerPorCodigoCentro(codigoCentro);
        }

        public IList<int> BuscarIDsDeCentrosPorRegionYDistritoCodigo(int regionID, int distritoID)
        {
            return centroAprendizajeRepositorio.TraerIDsDeCentrosPorRegionalDistrito(regionID, distritoID);
        }

        public IList<int> BuscarIDsDeCentrosPorRegion(int regionID)
        {
            return centroAprendizajeRepositorio.TraerIDsDeCentrosPorRegionalID(regionID);
        }

        public IList<int> BuscarIDsDeTodosLosCentros()
        {
            return centroAprendizajeRepositorio.TraerIDsDeCentrosPorTodoLosIDs();
        }


        public void EliminarEducador(int id)
        {
            repositorioEducadores.Eliminar(id);
            repositorioEducadores.Guardar();
        }

        public void EliminarParticipante(int id)
        {
            tmprepositorioParticipantes.Eliminar(id);
            tmprepositorioParticipantes.Guardar();
        }

        public void RegistrarOyente(ParticipantesOyente oyente)
        {

            participanteOyenteRepositorio.Agregar(oyente);
            participanteOyenteRepositorio.Guardar();

        }


        public IEnumerable<ParticipantesOyente> TraerOyentesDeEsteNucleo(int nucleoID)
        {
            return participanteOyenteRepositorio.TraerPorNucleoID(nucleoID);
        }

        public bool ChequearSiPreParticipanteEsCertificableEnQAC(PreParticipante p)
        {
            if (p.DocumentoIdentidad != null)
                return qacParticipanteRepositorio.EsCertificable(p.DocumentoIdentidad);
            else if (p.DocumentoPlain != null)
                return qacParticipanteRepositorio.EsCertificable(p.DocumentoPlain);
            else
                return false;
        }

        public bool ChequearSiPreParticipanteEstudiaEnNucleoNoActivoQAC(PreParticipante p)
        {
            if (p.DocumentoIdentidad != null)
                return qacParticipanteRepositorio.EstaEstudiandoEnNucleoNoActivoQAC(p.DocumentoIdentidad);
            else if (p.DocumentoPlain != null)
                return qacParticipanteRepositorio.EstaEstudiandoEnNucleoNoActivoQAC(p.DocumentoPlain);
            else
                return false;
        }

        public bool ChequearSiPreParticipanteExisteEnQAC(PreParticipante p)
        {

            if (p.DocumentoIdentidad != null)
                return qacParticipanteRepositorio.Existe(p.DocumentoIdentidad);
            else if (p.DocumentoPlain != null)
                return qacParticipanteRepositorio.Existe(p.DocumentoPlain);
            else
                return false;
        }


        public void RegistrarPreParticipanteOficialmente(PreParticipante prePart)
        {

            QACParticipante part = new QACParticipante();

            part.Nombre = prePart.Nombre;
            part.Apellido = prePart.Apellido;
            part.DocumentoIdentidad = prePart.DocumentoIdentidad == null ? prePart.DocumentoPlain : prePart.DocumentoIdentidad;
            // part.
            //tmprepositorioParticipantes.Agregar(prePart);
            // tmprepositorioParticipantes.Guardar();
        }

        public string TraerElNombreDeLaNacionalidad(int id)
        {
            return repositorioNacionalidad.TraerNombre(id);
        }

        //Organizacion

        public Organizacione TraerOrganizacionPorID(int id)
        {
            return organizacionRepositorio.FindByID(id);
        }
        //Organizacion Priorizada
        public OrganizacionPriorizada TraerOrganizacionPriorizadaPorOrganizacionID(int id)
        {
            return organizacionPriorizadaRepositorio.FindByOrganizacionID(id);
        }

        public IEnumerable<OrganizacionPriorizada> TraerTodasLasOrganizacionesPriorizadas()
        {
            return organizacionPriorizadaRepositorio.FindAll();
        }


        public bool EnviarNotificacion(List<string> destinatarios, string sujeto, string nombrePlnatillaHTML, Dictionary<string, string> filtro)
        {

            AvanzaMailKit mailkit = new AvanzaMailKit(destinatarios, sujeto);

            //Email mail = new Email();
            //mail.AgregarDestinatario(destinatarios);


            try
            {
                mailkit.ParseBody(nombrePlnatillaHTML, filtro);

                mailkit.Send();
                //mail.SendEmail(filtro, "ErrorEmail.html", "");
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }


        //Autorizar núcleo regular call center
        public void AutorizarNucleoRegular(Dictionary<string, object> parametros)
        {



            using (var context = qacDataContext)
            {
                try
                {
                    context.Connection.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                using (context.Transaction = context.Connection.BeginTransaction())
                {
                    try
                    {
                        Nucleo n = (Nucleo)parametros["nucleo"];
                        string justificacion = parametros["justificacion"].ToString();
                        string usuario = parametros["usuario"].ToString();




                        var buscarNucleo = from a in context.Nucleos
                                           where a.Id == n.Id
                                           select a;

                        Nucleo nucl = buscarNucleo.First();
                        nucl.Observaciones = nucl.Observaciones.Trim() + "  ***  " + justificacion.Trim();

                        var buscarNucleoAudit = from a in context.NucleosAudits
                                                where a.Numero == n.Numero
                                                select a;


                        NucleosAudit nucleoAudit = (NucleosAudit)buscarNucleoAudit.First();
                        nucleoAudit.FechaVerificacion = DateTime.Now;
                        nucleoAudit.usuarioVerifica = usuario;
                        nucleoAudit.Observaciones = nucleoAudit.Observaciones.Trim() + "  ***  " + justificacion.Trim();
                        nucleoAudit.Estatus = 1;


                        context.SubmitChanges();

                        //commit transaction  
                        context.Transaction.Commit();
                        //EnviarNotifiacionDeCallCenter("cancelar");




                    }
                    catch (Exception ex)
                    {


                        //Rollback transaction if exception occurs  
                        context.Transaction.Rollback();
                        throw ex;
                    }
                }
            }

        }

        public void CancelarNucleoRegular(Dictionary<string, object> parametros)
        {

            using (var context = qacDataContext)
            {
                try
                {
                    context.Connection.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                using (context.Transaction = context.Connection.BeginTransaction())
                {
                    try
                    {

                        Nucleo n = (Nucleo)parametros["nucleo"];
                        string justificacion = parametros["justificacion"].ToString();
                        string usuario = parametros["usuario"].ToString();
                        IEnumerable<QACParticipante> lstParts = (IEnumerable<QACParticipante>)parametros["participantesRegulares"];
                        IEnumerable<ParticipantesOyente> oyentes = (IEnumerable<ParticipantesOyente>)parametros["oyentes"];


                        n.EstatusNucleo = 4;
                        n.Observaciones += "  ***  " + justificacion;
                        n.canceladopor = usuario;
                        n.fechacancelado = DateTime.Now;

                        var buscar = from a in context.Nucleos
                                     where a.Id == n.Id
                                     select a;



                        Nucleo nucl = buscar.First();

                        nucl.EstatusNucleo = n.EstatusNucleo;
                        nucl.Observaciones = n.Observaciones;
                        nucl.canceladopor = n.canceladopor;
                        nucl.fechacancelado = n.fechacancelado;

                        context.SubmitChanges();



                        if (lstParts != null)
                        {
                            foreach (QACParticipante p in lstParts)
                            {

                                var buscarPartReg = from a in context.QACParticipantes
                                                    where a.Id == p.Id
                                                    select a;

                                QACParticipante part = buscarPartReg.First();

                                part.Estatus = "N";

                                context.SubmitChanges();
                            }
                        }

                        if (oyentes != null)
                        {
                            foreach (ParticipantesOyente p in oyentes)
                            {

                                var buscarOyente = from a in context.ParticipantesOyentes
                                                   where a.Id == p.Id
                                                   select a;





                                ParticipantesOyente part = buscarOyente.First();

                                part.Estatus = "N";

                                context.SubmitChanges();
                            }
                        }

                        //commit transaction  
                        context.Transaction.Commit();
                        //EnviarNotifiacionDeCallCenter("cancelar");

                    }
                    catch (Exception ex)
                    {


                        //Rollback transaction if exception occurs  
                        context.Transaction.Rollback();
                        throw ex;
                    }
                }
            }



















        }
    }
}
