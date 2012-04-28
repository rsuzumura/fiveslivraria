using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using FivesLivraria.Data;

namespace FivesLivraria
{
    public partial class CadastroCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                MembershipCreateStatus s;
                Membership.CreateUser(txtLogin.Text, txtPassword.Text, txtEmailAddress.Text, txtQuestion.Text, txtAnswer.Text, true, out s);
                if (s != MembershipCreateStatus.Success)
                { }//General.ShowError(3, TraduzMensagem(s));
                else
                {
                    Roles.AddUsersToRoles(new string[]{ txtLogin.Text }, new string[] { "cliente" });
                    Usuario u = new Usuario()
                    {
                        nmUsuario   = txtUser.Text,
                        dsEndereco  = txtAddress.Text,
                        dsLogin     = txtLogin.Text
                    };
                    u.Insert();

                    Response.Redirect("Login.aspx", false);
                }
            }
        }
    }
}