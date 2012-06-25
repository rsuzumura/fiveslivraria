using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace FivesLivraria
{
    public partial class ChangePassword : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("cadastro");
            if (Page.IsValid)
            {
                MembershipUser mu = Membership.GetUser();
                if (!mu.ChangePasswordQuestionAndAnswer(txtPassword.Text, txtQuestion.Text, txtAnswer.Text))
                    ShowMessage(MessageType.Warning, "Senha atual incorreta.", "Atenção");
                else if (!mu.ChangePassword(txtPassword.Text, txtNewPassword.Text))
                        ShowMessage(MessageType.Warning, "Senha atual incorreta.", "Atenção");
                else
                    Response.Redirect("~/ViewCliente.aspx", false);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ViewCliente.aspx", false);
        }
    }
}