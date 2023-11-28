using System;
using System.Collections.Generic;

namespace QACIglesia.Model
{
    public interface IQACParticipantesRepositorio : IDisposable
    {
        QACParticipante TraerPor(string cedula);
        QACParticipante TraerPorCedulaEstudiando(string cedula);
        IEnumerable<QACParticipante> TraerPorNucleoID(int id);
        bool Existe(string cedula);
        bool EsCertificable(string cedula);
        bool EstaEstudiandoEnNucleoNoActivoQAC(string cedula);
        void Agregar(QACParticipante participante);
        void Actualizar(QACParticipante participante);
        void Guardar();

    }
}
