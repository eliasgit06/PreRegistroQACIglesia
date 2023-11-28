namespace QACIglesia.Model
{
    public partial class Nucleo
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            Nucleo p = (Nucleo)obj;
            return this.Id == p.Id;
        }


    }
}
