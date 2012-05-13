using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data.Classes;

namespace FivesLivraria
{
   public partial class CaixaAcoes : BasePage
   {
      protected void Page_Load(object sender, EventArgs e)
      {
      }

      protected void btnAbrirCaixa_onClick(object sender, EventArgs e)
      {
         bool statusAbertura = false;
         bool statusFechamento = false;

         statusAbertura = (bool) Session["statusAberturaCaixa"];
         statusFechamento = (bool) Session["statusFechamentoCaixa"];

         if (!statusAbertura && !statusFechamento)
         {
            statusAbertura = true;
            Session["statusAbertura"] = statusAbertura;
            CupomFiscal cupom = new CupomFiscal();
            Session["cupom"] = cupom;
         }
         else 
         {
            string txtErro = "Caixa não pode ser re-aberto";
            ShowMessage(MessageType.Error, txtErro, "Erro no Caixa");
         }

      }

      protected void btnReducao_onClick(object sender, EventArgs e)
      {
         bool statusFechamento = (bool) Session["statusFechamentoCaixa"];

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