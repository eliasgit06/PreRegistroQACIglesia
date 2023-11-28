namespace QACIglesia.Model
{
    public partial class PreParticipante
    {
        public override string ToString()
        {
            return string.Format(
                "Nombres: {0}\n" +
                "Apellidos: {1}\n" +
                "Nacionalidad: {2}\n" +
                "Fecha Nacimiento: {3}\n" +
                "Edad: {4}\n" +
                "Sexo: {5}\n" +
                "DocumentoIdentidad: {6}\n" +
                "DocumentoPlain: {7}\n" +
                "Tipo Documento: {8}\n" +
                "Dirección: {9}\n" +
                "Teléfono: {10}\n" +
                "Discapacidad: {11}\n" +
                "ID: {12}\n",
                string.IsNullOrEmpty(Nombre) ? "null" : Nombre,
                string.IsNullOrEmpty(Apellido) ? "null" : Apellido,
                !Nacionalidad.HasValue ? "null" : Nacionalidad.Value.ToString(),
                !FechaNacimiento.HasValue ? "null" : FechaNacimiento.Value.ToString("yyyy-MM-dd"),
                !Edad.HasValue ? "null" : Edad.Value.ToString(),
                string.IsNullOrEmpty(Sexo) ? "null" : Sexo,
                string.IsNullOrEmpty(DocumentoIdentidad) ? "null" : DocumentoIdentidad,
                string.IsNullOrEmpty(DocumentoPlain) ? "null" : DocumentoPlain,
                string.IsNullOrEmpty(TipoDocumento) ? "null" : TipoDocumento,
                string.IsNullOrEmpty(Direccion) ? "null" : Direccion,
                string.IsNullOrEmpty(Telefono) ? "null" : Telefono,
                !Discapacidad.HasValue ? "null" : Discapacidad.Value.ToString(),
                Id);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            PreParticipante p = (PreParticipante)obj;
            return this.Id == p.Id;
        }

    }
}
