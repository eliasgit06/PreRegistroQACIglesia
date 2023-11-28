using QACIglesia.Model;
using System.Collections.Generic;
using System.Linq;

namespace EBF.EA.DomainModel.Repository
{
    public class OrganizacionPriorizadaRepositorio : IOrganizacionPriorizadaRepositorio
    {

        private QACDataContext context;

        public OrganizacionPriorizadaRepositorio(QACDataContext cont)
        {
            this.context = cont;
        }



        public OrganizacionPriorizada FindByOrganizacionID(int id)
        {
            var buscar = from a in context.OrganizacionPriorizadas
                         where a.IdOrganizacion == id
                         select a;

            return buscar.Any() ? buscar.First() : null;
        }

        //
        //Nota se hizo una excepción y no se trajo 'Sin Organización' 
        public IEnumerable<OrganizacionPriorizada> FindAll()
        {
            var buscar = from a in context.OrganizacionPriorizadas
                         where a.IdOrganizacion != 0
                         select a;

            return buscar.Any() ? buscar.ToList() : null;
        }
    }
}
