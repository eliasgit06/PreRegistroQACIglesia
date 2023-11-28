using System;
using System.Collections.Generic;

namespace QACIglesia.Model
{
    public interface ItmpParticipantesRepositorio : IDisposable
    {
        tmpParticipantes TraerPorID(int id);
        void Agregar(tmpParticipantes eAparticipantes);
        tmpParticipantes TraerPor(string cedula);
        IList<tmpParticipantes> TraerPorCentroID(int centroID);
        bool Existe(string cedula);
        void Eliminar(int id);
        void Guardar();
    }
}
