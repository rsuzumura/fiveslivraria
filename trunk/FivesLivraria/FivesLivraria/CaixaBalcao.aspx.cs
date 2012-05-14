using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

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
//              vendaOk = ((bool)Session["statusAberturaCaixa"]);  // remover
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

                 CupomFiscal cupom = (CupomFiscal) Session["cupom"];
                 if (cupom == null)
                 {
                    ShowMessage(MessageType.Error, "Problemas na geração do cupom", "Erro: Cupom");
                    cupom = new CupomFiscal();  // remover
                 }

                  cupom.setItem(item);
               
           }
           else
           {
              exibeErroAbertura();
           }
        }

       protected void btnPedido_onClick(object sender, EventArgs e)
        {
           bool pagamento = false;
           CupomFiscal cupom = (CupomFiscal) Session["cupom"];
           if (vendaOk)
           {
              pedido = (Pedido)ViewState["pedidoCaixa"];
              item = (ItemPedido)ViewState["itemCaixa"];

              // colando o conteúdo para simulação de cupom
              cupom.setItem(item);
              cupom.setCliente(long.Parse(box_CPFCliente.Text));
              cupom.gerarCodigo();
              area_Cupom.Value = cupom.imprimeCupom();

              if (ListFrmPgto.SelectedIndex != 0)
              {
                 long cardNumber = long.Parse(txtNumCartao.Text);
                 int cardId = int.Parse(txtCodCartao.Text);
                 TEF tef = new TEF() {
                        id = ListFrmPgto.SelectedIndex,
                        numeroCartao = cardNumber,
                        codigoCartao = cardId,
                        valorTransacao = cupom.item.totalizarItens(),
                        codigoCupom = cupom.codigoCupom
                    };
                  pagamento = tef.confirmarTransacao();
                  if (pagamento)
                  {
                     tef.saveFile();
                     area_TEF.Value = tef.imprimir();
                  }
              }
              else
                 pagamento = true;

              if (pagamento)
              {
                 //-------------------------------------------------------------------
                 //   Implementar gravação da venda no banco
                 //-------------------------------------------------------------------


                 //-------------------------------------------------------------------
                 //   Reiniciando variáveis relacionadas com o Pedido
                 //-------------------------------------------------------------------
                 ViewState["itemCaixa"] = new ItemPedido();
                 ViewState["pedidoCaixa"] = new Pedido();
              }
              else
                 ShowMessage(MessageType.Warning, "Dados para pagamento inválidos.", "Pagamento inválido");
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

       protected void ListFrmPgto_SelectedIndexChanged(object sender, EventArgs e)
       {
          bool transacaoTEF = (ListFrmPgto.SelectedIndex > 0 && ListFrmPgto.SelectedIndex <= 2);
             // --------------------------------------------------
             //   Habilitar e Desabilitar Get dos dados do cartão
             // --------------------------------------------------
             txtCodCartao.Visible = transacaoTEF;
             txtNumCartao.Visible = transacaoTEF;
       }

       protected void CarregaGrid(int idCliente)
       {
          DataSet ds = ListaCarrinho.List(idCliente);
          gvTable.DataSource = ds;
          gvTable.DataBind();
       }

       protected void gvTable_RowDeleted(object sender, GridViewDeletedEventArgs e)
       {
          int rowIndex = gvTable.SelectedIndex;

          string productId = gvTable.DataKeys[rowIndex].Value.ToString();

          //bool success = ShoppingCartAccess.RemoveItem(productId);
       }

       protected void gvTable_RowCommand(object sender, GridViewCommandEventArgs e)
       {
          int rowIndex = int.Parse((string)e.CommandArgument);
          int idCarrinho = Convert.ToInt32(gvTable.DataKeys[rowIndex].Value.ToString());

          switch (e.CommandName)
          {
             case "excluir":
                FivesLivraria.Data.Classes.Carrinho.Delete(idCarrinho);
                break;
             case "atualizar":
                TextBox qtd = (TextBox)((GridView)sender).Rows[rowIndex].Cells[2].FindControl("nrQtdProduto");
                FivesLivraria.Data.Classes.Carrinho.Update(idCarrinho, int.Parse(qtd.Text));
                break;
          }

          CarregaGrid(1);
       }
   }
}