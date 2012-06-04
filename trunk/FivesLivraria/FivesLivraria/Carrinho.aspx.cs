using System;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data.Classes;

namespace FivesLivraria
{
    public partial class Carrinho : BasePage
    {

        private int idUsuario
        {
            get { return Current.UserId; }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaGrid(idUsuario);

            }
        }

        protected void CarregaGrid(int idCliente)
        {
            DataSet ds = ListaCarrinho.List(idCliente);
            gvCarrinho.DataSource = ds;
            gvCarrinho.DataBind();
            calculaTotal();
        }


        protected void calculaTotal()
        {
            //double total = 0;
            //foreach(GridViewRow row in gvCarrinho.Rows)
            //{
            //    total = total + Convert.ToDouble(row.Cells[4].Text.Replace(".",","));
            //}
            //txtTotal.Text = total.ToString("N2");
        }


        protected void gvCarrinho_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse((string)e.CommandArgument);
            int idCarrinho = Convert.ToInt32(gvCarrinho.DataKeys[rowIndex].Value.ToString());

            switch (e.CommandName)
            {
                case "excluir":
                    FivesLivraria.Data.Classes.Carrinho.Delete(idCarrinho);
                    break;
                case "atualizar":
                    TextBox qtd = (TextBox)gvCarrinho.Rows[rowIndex].FindControl("nrQtdProduto");
                    FivesLivraria.Data.Classes.Carrinho.Update(idCarrinho, int.Parse(qtd.Text));
                    break;
            }

            CarregaGrid(idUsuario);
        }

        protected void btnContinuarComprando_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }

        protected void btnFecharPedido_Click(object sender, EventArgs e)
        {
            Response.Redirect("FinalizarCompra.aspx");
        }

        protected void gvCarrinho_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image imagem = (Image)e.Row.FindControl("imagem");
                if (!File.Exists(Server.MapPath(imagem.ImageUrl)))
                    imagem.ImageUrl = "~/Images/imagem_nao_disponivel.jpg";
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                decimal total = 0;
                foreach (DataRow dr in ((DataSet)gvCarrinho.DataSource).Tables[0].Rows)
                    total += ((SqlDecimal)dr["vlFinal"]).Value;

                e.Row.Cells[4].Text = string.Format("{0:C}", total);
            }
        }
    }
}