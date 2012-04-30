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
    public partial class LostPassword : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["user"]))
                throw new Exception("Nenhum usuário foi informado.");
            if (!IsPostBack)
            {
                MembershipUser mu   = Membership.GetUser(Request.QueryString["user"]);
                ltQuestion.Text     = mu.PasswordQuestion;
            }
        }

        protected void btnRequestPassword_Click(object sender, EventArgs e)
        {
            MembershipUser mu = Membership.GetUser(Request.QueryString["user"]);
            if (mu != null)
            {
                try
                {
                    string password = mu.GetPassword(txtAnswer.Text);
                    Chilkat.Email msg = new Chilkat.Email();
                    msg.From    = General.emailSettings["address"];
                    msg.Subject = "Confirmação de Email";

                    //endereços
                    Usuario u = Usuario.Get(mu.UserName);
                    msg.AddTo(u.nmUsuario.Value, mu.Email);
                    msg.Body = string.Format("Sua senha é {0}", password);

                    Chilkat.MailMan obox = new Chilkat.MailMan();
                    obox.UnlockComponent("SOrbiumMAILQ_dB8Ry3mhLqlg");

                    //login
                    obox.SmtpHost       = General.emailSettings["host"];
                    obox.SmtpPort       = Convert.ToInt32(General.emailSettings["port"]);
                    obox.SmtpPassword   = General.emailSettings["password"];
                    obox.SmtpUsername   = General.emailSettings["username"];

                    //envia
                    if (!obox.VerifySmtpConnection())
                        throw new Exception("Não foi possível efetuar a conexão SMTP.");

                    if (!obox.VerifySmtpLogin())
                        throw new Exception("O login do usuário está incorreto.");

                    if (obox.SendEmail(msg))
                    {

                    }
                    else
                        throw new Exception("O email não pôde ser enviado, tente novamente.");
                }
                catch (Exception ex)
                {
                    this.ShowMessage(MessageType.Error, ex.Message, "Erro");
                }
            }
        }
    }
}