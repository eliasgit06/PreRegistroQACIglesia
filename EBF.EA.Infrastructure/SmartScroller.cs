using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;


namespace QACIglesia.Infrastructure
{
    public class SmartScroller : System.Web.UI.Control
    {
        private HtmlForm m_theForm = new HtmlForm();

        public SmartScroller()
        {

        }

        protected override void Render(HtmlTextWriter writer)
        {
            Page.VerifyRenderingInServerForm(this);
            base.Render(writer);

        }
        public HtmlForm GetServerForm(ControlCollection parent)
        {
            foreach (Control child in parent)
            {
                Type t = child.GetType();
                if (t == typeof(System.Web.UI.HtmlControls.HtmlForm))
                    return (HtmlForm)child;

                if (child.HasControls())
                    return GetServerForm(child.Controls);
            }

            return new HtmlForm();
        }
    }
}
