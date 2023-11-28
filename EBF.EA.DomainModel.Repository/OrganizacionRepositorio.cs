using QACIglesia.Model;
using System.Linq;

namespace EBF.EA.DomainModel.Repository
{
    public class OrganizacionRepositorio : IOrganizacionRepositorio
    {

        private QACDataContext context;

        public OrganizacionRepositorio(QACDataContext cont)
        {
            this.context = cont;
        }


        public Organizacione FindByID(int id)
        {
            var buscar = from a in context.Organizaciones
                         where a.Id == id
                         select a;

            return buscar.Any() ? buscar.First() : null;
        }


    }
}
