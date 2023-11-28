using System;
using System.Collections.Generic;

namespace QACIglesia.Model
{
    public interface ICentroAprendizajeRepositorio : IDisposable
    {
        CentroAprendizaje TraerCentroPorID(int id);
        IList<CentroAprendizaje> TraerPorRegionalDistrito(int regionalID, int distritoID);
        CentroAprendizaje TraerPorCodigoCentro(string codigoCentro);
        IList<int> TraerIDsDeCentrosPorRegionalDistrito(int regionalID, int distritoID);
        IList<int> TraerIDsDeCentrosPorRegionalID(int regionalID);
        IList<int> TraerIDsDeCentrosPorTodoLosIDs();
    }
}
