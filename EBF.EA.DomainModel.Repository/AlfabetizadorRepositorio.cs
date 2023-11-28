using QACIglesia.Model;
using System;
using System.Linq;

namespace EBF.EA.DomainModel.Repository
{
    public class AlfabetizadorRepositorio : IAlfabetizadorRepositorio, IDisposable
    {

        private QACDataContext context;

        public AlfabetizadorRepositorio(QACDataContext cont)
        {
            this.context = cont;
        }

        public void Agregar(Alfabetizador alfabetizador)
        {
            context.Alfabetizadors.InsertOnSubmit(alfabetizador);
        }

        public void Eliminar(Alfabetizador alfabetizador)
        {
            var buscar = from a in context.Alfabetizadors
                         where a.Id == alfabetizador.Id
                         select a;

            context.Alfabetizadors.DeleteOnSubmit(buscar.First());
        }

        public void Actualizar(Alfabetizador alfabetizador)
        {
            var buscar = from a in context.Alfabetizadors
                         where a.Id == alfabetizador.Id
                         select a;

            Alfabetizador part = buscar.First();

            part.Documento = alfabetizador.Documento;//string.IsNullOrEmpty(alfabetizador.Documento) ? null : alfabetizador.Documento.Trim().Replace("-", "");
            part.DocumentoPlain = alfabetizador.DocumentoPlain;//string.IsNullOrEmpty(alfabetizador.DocumentoPlain)? null : alfabetizador.DocumentoPlain.Trim().Replace("-", "");
            part.Nombre = alfabetizador.Nombre;
            part.Apellido = alfabetizador.Apellido;
            part.Sexo = alfabetizador.Sexo;
            part.FechaNacimiento = alfabetizador.FechaNacimiento;

            part.ProvinciaId = alfabetizador.ProvinciaId;
            part.Provincia = alfabetizador.Provincia;
            part.MunicipioId = alfabetizador.MunicipioId;
            part.Municipio = alfabetizador.Municipio;
            part.DistritoMunicipalid = alfabetizador.DistritoMunicipalid;
            part.DistritoMunicipal = alfabetizador.DistritoMunicipal;
            part.Seccionid = alfabetizador.Seccionid;
            part.Seccion = alfabetizador.Seccion;
            part.BarrioId = alfabetizador.BarrioId;
            part.Barrio = alfabetizador.Barrio;
            part.SubBarrioId = alfabetizador.SubBarrioId;
            part.SubBarrio = alfabetizador.SubBarrio;
            part.Telefono = alfabetizador.Telefono;
            part.Email = alfabetizador.Email;
            part.Direccion = alfabetizador.Direccion;
            part.Experiencia = alfabetizador.Experiencia;
            part.TrabajoActual = alfabetizador.TrabajoActual;
            ////
            part.ExperienciaEducacionAdultos = alfabetizador.ExperienciaEducacionAdultos;
            part.Alfabetizador_NivelEducativo = alfabetizador.Alfabetizador_NivelEducativo;

            context.SubmitChanges();
        }

        public Alfabetizador TraerPorID(int id)
        {
            var buscar = from a in context.Alfabetizadors
                         where a.Id == id
                         select a;

            return buscar.Any() ? buscar.First() : null;
        }

        public Alfabetizador TraerPorCedula(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
            {
                return null;

            }

            var buscar = from a in context.Alfabetizadors
                         where a.Documento.Replace("-", "") == cedula.Replace("-", "") || a.DocumentoPlain.Replace("-", "") == cedula.Replace("-", "")
                         select a;

            return buscar.Any() ? buscar.First() : null;

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
