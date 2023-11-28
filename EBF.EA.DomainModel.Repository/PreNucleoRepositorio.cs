using QACIglesia.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EBF.EA.DomainModel.Repository
{
    public class PreNucleoRepositorio : IPreNucleoRepositorio, IDisposable
    {
        private QACDataContext context;

        public PreNucleoRepositorio(QACDataContext cont)
        {
            this.context = cont;
        }

        public void Agregar(PreNucleo preNucleo)
        {
            context.PreNucleos.InsertOnSubmit(preNucleo);
        }

        public void Actualizar(PreNucleo preNucleo)
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
            esp.TipoDocumento = preNucleo.TipoDocumento;
            esp.Edad = preNucleo.Edad;
            esp.Nombre = preNucleo.Nombre;
            esp.EstatusNucleo = preNucleo.EstatusNucleo;
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
            esp.DocumentoIdentidad = preNucleo.DocumentoIdentidad; //preNucleo.DocumentoIdentidad == null ? (preNucleo.DocumentoIdentidad == null ? null : preNucleo.DocumentoIdentidad.Replace("-", "")) : preNucleo.DocumentoIdentidad.Trim().Replace("-", "");
            esp.DocumentoIdentidadPlain = preNucleo.DocumentoIdentidadPlain;// preNucleo.DocumentoIdentidadPlain == null ? (preNucleo.DocumentoIdentidadPlain == null ? null : preNucleo.DocumentoIdentidadPlain.Replace("-", "")) : preNucleo.DocumentoIdentidadPlain.Trim().Replace("-", "");

            esp.Sexo = preNucleo.Sexo;
            esp.Email = preNucleo.Email;
            esp.Nacimiento = preNucleo.Nacimiento;
            esp.Telefono = preNucleo.Telefono;

            esp.Experiencia = preNucleo.Experiencia;//string.IsNullOrEmpty(preNucleo.Experiencia)? null : preNucleo.Experiencia.Substring(0,1);
            esp.TrabajoActual = preNucleo.TrabajoActual;
            esp.Observaciones = preNucleo.Observaciones;
            esp.Obs = preNucleo.Obs;
            esp.nucleoConfiable = preNucleo.nucleoConfiable;
            esp.OrganizacionConfiable = preNucleo.OrganizacionConfiable;
            esp.fechaCallCenter = preNucleo.fechaCallCenter;
            esp.NotasCallCenter = preNucleo.NotasCallCenter;
            esp.UsuarioCallCenter = preNucleo.UsuarioCallCenter;
            esp.EnRevisionCC = preNucleo.EnRevisionCC;
            context.SubmitChanges();

        }

        public PreNucleo TraerPorID(int id)
        {
            var buscar = from a in context.PreNucleos
                         where a.Id == id
                         select a;

            return buscar.Any() ? buscar.First() : null;
        }

        public IList<PreNucleo> TraerPorProvinciaYMunicipio(string provinciaID, string municipioID)
        {
            var buscar = from a in context.PreNucleos
                         where a.ProvinciaId == provinciaID && a.MunicipioId == municipioID // && a.EstatusNucleo >= 3
                         select a;

            return buscar.Any() ? buscar.ToList() : null;
        }

        public IList<PreNucleo> TraerPorProvinciaYMunicipioPorEstatus(string provinciaID, string municipioID, string estatus)
        {
            var buscar = from a in context.PreNucleos
                         where a.ProvinciaId == provinciaID && a.MunicipioId == municipioID && a.EstatusNucleo == Int32.Parse(estatus) // && a.EstatusNucleo >= 3
                         select a;

            return buscar.Any() ? buscar.ToList() : null;
        }


        public void Guardar()
        {

            context.SubmitChanges();

        }

        public string DescripcionDeEstatusPreNucleo(int codigo)
        {
            switch (codigo)
            {
                case 1:
                    return "Inactivo";
                case 2:
                    return "Procesado";
                case 3:
                    return "Error";
                case 4:
                    return "Cancelado";
                case 5:
                    return "Pendiente de autorización";
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
