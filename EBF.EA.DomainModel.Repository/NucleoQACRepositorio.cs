using QACIglesia.Infrastructure;
using QACIglesia.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QACIglesia.Respository
{
    public class NucleoQACRepositorio : INucleoQACRepositorio, IDisposable
    {
        private QACDataContext context;

        public NucleoQACRepositorio(QACDataContext cont)
        {
            context = cont;
        }

        public Nucleo TraerNucleoPorID(int id)
        {

            var buscar = from a in context.Nucleos
                         where a.Id == id
                         select a;

            return buscar.Any() ? buscar.First() : null;

        }
        public IList<Nucleo> TraerPorTraerNucleosPorCedula(string cedula)
        {
            var buscar = from a in context.Nucleos
                         where a.DocumentoIdentidad == Utility.FormatearCedulaConGuiones(cedula)
                         select a;

            return buscar.Any() ? buscar.ToList() : null;
        }
        public bool AlfabetizadorConExperienciaEnNucleo(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
                return false;

            var buscar = from a in context.Nucleos
                         where (a.DocumentoIdentidad == Utility.FormatearCedulaConGuiones(cedula)) && a.EstatusCapacitacion == 2
                         select a;

            return buscar.Any();
        }

        public void Agregar(Nucleo nucleo)
        {
            context.Nucleos.InsertOnSubmit(nucleo);
            Guardar();
        }

        public void Actualizar(Nucleo nucleo)
        {
            var buscar = from a in context.Nucleos
                         where a.Id == nucleo.Id
                         select a;

            Nucleo n = buscar.First();

            n.TipoDocumento = nucleo.TipoDocumento;
            n.Edad = nucleo.Edad;
            n.Nombre = nucleo.Nombre;
            n.EstatusNucleo = nucleo.EstatusNucleo;
            n.Nucleo_Organizacion = nucleo.Nucleo_Organizacion;
            //n.Organizacion = nucleo.Organizacion;
            n.FechaRegistro = nucleo.FechaRegistro;
            n.ProvinciaId = nucleo.ProvinciaId;
            n.Provincia = nucleo.Provincia;
            n.MunicipioId = nucleo.MunicipioId;
            n.Municipio = nucleo.Municipio;
            n.DistritoMunicipalId = nucleo.DistritoMunicipalId;
            n.DistritoMunicipal = nucleo.DistritoMunicipal;
            n.SeccionId = nucleo.SeccionId;
            n.Seccion = nucleo.Seccion;
            n.BarrioId = nucleo.BarrioId;
            n.Barrio = nucleo.Barrio;
            n.SubBarrioId = nucleo.SubBarrioId;
            n.RegistradoPor = nucleo.RegistradoPor;
            n.Direccion = nucleo.Direccion;
            n.Nucleo_NivelEducativo = nucleo.Nucleo_NivelEducativo;
            n.AlfabetizadorNombre = nucleo.AlfabetizadorNombre;
            n.AlfabetizadorApellido = nucleo.AlfabetizadorApellido;
            n.DocumentoIdentidad = nucleo.DocumentoIdentidad; //nucleo.DocumentoIdentidad == null ? (nucleo.DocumentoIdentidad == null ? null : nucleo.DocumentoIdentidad.Replace("-", "")) : nucleo.DocumentoIdentidad.Trim().Replace("-", "");
            n.DocumentoIdentidadPlain = nucleo.DocumentoIdentidadPlain;// nucleo.DocumentoIdentidadPlain == null ? (nucleo.DocumentoIdentidadPlain == null ? null : nucleo.DocumentoIdentidadPlain.Replace("-", "")) : nucleo.DocumentoIdentidadPlain.Trim().Replace("-", "");

            n.Sexo = nucleo.Sexo;
            n.Email = nucleo.Email;
            n.Nacimiento = nucleo.Nacimiento;
            n.Telefono = nucleo.Telefono;

            n.Experiencia = nucleo.Experiencia;//string.IsNullOrEmpty(nucleo.Experiencia)? null : nucleo.Experiencia.Substring(0,1);
            n.TrabajoActual = nucleo.TrabajoActual;
            n.Observaciones = nucleo.Observaciones;
            n.Obs = nucleo.Obs;

            context.SubmitChanges();

        }

        public void Guardar()
        {
            context.SubmitChanges();
        }

        public string DescripcionDeEstatusNucleoOficial(int codigo)
        {
            switch (codigo)
            {
                case 1:
                    return "Activo";
                case 2:
                    return "Inactivo";
                case 3:
                    return "Concluido";
                case 4:
                    return "Cancelado";
                case 5:
                    return "Iniciado";
                default:
                    return "No disponible";
            }
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
