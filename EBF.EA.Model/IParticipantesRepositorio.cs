using System;

namespace QACIglesia.Model
{
    public interface IParticipantesRepositorio : IDisposable
    {
        void Actualizar(Participantes eAparticipantes);
        void Agregar(Participantes eAparticipantes);
        Participantes TraerPor(string cedula);
        bool Existe(string cedula);
        void Guardar();
    }
}
