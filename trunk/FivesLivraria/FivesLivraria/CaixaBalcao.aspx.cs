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
    public partial class CaixaBalcao : System.Web.UI.Page
    {
       protected static string[,] ProdutosTeste = new string[5,3]
                                 { {"1", "Produto 01", "25,00" },
                                   {"2", "Produto 02", "15,00" },
                                   {"3", "Produto 03", "35,00" },
                                   {"4", "Produto 04", "20,00" },
                                   {"5", "Produto 05", "10,00" } };
       ItemPedido item;
       int qtde;

        protected void Page_Load(object sender, EventArgs e)
        {
           item = new ItemPedido();
           qtde = 0;
        }

        protected void clk_itemPedido(object sender, EventArgs e)
        {
           int codProd = int.Parse(ListFrmPgto.SelectedValue);
           double vlr = double.Parse(ProdutosTeste[(codProd-1),2]);
           string nome = ProdutosTeste[(codProd-1),1];

           item.AddProduto(codProd, nome, vlr);
           qtde++;

           area_Cupom.Value = area_Cupom.Value + "\n" + item.ToString(qtde);
           area_TEF.Value = "Teste";
        }
    }
}