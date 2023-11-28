using System;

namespace QACIglesia.Model
{
    public interface IQACDBUtilidadRepositorio : IDisposable
    {
        int GenerarSeguimientoParticipante(int nucleoID, int participanteID, ref int? resultado);
        vw_nucleos_participante TraerAsociacionNucleoParticipantePorCedula(string cedula);
    }
}
