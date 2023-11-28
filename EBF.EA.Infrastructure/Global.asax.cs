using System.Web;
//using System.Web.//Optimization;

namespace QACIglesia.Infrastructure
{
    public class Global : HttpApplication
    {/*
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        void Application_Error(object sender, EventArgs e)
        {

            Exception exc = Server.GetLastError();
      
            if (exc == null) {
                
                Response.Redirect("~/ErrorPage");
            }
            Session["Boom_Error"] = exc;
            Response.AddHeader("Boom", exc.Message);
            Response.AddHeader("Boom-StackTrace", exc.StackTrace);

            var dest = ConfigurationManager.AppSettings["adminEmails"].ToString();
            var destinatarios = new List<string>();

            foreach (string itm in dest.Split(';'))
            {
                destinatarios.Add(itm);
            }
            AvanzaMailKit mailkit = new AvanzaMailKit(destinatarios, "Notificación de Error");
            
            //Email mail = new Email();
            //mail.AgregarDestinatario(destinatarios);

            var filtro = new Dictionary<string, string>();
            Usuario usuario =  null;
            HttpSessionState session = HttpContext.Current.Session;
            if (session != null) {

                usuario = (Usuario)session["usuario"];
            }
           

            filtro.Add("{ERROR_MESSAGE}", exc.Message);
            filtro.Add("{STACK_TRACE}", exc.StackTrace.Replace("\n", "<br/>"));
            filtro.Add("{INNER_EXCEPTION}", exc.InnerException?.Message);
            filtro.Add("{INNER_EXCEPTION_STACK_TRACE}", exc.InnerException?.StackTrace.Replace("\n", "<br/>"));
            filtro.Add("{USER}", (usuario == null? "N/A" : usuario.NombreUsuario));


            try {
                mailkit.ParseBody("ErrorEmail.html",filtro);

                mailkit.Send();
                //mail.SendEmail(filtro, "ErrorEmail.html", "");
            }
            catch (Exception err) {

                Session["Boom_Error"] = err;
                Response.AddHeader("Boom", err.Message);
                Response.AddHeader("Boom-StackTrace", err.StackTrace);
            }
            



            Response.Redirect("~/ErrorPage");

            Server.ClearError();
        }
   */
    }
}