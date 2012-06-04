using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data;
using System.IO;

namespace FivesLivraria
{
    public partial class Pedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData(0);
            }
        }

        protected void gridPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void LoadData(int pageIndex)
        {
            int idPedido = Convert.ToInt32(Request.QueryString["idpedido"]);
            FivesLivraria.Data.Pedido p = FivesLivraria.Data.Pedido.Get(idPedido);
            ltPedido.Text = string.Format("Pedido Nº {0}", p.idPedido.Value);
            ltDtPedido.Text = p.dtPedido.Value.ToShortDateString();
            ltDataPrevista.Text = !p.dtEntregaPrevista.IsNull ? p.dtEntregaPrevista.Value.ToShortDateString() : string.Empty;
            ltEnderecoEntrega.Text = !p.EnderecoEntrega.IsNull ? p.EnderecoEntrega.Value : string.Empty;
            ltEnderecoCobranca.Text = !p.EnderecoCobranca.IsNull ? p.EnderecoCobranca.Value : string.Empty;

            ListaProdutos lp = ListaProdutos.ListByPedido(idPedido);
            gridProdutos.DataSource = lp;
            gridProdutos.DataBind();
        }

        protected void gridProdutos_RowDataBound(object sender, GridViewRowEventArgs e)
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
                foreach (Produto p in (ListaProdutos)gridProdutos.DataSource)
                {
                    total += p.vlFinal.Value;
                }
                e.Row.Cells[3].Text = string.Format("{0:C}", total);
            }
        }
    }
}