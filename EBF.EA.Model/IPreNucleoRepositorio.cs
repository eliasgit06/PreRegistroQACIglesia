using System;
using System.Collections.Generic;

namespace QACIglesia.Model
{
    public interface IPreNucleoRepositorio : IDisposable
    {
        void Agregar(PreNucleo preNucleo);
        PreNucleo TraerPorID(int id);
        IList<PreNucleo> TraerPorProvinciaYMunicipio(string provinciaID, string municipioID);
        IList<PreNucleo> TraerPorProvinciaYMunicipioPorEstatus(string provinciaID, string municipioID, string estatus);
        void Actualizar(PreNucleo preNucleo);
        void Guardar();
        string DescripcionDeEstatusPreNucleo(int codigo);
    }
}
