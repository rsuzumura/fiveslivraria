using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace FivesLivraria
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Current.UserId = 0;
                FormsAuthentication.SignOut();
            }
            Login1.FindControl("UserName").Focus();
            //-- regra para senha do tipo hashed, não é possível recuperar
            ((LinkButton)Login1.FindControl("LostPassword")).Visible = (Membership.Provider.PasswordFormat != MembershipPasswordFormat.Hashed);
        }

        protected void btnClientRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroCliente.aspx", false);
        }

        protected void LostPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("LostPassword.aspx?user={0}", Login1.UserName), false);
        }
    }
}