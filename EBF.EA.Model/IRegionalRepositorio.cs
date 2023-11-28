using System.Collections.Generic;

namespace QACIglesia.Model
{
    public interface IRegionalRepositorio
    {
        IList<Regional> TraerTodos();
        Regional TraerRegionalPorID(string codigoRegional);
    }
}
