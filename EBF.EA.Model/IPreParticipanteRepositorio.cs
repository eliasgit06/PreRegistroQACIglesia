using System;
using System.Collections.Generic;

namespace QACIglesia.Model
{
    public interface IPreParticipanteRepositorio : IDisposable
    {
        void Agregar(PreParticipante preParticipante);
        void Actualizar(PreParticipante preParticipante);
        PreParticipante TraerPorID(int id);
        PreParticipante TraerPorCedula(string cedula);
        PreParticipante TraerPorCedulaNucleoNoCancelado(string cedula);
        //PreParticipante TraerPorCedulaNoVinculadoANucleo(string cedula);        
        IList<PreParticipante> TraerMuchosPorNucleoID(int id);
        IList<PreParticipante> TraerMuchosPorProvMuncOrg(string provinciaID, string municipioID, string distritoMunicipalID, string seccionID, string barrioID, string subBarrioID, int organizacionID);


        void Eliminar(PreParticipante preParticipante);
        bool Existe(string cedula);
        void Guardar();
    }
}
