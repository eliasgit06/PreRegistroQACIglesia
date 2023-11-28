namespace QACIglesia.Model
{
    public partial class Alfabetizador
    {
        public override string ToString()
        {
            return string.Format("Nombres: {0}\n" +
                    "Apellidos: {1}\n" +
                    "Documento con guiones: {2}\n" +
                    "Documento limpio: {3}\n" +
                    "Sexo: {4}\n" +
                    "Fecha Nacimiento: {5}\n" +
                    "Teléfono: {6}\n" +
                    "Email: {7}\n" +
                    "Experiencia: {8}\n" +
                    "Trabajo Actual: {9}\n" +
                    "Provincia: {10}\n" +
                    "Municipio: {11}\n" +
                    "Distrito Municipal: {12}\n" +
                    "Sección: {13}\n" +
                    "Barrio: {14}\n" +
                    "Sub-Barrio: {15}\n" +
                    "Dirección: {16}\n" +
                    "Tipo Documento: {17}\n" +
                    "Experiencia Educacion Adultos: {18}\n" +
                    "Alfabetizador Nivel Educativo: {19}\n" +
                    "ID: {20}",
                    string.IsNullOrEmpty(Nombre) ? "null" : Nombre,
                    string.IsNullOrEmpty(Apellido) ? "null" : Apellido,
                    string.IsNullOrEmpty(Documento) ? "null" : Documento,
                    string.IsNullOrEmpty(DocumentoPlain) ? "null" : DocumentoPlain,
                    string.IsNullOrEmpty(Sexo) ? "null" : Sexo,
                    !FechaNacimiento.HasValue ? "null" : FechaNacimiento.Value.ToString("yyyy-MM-dd"),
                    string.IsNullOrEmpty(Telefono) ? "null" : Telefono,
                    string.IsNullOrEmpty(Email) ? "null" : Email,
                    string.IsNullOrEmpty(Experiencia) ? "null" : Experiencia,
                    string.IsNullOrEmpty(TrabajoActual) ? "null" : TrabajoActual,
                    string.IsNullOrEmpty(Provincia) ? "null" : Provincia,
                    string.IsNullOrEmpty(Municipio) ? "null" : Municipio,
                    string.IsNullOrEmpty(DistritoMunicipal) ? "null" : DistritoMunicipal,
                    string.IsNullOrEmpty(Seccion) ? "null" : Seccion,
                    string.IsNullOrEmpty(Barrio) ? "null" : Barrio,
                    string.IsNullOrEmpty(SubBarrio) ? "null" : SubBarrio,
                    string.IsNullOrEmpty(Direccion) ? "null" : Direccion,
                    string.IsNullOrEmpty(TipoDocumento) ? "null" : TipoDocumento,
                    string.IsNullOrEmpty(ExperienciaEducacionAdultos) ? "null" : ExperienciaEducacionAdultos,
                    Alfabetizador_NivelEducativo == 0 ? "0" : Alfabetizador_NivelEducativo.ToString(),
                    Id);

        }


    }
}
