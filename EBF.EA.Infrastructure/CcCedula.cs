using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Xml;


namespace QACIglesia.Infrastructure
{
    public partial class CcCedula : Component
    {
        public string cedula;
        public string nombres;
        public string apellido1;
        public string apellido2;
        public string sexo;
        public string fechanacimiento;
        public string estadocivil;
        public string estado;
        public string rutafoto;
        public string LugarNacimiento;
        public System.IO.Stream dataStream;

        public CcCedula()
        {
            //InitializeComponent();
        }

        public CcCedula(IContainer container)
        {
            container.Add(this);

            // InitializeComponent();
        }
        public void BuscarCedulaOnline(string strcedula)
        {
            try
            {
                string c1, c2, c3, cedulaplain;

                cedulaplain = strcedula.Replace("-", "");
                c1 = cedulaplain.Substring(0, 3);
                c2 = cedulaplain.Substring(3, 7);
                c3 = cedulaplain.Substring(10, 1);

                string tbServiceID = "***COLOCAR TOKEN DE DE LA JUNTA CENTRAL ELECTORAL**";
                string URLPrefix = "http://dataportal.jce.gob.do";
                string URL = string.Format("{4}/idcons/IndividualDataHandler.aspx?ServiceID={0}&ID1={1}&ID2={2}&ID3={3}", tbServiceID, c1, c2, c3, URLPrefix);
                System.Net.HttpWebRequest objRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                objRequest.Method = "GET";
                //because server protected from the hackers attacks, we have to define UserAgent like web browser
                objRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";

                System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();
                System.IO.Stream dataStream = objResponse.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                string XMLResponse = reader.ReadToEnd();

                reader.Close();
                dataStream.Close();
                objResponse.Close();

                System.Xml.XmlDocument objDoc = new System.Xml.XmlDocument();
                objDoc.LoadXml(XMLResponse);
                //StringBuilder sb = new StringBuilder();

                foreach (System.Xml.XmlElement objElement in objDoc.DocumentElement.ChildNodes)
                {
                    // sb.AppendFormat("{0} = {1}\r\n", objElement.Name, objElement.InnerText);

                    if (objElement.Name.ToLowerInvariant() == "nombres" && (objElement.Name.ToLowerInvariant() != " ")) { this.nombres = string.Format("{0}", objElement.InnerText); }
                    if (objElement.Name.ToLowerInvariant() == "apellido1" && (objElement.Name.ToLowerInvariant() != " ")) { this.apellido1 = string.Format("{0}", objElement.InnerText); }
                    if (objElement.Name.ToLowerInvariant() == "apellido2" && (objElement.Name.ToLowerInvariant() != " ")) { this.apellido2 = string.Format("{0}", objElement.InnerText); }
                    if (objElement.Name.ToLowerInvariant() == "apellidoconyuge" && (objElement.Name.ToLowerInvariant() != " ")) { this.apellido2 = string.Format("{0}", objElement.InnerText); }
                    if (objElement.Name.ToLowerInvariant() == "fecha_nac" && (objElement.Name.ToLowerInvariant() != " ")) { this.fechanacimiento = string.Format("{0}", objElement.InnerText); }
                    if (objElement.Name.ToLowerInvariant() == "lugar_nac" && (objElement.Name.ToLowerInvariant() != " ")) { this.LugarNacimiento = string.Format("{0}", objElement.InnerText); }
                    if (objElement.Name.ToLowerInvariant() == "ced_a_sexo" && (objElement.Name.ToLowerInvariant() != " ")) { this.sexo = string.Format("{0}", objElement.InnerText); }
                    if (objElement.Name.ToLowerInvariant() == "est_civil" && (objElement.Name.ToLowerInvariant() != " ")) { this.estadocivil = string.Format("{0}", objElement.InnerText); }
                    if (objElement.Name.ToLowerInvariant() == "estatus" && (objElement.Name.ToLowerInvariant() != " ")) { this.estado = string.Format("{0}", objElement.InnerText); }

                    if (objElement.Name.ToLowerInvariant() == "fotourl")
                    {
                        string FotoURL = string.Format("{0}{1}", URLPrefix, objElement.InnerText);
                        objRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FotoURL);
                        objRequest.Method = "GET";
                        //because server protected from the hackers attacks, we have to define UserAgent like web browser
                        objRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                        objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();
                        dataStream = objResponse.GetResponseStream();

                        this.rutafoto = objResponse.ResponseUri.ToString();
                        this.dataStream = dataStream;

                    }
                }
            }
            catch (Exception exe)
            {
                //MessageBox.Show(" No se pudo establecer conexión con la JCE, favor intentelo mas tarde",exe.Message);
                System.Console.WriteLine(exe.Message);
            }


        }

        public void BuscarCedulaOnline1(string strcedula)
        {
            try
            {
                string cedulaplain;
                cedulaplain = strcedula.Replace("-", "");

                SqlConnection cnx = new SqlConnection("Data Source=h7dxy4lo9g.database.windows.net;Initial Catalog=CedulasDB;User ID=qacusuario;Password=digepep2016@");
                SqlCommand comando = new SqlCommand("select nombres,papellido,sapellido,nacimiento,sexo from Padron where cedula='" + cedulaplain + "'", cnx);
                cnx.Open();
                SqlDataReader leer = comando.ExecuteReader();

                if (leer.Read())
                {
                    if (leer.GetValue(0) == System.DBNull.Value) { this.nombres = ""; } else { this.nombres = leer.GetValue(0).ToString(); }
                    if (leer.GetValue(1) == System.DBNull.Value) { this.apellido1 = ""; } else { this.apellido1 = leer.GetValue(1).ToString(); }
                    if (leer.GetValue(2) == System.DBNull.Value) { this.apellido2 = ""; } else { this.apellido2 = string.Format("{0}", (string)leer.GetValue(2)); }
                    if (leer.GetValue(3) == System.DBNull.Value) { this.fechanacimiento = ""; } else { this.fechanacimiento = leer.GetValue(3).ToString(); }
                    if (leer.GetValue(4) == System.DBNull.Value) { this.sexo = ""; } else { this.sexo = leer.GetValue(4).ToString(); }

                    this.LugarNacimiento = "";
                    this.estadocivil = "";
                    this.estado = "";
                    this.rutafoto = null;
                }
                cnx.Close();
            }
            catch (Exception exe)
            {
                //MessageBox.Show(" No se pudo establecer conexión con la JCE, favor intentelo mas tarde",exe.Message);
                System.Console.WriteLine(exe.Message);
            }


        }

        public void BuscarCedulaOnline2(string strcedula)
        {
            //Si no se especifica una cedula no se continua con el proceso;
            if (string.IsNullOrEmpty(strcedula)) { return; }
            if ((strcedula.Length != 13) && (strcedula.Length != 11)) { return; }
            try
            {
                string c1, c2, c3, cedulaplain, campo = "";

                cedulaplain = strcedula.Replace("-", "");
                c1 = cedulaplain.Substring(0, 3);
                c2 = cedulaplain.Substring(3, 7);
                c3 = cedulaplain.Substring(10, 1);

                String URLString = "http://dataportal.jce.gob.do/idcons//IndividualDataHandler.aspx?ServiceID=cf257911-87f4-4648-aa6c-fdb2c095f86c&ID1=" + c1 + "&ID2=" + c2 + "&ID3=" + c3 + "";

                XmlTextReader reader = new XmlTextReader(URLString);

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // El nodo es un elemento.
                            while (reader.MoveToNextAttribute()) // Lee los atributos.
                                Console.Write(" " + reader.Name + "='" + reader.Value + "'");
                            campo = reader.Name;
                            break;
                        case XmlNodeType.Text: //Muestra el texto en cada elemento.
                            if ((campo == "nombres") && (reader.Value != " ")) this.nombres = reader.Value;
                            if ((campo == "apellido1") && (reader.Value != " ")) this.apellido1 = reader.Value;
                            if ((campo == "apellido2") && (reader.Value != " ")) this.apellido2 = reader.Value;
                            if ((campo == "apellidoconyuge") && (reader.Value != " ")) this.apellido2 = reader.Value;
                            if ((campo == "fecha_nac") && (reader.Value != " ")) this.fechanacimiento = reader.Value;
                            if ((campo == "lugar_nac") && (reader.Value != " ")) this.LugarNacimiento = reader.Value;
                            if ((campo == "ced_a_sexo") && (reader.Value != " ")) this.sexo = reader.Value;
                            if ((campo == "est_civil") && (reader.Value != " ")) this.estadocivil = reader.Value;
                            if ((campo == "estatus") && (reader.Value != " ")) this.estado = reader.Value;
                            if ((campo == "fotourl") && (reader.Value != " ")) this.rutafoto = "http://dataportal.jce.gob.do/" + reader.Value;
                            break;
                        case XmlNodeType.EndElement: //Muestra el final del elemento.
                            break;
                    }

                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(" No se pudo establecer conexión con la JCE, favor intentelo mas tarde");
                System.Console.WriteLine(ex.Message);
            }

        }
    }
}
