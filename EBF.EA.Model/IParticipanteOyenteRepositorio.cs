using System;
using System.Collections.Generic;

namespace QACIglesia.Model
{
    public interface IParticipanteOyenteRepositorio : IDisposable
    {
        void Agregar(ParticipantesOyente preParticipante);
        void Guardar();
        IEnumerable<ParticipantesOyente> TraerPorNucleoID(int id);
    }
}
