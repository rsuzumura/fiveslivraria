using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// bibliotecas específicas do projeto
using FivesLivraria.Data.Classes;

namespace FivesLivraria
{
    public partial class CaixaBalcao : BasePage
    {
       protected static string[,] ProdutosTeste = new string[5,3]
                                 { {"1", "Produto 01", "25,00" },
                                   {"2", "Produto 02", "15,00" },
                                   {"3", "Produto 03", "35,00" },
                                   {"4", "Produto 04", "20,00" },
                                   {"5", "Produto 05", "10,00" } };
       Pedido pedido;
       ItemPedido item;
       bool vendaOk = true;

        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack) 
           {
              vendaOk = ((bool)Session["statusAberturaCaixa"]);
              if ( vendaOk )
              {
                 ViewState["pedidoCaixa"] = new Pedido();
                 ViewState["itemCaixa"] = new ItemPedido();
                 area_Cupom.Visible = true;
                 area_TEF.Visible = true;
              }
              else
              {
                 exibeErroAbertura();
              }
           }
        }

       protected void btnItem_onClick(object sender, EventArgs e)
        {
           if (vendaOk)
           {
              int indice = listProdutosTeste.SelectedIndex;

              int codProd = int.Parse(ProdutosTeste[indice, 0]);
              double vlr = double.Parse(ProdutosTeste[indice, 2]);
              string nome = ProdutosTeste[indice, 1];

              item = (ItemPedido)ViewState["itemCaixa"];
              item.AddProduto(codProd, nome, vlr);

              TableCell cel1 = new TableCell();
              cel1.Text = ProdutosTeste[indice, 0];
              TableCell cel2 = new TableCell();
              cel2.Text = ProdutosTeste[indice, 1];
              TableCell cel3 = new TableCell();
              cel3.Text = ProdutosTeste[indice, 2];

              TableRow linha = new TableRow();
              linha.Cells.Add(cel1);
              linha.Cells.Add(cel2);
              linha.Cells.Add(cel3);

              tbl_Itens.Rows.Add(linha);

                 CupomFiscal cupom = (CupomFiscal) ViewState["cupom"];
                 cupom.item = item;
           }
           else
           {
              exibeErroAbertura();
           }
        }

       protected void btnPedido_onClick(object sender, EventArgs e)
        {
           CupomFiscal cupom = (CupomFiscal) Session["cupom"];
           if (vendaOk)
           {
              pedido = (Pedido)ViewState["pedidoCaixa"];
              item = (ItemPedido)ViewState["itemCaixa"];

              // colando o conteúdo para simulação de cupom
              area_Cupom.Value = cupom.cabecalho();

              if (ListFrmPgto.SelectedIndex != 0) 
              {
                 TEF tef = new TEF();
                 // continuar
              }
              ViewState["itemCaixa"] = new ItemPedido();
              ViewState["pedidoCaixa"] = new Pedido();
           }
           else
           {
              exibeErroAbertura();
           }
        }

       protected void exibeErroAbertura()
       {
          string txtErro = "Caixa não foi aberto, realizar abertura ou aguardar dia seguinte!";
          ShowMessage(MessageType.Error, txtErro, "Erro: Venda");
       }

   }
}