using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data.Classes;
using System.IO;
using FivesLivraria.Data;

namespace FivesLivraria
{
    public partial class CaixaAcoes : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnAbrirCaixa_onClick(object sender, EventArgs e)
        {
            CupomFiscal cupom = null;
            #region Carregamento da abertura de caixa
            SchemaManager sm = new SchemaManager();
            CaixaAcesso ca = null;
            if (File.Exists(Server.MapPath("/") + "acesso.xml"))
                ca = sm.LoadFile(Server.MapPath("/"), "acesso.xml");

            if (ca != null)
            {
                if (ca.Exists(Current.UserId, DateTime.Now))
                    ShowMessage(MessageType.Warning, "O caixa já foi aberto por este usuário.", "Caixa Aberto");
                else
                {
                    ca.acessos.Add(new Acesso() { dataAcesso = DateTime.Now, userId = Current.UserId, userName = Current.UserName });
                    cupom = new CupomFiscal();
                    Session["cupom"] = cupom;
                    ShowMessage(MessageType.Info, string.Format("Caixa aberto pelo usuário {0}", Current.UserName), "Caixa Aberto");
                }
            }
            else
            {
                ca = new CaixaAcesso();
                ca.acessos = new Acessos();
                ca.acessos.Add(new Acesso() { dataAcesso = DateTime.Now, userId = Current.UserId, userName = Current.UserName });
                cupom = new CupomFiscal();
                Session["cupom"] = cupom;
                ShowMessage(MessageType.Info, string.Format("Caixa aberto pelo usuário {0}", Current.UserName), "Caixa Aberto");
            }
            sm.SaveFile("acesso.xml", ca);
            #endregion
        }

        protected void btnReducao_onClick(object sender, EventArgs e)
        {
            bool statusFechamento = (bool)Session["statusFechamentoCaixa"];

            if (!statusFechamento)
            {
                statusFechamento = true;
                Session["statusFechamentoCaixa"] = statusFechamento;
                // -----------------------------------------------------
                // chamar reducaoZ
                // que utilizará o objeto cupom na Session para 
                // recuperar os valores ocorridos durante as transações
                // -----------------------------------------------------
            }
        }

        protected void btnLeitura_onClick(object sender, EventArgs e)
        {
            // -----------------------------------------------------
            // chamar leituraX
            // que utilizará o objeto cupom na Session para 
            // recuperar os valores ocorridos durante as transações
            // -----------------------------------------------------
            // CupomFiscal cupom = (CupomFiscal)Session["cupom"];
            // cupom.LeituraX();
        }

        protected void btnHistorico_onClick(object sender, EventArgs e)
        {
            // -----------------------------------------------------
            // Decidindo como montar
            //   deixar a livre escolha de datas e consulta a 
            //   existência de registro ou carregar a informações 
            //   e já exibi-las
            // outro ponto a decidir... gravar em banco ou deixar em 
            //      arquivos?
            // -----------------------------------------------------
        }
    }
}