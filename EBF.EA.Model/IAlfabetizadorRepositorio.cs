using System;

namespace QACIglesia.Model
{
    public interface IAlfabetizadorRepositorio : IDisposable
    {
        void Agregar(Alfabetizador preHorario);
        void Eliminar(Alfabetizador alfabetizador);
        void Actualizar(Alfabetizador alfabetizador);
        Alfabetizador TraerPorID(int id);
        Alfabetizador TraerPorCedula(string cedula);
        void Guardar();
    }
}
