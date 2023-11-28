namespace QACIglesia.Model
{
    public partial class PreHorario
    {
        public override string ToString()
        {
            return string.Format("Día: {0}\n" +
                                 "Hora de Inicio: {1}\n" +
                                 "Hora de Final: {2}\n" +
                                 "Lugar: {3}\n" +
                                 "Dirección: {4}\n" +
                                 "ID: {5}\n",
                                 string.IsNullOrEmpty(Dia) ? "null" : Dia,
                                 HoraInicio == null ? "null" : HoraInicio.Date.ToString("yyyy-MM-dd h:mm tt"),
                                 HoraFin == null ? "null" : HoraFin.Date.ToString("`yyyy-MM-dd h:mm tt"),
                                 string.IsNullOrEmpty(Lugar) ? "null" : Lugar,
                                 string.IsNullOrEmpty(Direccion) ? "null" : Direccion,
                                 Id);
        }


    }
}
