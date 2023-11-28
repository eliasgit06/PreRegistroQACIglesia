using System;

namespace QACIglesia.Model
{
    public interface INucleoAuditRepositorio : IDisposable
    {
        NucleosAudit TraerPorID(int id);
        NucleosAudit TraerPorNumero(int numero);
        void Actualizar(NucleosAudit preNucleo);
        void Guardar();
    }
}
