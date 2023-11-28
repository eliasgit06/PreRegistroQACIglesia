using System.Collections.Generic;

namespace QACIglesia.Model
{
    public interface IOrganizacionPriorizadaRepositorio
    {
        OrganizacionPriorizada FindByOrganizacionID(int id);
        IEnumerable<OrganizacionPriorizada> FindAll();
    }
}
