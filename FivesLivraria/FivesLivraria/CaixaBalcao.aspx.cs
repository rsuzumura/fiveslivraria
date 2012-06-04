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
        protected static string[,] ProdutosTeste = new string[5, 3]
                                 { {"1", "Produto 01", "25,00" },
                                   {"2", "Produto 02", "15,00" },
                                   {"3", "Produto 03", "35,00" },
                                   {"4", "Produto 04", "20,00" },
                                   {"5", "Produto 05", "10,00" } };
        FivesLivraria.Data.Classes.Pedido pedido;
        ItemPedido  item;
        CupomFiscal cupom;
        bool vendaOk = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Current.AcessoCaixa.userId != 0)
                {
                    ViewState["pedidoCaixa"] = new FivesLivraria.Data.Classes.Pedido();
                    ViewState["itemCaixa"]   = new ItemPedido();
                    area_Cupom.Visible       = true;
                    area_TEF.Visible         = true;
                    Session["cupom"]         = new CupomFiscal();
                }
                else
                    exibeErroAbertura();
            }
        }

        protected void btnItem_Click(object sender, EventArgs e)
        {
            if (vendaOk)
            {
                int indice = listProdutosTeste.SelectedIndex;

                int codProd = int.Parse(ProdutosTeste[indice, 0]);
                double vlr = double.Parse(ProdutosTeste[indice, 2]);
                string nome = ProdutosTeste[indice, 1];

                item = (ItemPedido)ViewState["itemCaixa"];
                item.AddProduto(codProd, nome, vlr);

                cupom = (CupomFiscal)Session["cupom"];
                cupom.setItem(item);

                TableCell celltemp1 = new TableCell();
                celltemp1.Text = codProd.ToString();
                TableCell celltemp2 = new TableCell();
                celltemp2.Text = nome;
                TableCell celltemp3 = new TableCell();
                celltemp3.Text = vlr.ToString();
                TableRow rowtemp = new TableRow();
                rowtemp.Cells.Add(celltemp1);
                rowtemp.Cells.Add(celltemp2);
                rowtemp.Cells.Add(celltemp3);

                tblItensTeste.Rows.Add(rowtemp);

            }
            else
            {
                exibeErroAbertura();
                // redirecionar página para não acessar
            }
        }

        protected void btnPedido_Click(object sender, EventArgs e)
        {
            bool pagamento = false;
            cupom = (CupomFiscal)Session["cupom"];

            if (cupom != null)
            {
                if (vendaOk)
                {
                    pedido = (FivesLivraria.Data.Classes.Pedido)ViewState["pedidoCaixa"];
                    item = (ItemPedido)ViewState["itemCaixa"];

                    // colando o conteúdo para simulação de cupom
                    cupom.setItem(item);
                    cupom.setCliente((box_CPFCliente.Text.Length != 0 ? long.Parse(box_CPFCliente.Text) : 0));
                    cupom.gerarCodigo();

                    if (ListFrmPgto.SelectedIndex != 0)
                    {
                        string cardNumber = (string)txtNumCartao.Text;
                        string cardId = (string)txtCodCartao.Text;
                        TEF tef = new TEF()
                        {
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
                        area_Cupom.Value = cupom.imprimeCupom();
                        //-------------------------------------------------------------------
                        //   Implementar gravação da venda no banco
                        //-------------------------------------------------------------------


                        //-------------------------------------------------------------------
                        //   Reiniciando variáveis relacionadas com o Pedido
                        //-------------------------------------------------------------------
                        ViewState["itemCaixa"] = new ItemPedido();
                        ViewState["pedidoCaixa"] = new FivesLivraria.Data.Classes.Pedido();

                        //-------------------------------------------------------------------
                        //   Reposicionar link para não conseguir 'voltar' os comandos
                        //-------------------------------------------------------------------

                    }
                    else
                        ShowMessage(MessageType.Warning, "Dados para pagamento inválidos.", "Pagamento inválido");
                }
                else
                {
                    exibeErroAbertura();
                    // redirecionar página para não acessar
                }
            }
            else
                exibeErroAbertura();
            // redirecionar página para não acessar
        }

        protected void ListFrmPgto_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool transacaoTEF = (ListFrmPgto.SelectedIndex > 0 && ListFrmPgto.SelectedIndex <= 2);
            // --------------------------------------------------
            //   Habilitar e Desabilitar Get dos dados do cartão
            // --------------------------------------------------
            lblCartao.Enabled = transacaoTEF;
            lblCodCartao.Enabled = transacaoTEF;
            txtCodCartao.Enabled = transacaoTEF;
            lblNumCartao.Enabled = transacaoTEF;
            txtNumCartao.Enabled = transacaoTEF;

            lblCartao.Visible = transacaoTEF;
            lblCodCartao.Visible = transacaoTEF;
            txtCodCartao.Visible = transacaoTEF;
            lblNumCartao.Visible = transacaoTEF;
            txtNumCartao.Visible = transacaoTEF;


        }

        protected void exibeErroAbertura()
        {
            string txtErro = "Caixa não foi aberto, realizar abertura ou aguardar dia seguinte!";
            ShowMessage(MessageType.Error, txtErro, "Erro: Venda", "CaixaOpcoes.aspx");
        }

    }
}