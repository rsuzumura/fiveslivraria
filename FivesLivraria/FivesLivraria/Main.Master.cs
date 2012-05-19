using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace FivesLivraria
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUser.Text = !string.IsNullOrEmpty(Current.UserName) ? Current.UserName : "Visitante";
            }
        }

        protected void lkbExit_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Current.UserId = 0;
            Current.UserName = string.Empty;
            Response.Redirect("~/Login.aspx");            
        }

        protected void lkbCarrinho_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Carrinho.aspx");
        }
    }
}