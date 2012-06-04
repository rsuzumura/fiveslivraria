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
                if (Current.UserId == 0)
                {
                    lkbLogin.Text       = "Entrar";
                    lblUser.Text        = "Visitante";
                    lkbCadastro.Text    = "Cadastrar";
                    lkbCarrinho.Visible = false;
                    separator.Visible   = false;
                }
                else
                {
                    lkbLogin.Text   = "Sair";
                    lblUser.Text    = Current.UserName;
                    if (Page.User.IsInRole("cliente"))
                    {
                        lkbCadastro.Text    = "Visualizar Cadastro";
                        lkbCarrinho.Visible = true;
                        separator.Visible   = true;
                    }
                    else
                    {
                        lkbCadastro.Visible = false;
                        lkbCarrinho.Visible = false;
                        separator.Visible   = false;
                    }
                }
            }
        }

        protected void lkbLogin_Click(object sender, EventArgs e)
        {
            if (Current.UserId == 0)
                Response.Redirect("~/Login.aspx");
            else
            {
                FormsAuthentication.SignOut();
                Current.UserId = 0;
                Current.UserName = string.Empty;
                Response.Redirect("~/Principal.aspx");
            }
        }

        protected void lkbCarrinho_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Carrinho.aspx");
        }

        protected void lkbCadastro_Click(object sender, EventArgs e)
        {
            if (Current.UserId == 0)
                Response.Redirect("~/CadastroCliente.aspx", false);
            else
                Response.Redirect("~/ViewCliente.aspx", false);
        }
    }
}