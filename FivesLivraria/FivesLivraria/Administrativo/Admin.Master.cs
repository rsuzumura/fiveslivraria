using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace FivesLivraria.Administrativo
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUser.Text = Current.UserName;
            }
        }

        protected void lnkExit_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }

        protected void menu_ItemClick(object sender, EO.Web.NavigationItemEventArgs e)
        {
            if (e.MenuItem.CustomItemID == "Sair")
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Login.aspx", false);
            }
        }

        protected void lnkHelp_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminHelp.htm", false);
        }
    }
}