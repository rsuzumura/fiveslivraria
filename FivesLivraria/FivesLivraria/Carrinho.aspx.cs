using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data.Classes;
using System.Data;

namespace FivesLivraria
{
    public partial class Carrinho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaGrid(1);
        }

        protected void CarregaGrid(int idCliente) 
        {
            DataSet ds = ListaCarrinho.List(idCliente);
            gvCarrinho.DataSource = ds;
            gvCarrinho.DataBind();
        } 

        protected void gvCarrinho_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            int rowIndex = gvCarrinho.SelectedIndex;

            string productId = gvCarrinho.DataKeys[rowIndex].Value.ToString();
            
            //bool success = ShoppingCartAccess.RemoveItem(productId);
        }

        protected void gvCarrinho_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse((string)e.CommandArgument);
            int idCarrinho = Convert.ToInt32(gvCarrinho.DataKeys[rowIndex].Value.ToString());

            switch(e.CommandName)
            {
                case "excluir" :
                    FivesLivraria.Data.Classes.Carrinho.Delete(idCarrinho);
                    break;
                case "atualizar" :
                    TextBox qtd = (TextBox)((GridView)sender).Rows[rowIndex].Cells[2].FindControl("nrQtdProduto");
                    FivesLivraria.Data.Classes.Carrinho.Update(idCarrinho, int.Parse(qtd.Text));
                    break;
            }

            CarregaGrid(1);           
        }

        protected void btnContinuarComprando_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }

        protected void btnFecharPedido_Click(object sender, EventArgs e)
        {

        }
    }
}