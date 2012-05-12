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

        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack)
           ViewState["item"] = new ItemPedido();
        }

       protected void btn_Item_onClick(object sender, EventArgs e)
        {
           int indice = listProdutosTeste.SelectedIndex;
           
           int codProd = int.Parse(ProdutosTeste[indice, 0]);
           double vlr = double.Parse(ProdutosTeste[indice, 2]);
           string nome = ProdutosTeste[indice, 1];

           item = (ItemPedido) ViewState["item"];
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
                
        }

       protected void btn_Pedido_onClick(object sender, EventArgs e)
        {
          string area = area_Cupom.Value;
          
          item = (ItemPedido)ViewState["item"];          
          
          if (area.Length != 0)
              area += "\n";

          for ( int n = 0; n < item.Count(); n++)
              area += item.ToString(n) + "\n";

          // colando o conteúdo para simulação de cupom
          area_Cupom.Value = area;

          if ( ListFrmPgto.SelectedIndex != 0 )
            area_TEF.Value = ListFrmPgto.SelectedValue;

          ViewState["item"] = new ItemPedido();
        }

   }
}