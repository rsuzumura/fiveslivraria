using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using FivesLivraria.Data;

namespace FivesLivraria.Administrativo
{
    public partial class CadastroUsuario : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillControlWithoutNull(dropRoles, Roles.GetAllRoles());
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx", false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                try
                {
                    MembershipCreateStatus s;
                    Membership.CreateUser(txtLogin.Text, txtPassword.Text, txtEmailAddress.Text, txtQuestion.Text, txtAnswer.Text, true, out s);
                    if (s != MembershipCreateStatus.Success)
                        ShowMessage(MessageType.Error, TraduzMensagem(s), "Erro na criação do usuário");
                    else
                    {
                        Roles.AddUsersToRoles(new string[] { txtLogin.Text }, new string[] { dropRoles.SelectedValue });
                        Usuario u = new Usuario()
                        {
                            nmUsuario = txtUser.Text,
                            dsEndereco = txtAddress.Text,
                            dsLogin = txtLogin.Text
                        };
                        u.Insert();

                        Response.Redirect("Usuarios.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage(MessageType.Error, string.Concat("Atenção: erro no cadastro do usuário: ", ex.Message), "Erro na criação do usuário");
                }
            }
        }
    }
}