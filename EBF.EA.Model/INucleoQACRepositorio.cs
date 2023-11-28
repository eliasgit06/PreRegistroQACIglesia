using System;
using System.Collections.Generic;

namespace QACIglesia.Model
{
    public interface INucleoQACRepositorio : IDisposable
    {

        Nucleo TraerNucleoPorID(int id);
        IList<Nucleo> TraerPorTraerNucleosPorCedula(string cedula);
        bool AlfabetizadorConExperienciaEnNucleo(string cedula);
        void Agregar(Nucleo nucle);
        void Actualizar(Nucleo nucleo);
        void Guardar();
        string DescripcionDeEstatusNucleoOficial(int codigo);
    }
}
