using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace QACIglesia.Infrastructure
{
    public class csUsuario
    {
        public string usuario { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string funcion { get; set; } //P-Provincial,M-Municipal N-Enlace
        public string referencia { get; set; }
        public string provinciaid { get; set; }
        public string provincia { get; set; }
        public string municipioid { get; set; }
        public string municipio { get; set; }
        public byte[] LlaveClave = Encoding.ASCII.GetBytes("Educacion Adulto");
        public byte[] IV = Encoding.ASCII.GetBytes("ParaElFuturo@2013");
        public string CorreoElectronico { get; set; }
        private string connectionString;
        public string organizacionID { get; set; }
        public string verReporteOrg { get; set; }

        public csUsuario()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["VOLUNTARIADODB_PRODUCCION"].ConnectionString;
        }
        public csUsuario(string connString)
        {
            connectionString = connString;
        }

        public bool Autenticar(string usuario, string clave)
        {
            string ClaveEncriptada = "";
            ClaveEncriptada = this.Encripta(clave);

            // string strsql = "select b.provinciaid,b.provincia,b.municipioid,b.municipio,a.nombres,concat(a.papellido,' ',a.sapellido) as apellidos,b.referencia,b.funcion,c.username,c.Userclave,a.CorreoElectronico";
            // strsql = strsql + " from personas as a left join coordinadores as b on a.id = b.coordinador_persona left join usuarios as c on a.usuario = c.UserName where c.UserName=@usuario and c.UserClave=@clave";

            string strsql = "select provinciaid,provincia,municipioid,municipio,nombres,concat(papellido,' ',sapellido) as apellidos,referencia,funcion,username,Userclave,CorreoElectronico, OrganizacionID, VerReporteOrg";
            strsql = strsql + " from usuarios where UserName=@usuario and UserClave=@clave";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(strsql, conn);
                command.Parameters.AddWithValue("@usuario", usuario);
                command.Parameters.AddWithValue("@clave", ClaveEncriptada);

                SqlDataReader datos = command.ExecuteReader();

                if (datos.Read())
                {
                    try
                    {
                        //Cedula encontrada
                        this.provinciaid = datos.IsDBNull(0) ? null : datos.GetValue(0).ToString();
                        this.provincia = datos.IsDBNull(1) ? null : datos.GetValue(1).ToString();
                        this.municipioid = datos.IsDBNull(2) ? null : datos.GetValue(2).ToString();
                        this.municipio = datos.IsDBNull(3) ? null : datos.GetValue(3).ToString();
                        this.nombre = datos.IsDBNull(4) ? null : datos.GetValue(4).ToString();
                        this.apellidos = datos.IsDBNull(5) ? null : datos.GetValue(5).ToString();
                        this.referencia = datos.IsDBNull(6) ? null : datos.GetValue(6).ToString();
                        this.funcion = datos.IsDBNull(7) ? null : datos.GetValue(7).ToString();
                        this.usuario = datos.IsDBNull(8) ? null : datos.GetValue(8).ToString();
                        this.clave = datos.IsDBNull(9) ? null : datos.GetValue(9).ToString();
                        this.CorreoElectronico = datos.IsDBNull(10) ? null : datos.GetValue(10).ToString();
                        this.organizacionID = datos.IsDBNull(11) ? null : datos.GetValue(11).ToString();
                        this.verReporteOrg = datos.IsDBNull(12) ? null : datos.GetValue(12).ToString();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        //Metodo simetrica con MD5, especificamente el algoritmo de rijndael .
        public string Encripta(string Cadena)
        {

            byte[] inputBytes = Encoding.ASCII.GetBytes(Cadena);
            byte[] encripted;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes.Length))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(LlaveClave, IV), CryptoStreamMode.Write))
                {
                    objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    objCryptoStream.FlushFinalBlock();
                    objCryptoStream.Close();
                }
                encripted = ms.ToArray();
            }
            return Convert.ToBase64String(encripted);
        }

        public string Desencripta(string Cadena)
        {
            byte[] inputBytes = Convert.FromBase64String(Cadena);
            byte[] resultBytes = new byte[inputBytes.Length];
            string textoLimpio = String.Empty;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(LlaveClave, IV), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(objCryptoStream, true))
                    {
                        textoLimpio = sr.ReadToEnd();
                    }
                }
            }
            return textoLimpio;
        }
        public bool CambiarClave(string usuario, string claveanterior, string nuevaclave)
        {
            bool ejecutado = false;
            string ClaViejaEncriptada, ClaveNuevaEncriptada = "";
            ClaViejaEncriptada = this.Encripta(claveanterior.Replace("-", ""));
            ClaveNuevaEncriptada = this.Encripta(nuevaclave);

            string strsql = "Update usuarios SET userclave=@clavenueva WHERE username=@usuario and userclave=@clavevieja";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(strsql, conn);

                command.Parameters.AddWithValue("@usuario", usuario);
                command.Parameters.AddWithValue("@clavevieja", ClaViejaEncriptada);
                command.Parameters.AddWithValue("@clavenueva", ClaveNuevaEncriptada);
                int nfilas = command.ExecuteNonQuery();

                if (nfilas > 0) { ejecutado = true; }

            }

            return ejecutado;
        }

        public void EncriptarTodasClaves()
        {

            string strsql = "select c.username,c.userclave from personas as a left join coordinadores as b on a.id = b.coordinador_persona left join usuarios as c on a.usuario = c.UserName where c.username is not null and b.provinciaid is not null and b.municipioid is not null and a.documento is not null and username in ('digitador1','digitador20') order by usuario ";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(strsql, conn);

                SqlDataReader ListaUsuarios = command.ExecuteReader();
                csUsuario Usuarios = new csUsuario(connectionString);
                string usuario = "";
                string clave = "";

                while (ListaUsuarios.Read())
                {
                    usuario = (string)ListaUsuarios.GetValue(0).ToString();
                    clave = ListaUsuarios.GetValue(1).ToString();
                    this.CambiarClave(usuario, clave, this.Encripta(clave));
                }
            }

        }

        public bool TraerUsuarioPorNombreDeUsuario(string usuario)
        {

            // string strsql = "select b.provinciaid,b.provincia,b.municipioid,b.municipio,a.nombres,concat(a.papellido,' ',a.sapellido) as apellidos,b.referencia,b.funcion,c.username,c.Userclave,a.CorreoElectronico";
            // strsql = strsql + " from personas as a left join coordinadores as b on a.id = b.coordinador_persona left join usuarios as c on a.usuario = c.UserName where c.UserName=@usuario and c.UserClave=@clave";

            string strsql = "select provinciaid,provincia,municipioid,municipio,nombres,concat(papellido,' ',sapellido) as apellidos,referencia,funcion,username,Userclave,CorreoElectronico,OrganizacionID, VerReporteOrg";
            strsql = strsql + " from usuarios where UserName=@usuario";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(strsql, conn);
                command.Parameters.AddWithValue("@usuario", usuario);

                SqlDataReader datos = command.ExecuteReader();

                if (datos.Read())
                {
                    try
                    {
                        //Cedula encontrada
                        this.provinciaid = datos.IsDBNull(0) ? null : datos.GetValue(0).ToString();
                        this.provincia = datos.IsDBNull(1) ? null : datos.GetValue(1).ToString();
                        this.municipioid = datos.IsDBNull(2) ? null : datos.GetValue(2).ToString();
                        this.municipio = datos.IsDBNull(3) ? null : datos.GetValue(3).ToString();
                        this.nombre = datos.IsDBNull(4) ? null : datos.GetValue(4).ToString();
                        this.apellidos = datos.IsDBNull(5) ? null : datos.GetValue(5).ToString();
                        this.referencia = datos.IsDBNull(6) ? null : datos.GetValue(6).ToString();
                        this.funcion = datos.IsDBNull(7) ? null : datos.GetValue(7).ToString();
                        this.usuario = datos.IsDBNull(8) ? null : datos.GetValue(8).ToString();
                        this.clave = datos.IsDBNull(9) ? null : datos.GetValue(9).ToString();
                        this.CorreoElectronico = datos.IsDBNull(10) ? null : datos.GetValue(10).ToString();
                        this.organizacionID = datos.IsDBNull(11) ? null : datos.GetValue(11).ToString();
                        this.verReporteOrg = datos.IsDBNull(12) ? null : datos.GetValue(12).ToString();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        public bool UsuarioExiste(string usuario)
        {
            bool existe = false;

            string strsql = "SELECT b.provinciaid,b.provincia,b.municipioid,b.municipio,a.nombres,concat(a.papellido,' ',a.sapellido) as apellidos,b.referencia,b.funcion,c.username,c.Userclave,a.CorreoElectronico,OrganizacionID, VerReporteOrg";
            strsql = strsql + " FROM personas as a left join coordinadores as b on a.id = b.coordinador_persona left join usuarios as c on a.usuario = c.UserName where c.UserName=@usuario";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(strsql, conn);

                command.Parameters.AddWithValue("@usuario", usuario);

                int nfilas = command.ExecuteNonQuery();

                if (nfilas > 0) { existe = true; }

            }

            return existe;
        }
        public bool CrearUsuario(string documento, string nombre, string papellido, string sapellido, string provinciaid, string provincia, string municipioid, string municipio, string tipo, string email, string telresidencia, string telmovil, string direccion)
        {
            bool creado = false;
            string nombreusuario = "";
            nombreusuario = nombre.Substring(0, 2) + papellido;
            if (UsuarioExiste(nombreusuario) == false)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //string strsql = "EXEC stp_Crear_Usuario '"+documento+"','"+nombre+"','"+papellido+"','"+sapellido+"','"+provinciaid+"','"+provincia+"','"+municipioid+"','"+municipio+"','"+tipo+"','"+email+"','"+telresidencia+"','"+telmovil+"','"+direccion+"'";
                    string strsql = "EXEC stp_Crear_Usuario @documento,@nombre,@papellido,@sapellido,@provinciaid,@provincia,@municipioid,@municipio,@tipo,@email,@telresidencia,@telmovil,@direccion";
                    conn.Open();
                    SqlCommand command = new SqlCommand(strsql, conn);

                    command.Parameters.AddWithValue("@documento", documento);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@papellido", papellido);
                    command.Parameters.AddWithValue("@sapellido", sapellido);
                    command.Parameters.AddWithValue("@provinciaid", provinciaid);
                    command.Parameters.AddWithValue("@provincia", provincia);
                    command.Parameters.AddWithValue("@municipioid", municipioid);
                    command.Parameters.AddWithValue("@municipio", municipio);
                    command.Parameters.AddWithValue("@tipo", tipo);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@telresidencia", telresidencia);
                    command.Parameters.AddWithValue("@telmovil", telmovil);
                    command.Parameters.AddWithValue("@direccion", direccion);

                    int nfilas = command.ExecuteNonQuery();
                    //si se creo el usuario se procede asingar a la proviedad usuario y clave el usuario creado , asi como tambien actualizar la clave encriptada en la base de datos
                    if (nfilas > 0)
                    {
                        creado = true;
                        this.usuario = nombreusuario; this.clave = documento.Replace("-", "");

                        string strsql2 = "Update usuarios SET userclave=@clavenueva WHERE username=@usuario and userclave=@clavevieja";
                        using (SqlConnection conn2 = new SqlConnection(connectionString))
                        {
                            conn2.Open();
                            SqlCommand command2 = new SqlCommand(strsql2, conn2);

                            command2.Parameters.AddWithValue("@usuario", this.usuario);
                            command2.Parameters.AddWithValue("@clavevieja", this.clave);
                            command2.Parameters.AddWithValue("@clavenueva", this.Encripta(this.clave));
                            int nfilas2 = command2.ExecuteNonQuery();

                        }
                        if (nfilas > 0) { creado = true; }
                    }
                    else
                    {
                        this.usuario = "";
                    }
                }
            }
            return creado;
        }
    }
}