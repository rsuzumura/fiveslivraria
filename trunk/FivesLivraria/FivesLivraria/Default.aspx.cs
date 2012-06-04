using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data;
using System.IO;

namespace FivesLivraria
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario u = Usuario.Get(User.Identity.Name);
                if (u != null)
                {
                    Current.UserId      = u.idUsuario.Value;
                    Current.UserName    = u.nmUsuario.Value;
                    //Response.Redirect("FinalizarCompra.aspx");
                    if (User.IsInRole("gestor"))
                    {
                        CaixaAcesso ca = null;
                        SchemaManager sm = new SchemaManager();
                        if (File.Exists(Server.MapPath("/") + "acesso.xml"))
                        {
                            ca = sm.LoadFile(Server.MapPath("/"), "acesso.xml");
                            Current.AcessoCaixa = Acesso.Get(ca, Current.UserId);
                        }
                        Response.Redirect("~/Administrativo/Default.aspx", false);
                    }
                    else if (User.IsInRole("usuario"))
                    {
                        CaixaAcesso ca = null;
                        SchemaManager sm = new SchemaManager();
                        if (File.Exists(Server.MapPath("/") + "acesso.xml"))
                        {
                            ca = sm.LoadFile(Server.MapPath("/"), "acesso.xml");
                            Current.AcessoCaixa = Acesso.Get(ca, Current.UserId);
                        }
                        Response.Redirect("CaixaOpcoes.aspx", false);
                    }
                    else
                        Response.Redirect("Principal.aspx", false);
                }
                else
                {
                    Current.UserId = 0;
                    Current.UserName = string.Empty;
                    Response.Redirect("Principal.aspx", false);
                }
            }
        }
    }
}