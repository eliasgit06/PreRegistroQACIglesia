namespace QACIglesia.Model
{
    public partial class PreNucleo
    {
        public override string ToString()
        {
            return string.Format("Nombre: {0}\n" +
                "Nucleo_Organización: {1}\n" +
                "FechaNacimiento Alfabetizador: {2}\n" +
                "Edad Alfabetizador: {3}\n" +
                "AlfabetizadorNombre: {4}\n" +
                "AlfabetizadorApellido: {5}\n" +
                "DocumentoIdentidad: {6}\n" +
                "DocumentoIdentidadPlain: {7}\n" +
                "Tipo Documento: {8}\n" +
                "Nucleo_NivelEducativo: {9}\n" +
                "Estatus Verificacion: {10}\n" +
                "Estatus Capacitacion: {11}\n" +
                "FlagPrimero: {12}\n" +
                "CedulaError: {13}\n" +
                "KitsEnviados: {14}\n" +
                "KitsEntregados: {15}\n" +
                "KitAlfabetizadorEntregado: {16}\n" +
                "AnimadorAsignado: {17}\n" +
                "AnimadorPropuesto: {18}\n" +
                "NumeroRegJM: {19}\n" +
                "Observaciones: {20}\n" +
                "Experiencias: {21}\n" +
                "Trabajo Actual: {22}\n" +
                "Provincia: {23}\n" +
                "Municipio: {24}\n" +
                "Distrito Municipal: {25}\n" +
                "Sección: {26}\n" +
                "Barrio: {27}\n" +
                "Sub-Barrio ID: {28}\n" +
                "Dirección: {29}\n" +
                "ID: {30}\n",

                string.IsNullOrEmpty(Nombre) ? "null" : Nombre,
                !Nucleo_Organizacion.HasValue ? "null" : Nucleo_Organizacion.Value.ToString(),
                Nacimiento.HasValue ? "null" : Nacimiento.Value.ToString("yyyy-MM-dddd"),
                !Edad.HasValue ? "null" : Edad.Value.ToString(),
                string.IsNullOrEmpty(AlfabetizadorNombre) ? "null" : AlfabetizadorNombre,
                string.IsNullOrEmpty(AlfabetizadorApellido) ? "null" : AlfabetizadorApellido,
                string.IsNullOrEmpty(DocumentoIdentidad) ? "null" : DocumentoIdentidad,
                string.IsNullOrEmpty(DocumentoIdentidadPlain) ? "null" : DocumentoIdentidadPlain,
                string.IsNullOrEmpty(TipoDocumento) ? "null" : TipoDocumento,
                !Nucleo_NivelEducativo.HasValue ? "null" : Nucleo_NivelEducativo.Value.ToString(),
                !EstatusVerificacion.HasValue ? "null" : EstatusVerificacion.Value.ToString(),
                !EstatusCapacitacion.HasValue ? "null" : EstatusCapacitacion.Value.ToString(),
                !FlagPrimero.HasValue ? "null" : FlagPrimero.Value.ToString(),
                !CedulaError.HasValue ? "null" : CedulaError.Value.ToString(),
                !KitsEnviados.HasValue ? "null" : KitsEnviados.Value.ToString(),
                !KitsEntregados.HasValue ? "null" : KitsEntregados.Value.ToString(),
                !KitAlfabetizadorEntregado.HasValue ? "null" : KitAlfabetizadorEntregado.Value.ToString(),
                !AnimadorAsignado.HasValue ? "null" : AnimadorAsignado.Value.ToString(),
                !AnimadorPropuesto.HasValue ? "null" : AnimadorPropuesto.Value.ToString(),
                string.IsNullOrEmpty(NumeroRegJM) ? "null" : NumeroRegJM,
                string.IsNullOrEmpty(Observaciones) ? "null" : Observaciones,
                string.IsNullOrEmpty(Experiencia) ? "null" : Experiencia,
                string.IsNullOrEmpty(TrabajoActual) ? "null" : TrabajoActual,
                string.IsNullOrEmpty(Provincia) ? "null" : Provincia,
                string.IsNullOrEmpty(Municipio) ? "null" : Municipio,
                string.IsNullOrEmpty(DistritoMunicipal) ? "null" : DistritoMunicipal,
                string.IsNullOrEmpty(Seccion) ? "null" : Seccion,
                string.IsNullOrEmpty(Barrio) ? "null" : Barrio,
                string.IsNullOrEmpty(SubBarrioId) ? "null" : SubBarrioId,
                string.IsNullOrEmpty(Direccion) ? "null" : Direccion,
                Id);

        }


    }
}
