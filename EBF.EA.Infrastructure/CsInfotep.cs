using System;
using System.Data;
using System.Data.SqlClient;
namespace QACIglesia.Infrastructure
{
    public class CsInfotep
    {
        public string codcurso = "";
        public string documento = "";
        public string nombres = "";
        public string apellido1 = "";
        public string apellido2 = "";
        public string sexo = "";
        public string fechanacimiento = "";
        public string nivel_educacion = "";
        public string ID_MUNICIPIO_CURSO = "";
        public string MUNICIPIO_CURSO = "";
        public string ID_MUNICIPIO_PARTICIPANTE = "";
        public string MUNICIPIO_PARTICIPANTE = "";
        public string PROVINCIA_PARTICIPANTE = "";
        public string ID_PROVICINCIA_PARTICIPANTE = "";
        public string FECHA_INICIO_REAL = "";
        public string FECHA_TERMINO_REAL = "";
        public string LOCAL_IMPARTE = "";
        public string REGIONAL = "";
        public string SALIDA = "";

        ServiceReferenceInfotep.CertificadosWSSoapClient ServicioInfotep = new ServiceReferenceInfotep.CertificadosWSSoapClient();
        DataTable DatosInfotep = new DataTable();

        public void BuscarCedula(string strcedula)
        {
            try
            {
                //Si no se especifica una cedula no se continua con el proceso;
                if (string.IsNullOrEmpty(strcedula)) { return; }
                if ((strcedula.Length != 13) && (strcedula.Length != 11)) { return; }

                strcedula = strcedula.Replace("-", "");

                //ServiceReference3.CertificadosWSSoapClient ServicioInfotep = new ServiceReference3.CertificadosWSSoapClient();
                //DataTable DatosInfotep = new DataTable();
                
                //Coneccion al web services proporcionado por INFOTEP para verificar si una cedula 
                //proporcionada se encuentra en su base de datos
                DatosInfotep = ServicioInfotep.CertificadosParticipanteDocumento("digepep", "d00a912c-0225-4ce5-a93c-c4e195429578", strcedula);

                for (int i = 0; i < DatosInfotep.Rows.Count; i++)
                {
                    codcurso = DatosInfotep.Rows[i]["COD_CURSO"].ToString();
                    nombres = DatosInfotep.Rows[i]["NOMBRES"].ToString();
                    apellido1 = DatosInfotep.Rows[i]["APELLIDOS"].ToString();
                    apellido2 = "";
                    sexo = DatosInfotep.Rows[i]["SEXO"].ToString();
                    fechanacimiento = DatosInfotep.Rows[i]["FECHA_NACIMIENTO"].ToString();
                    nivel_educacion = DatosInfotep.Rows[i]["NIVEL_EDUCACION"].ToString();
                    ID_MUNICIPIO_CURSO = DatosInfotep.Rows[i]["ID_MUNICIPIO_CURSO"].ToString();
                    MUNICIPIO_CURSO = DatosInfotep.Rows[i]["MUNICIPIO_CURSO"].ToString();
                    ID_MUNICIPIO_PARTICIPANTE = DatosInfotep.Rows[i]["ID_MUNICIPIO_PARTICIPANTE"].ToString();
                    MUNICIPIO_PARTICIPANTE = DatosInfotep.Rows[i]["MUNICIPIO_PARTICIPANTE"].ToString();
                    PROVINCIA_PARTICIPANTE = DatosInfotep.Rows[i]["PROVINCIA_PARTICIPANTE"].ToString();
                    ID_PROVICINCIA_PARTICIPANTE = DatosInfotep.Rows[i]["ID_PROVICINCIA_PARTICIPANTE"].ToString();
                    FECHA_INICIO_REAL = DatosInfotep.Rows[i]["FECHA_INICIO_REAL"].ToString();
                    FECHA_TERMINO_REAL = DatosInfotep.Rows[i]["FECHA_TERMINO_REAL"].ToString();
                    LOCAL_IMPARTE = DatosInfotep.Rows[i]["LOCAL_IMPARTE"].ToString();
                    REGIONAL = DatosInfotep.Rows[i]["REGIONAL"].ToString();
                    SALIDA = DatosInfotep.Rows[i]["SALIDA"].ToString();
                }

            }
            catch (Exception ex)
            {
                this.nombres = ex.Message.Substring(1, 25);
            }

        }
        public void BuscarCedula2(string strcedula)
        {
            try
            {
                //Si no se especifica una cedula no se continua con el proceso;
                if (string.IsNullOrEmpty(strcedula)) { return; }
                if ((strcedula.Length != 13) && (strcedula.Length != 11)) { return; }

                strcedula = strcedula.Replace("-", "");

                SqlConnection cnx = new SqlConnection("Data Source=h7dxy4lo9g.database.windows.net;Initial Catalog=QACPRODB;User ID=qacusuario;Password=digepep2016@");
                SqlCommand comando = new SqlCommand("select TOP 1 NO_DOCUMENTO,PARTICIPANTE,SEXO,FECHA_NACIMIENTO,LOCALIDAD,CIUDAD,OCUPACION_SALIDA,NIVEL_EDUCACION,FECHA_INICIO_REAL,FECHA_TERMINO_REAL FROM infotep WHERE no_documento is not null AND no_documento='" + strcedula + "' ORDER BY FECHA_TERMINO_REAL DESC", cnx);
                cnx.Open();
                SqlDataReader leer = comando.ExecuteReader();

                if (leer.Read())
                {
                    codcurso = "";
                    documento = (string)leer.GetValue(leer.GetOrdinal("NO_DOCUMENTO"));
                    nombres = (string)leer.GetValue(leer.GetOrdinal("PARTICIPANTE"));
                    apellido1 = "";
                    apellido2 = "";
                    sexo = (string)leer.GetValue(leer.GetOrdinal("SEXO"));
                    fechanacimiento = leer.GetValue(leer.GetOrdinal("FECHA_NACIMIENTO")).ToString();
                    nivel_educacion = (string)leer.GetValue(leer.GetOrdinal("NIVEL_EDUCACION"));
                    ID_MUNICIPIO_CURSO = "";
                    MUNICIPIO_CURSO = "";
                    ID_MUNICIPIO_PARTICIPANTE = "";
                    MUNICIPIO_PARTICIPANTE = "";
                    PROVINCIA_PARTICIPANTE = "";
                    ID_PROVICINCIA_PARTICIPANTE = "";
                    FECHA_INICIO_REAL = leer.GetValue(leer.GetOrdinal("FECHA_INICIO_REAL")).ToString();
                    FECHA_TERMINO_REAL = leer.GetValue(leer.GetOrdinal("FECHA_TERMINO_REAL")).ToString();
                    LOCAL_IMPARTE = "";
                    REGIONAL = "";
                    SALIDA = (string)leer.GetValue(leer.GetOrdinal("OCUPACION_SALIDA"));
                }

            }
            catch (Exception ex)
            {
                this.nombres = ex.Message.Substring(1, 25);
            }
        }
        public DataTable BuscarPorNombres(string nombres, string apellidos)
        {
            try
            {
                DatosInfotep = ServicioInfotep.CertificadosParticipanteNombre("digepep", "d00a912c-0225-4ce5-a93c-c4e195429578", nombres, apellidos);
                return DatosInfotep;
            }
            catch (Exception ex)
            {
                this.nombres = ex.Message.Substring(1, 33);
                return null;
            }

        }

    }
}