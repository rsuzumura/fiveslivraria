using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data;
using System.IO;

namespace FivesLivraria.Administrativo
{
    public partial class Produtos : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int t = 0;
                ListaCategoria lc = ListaCategoria.List(null, true, 0, int.MaxValue - 1, out t);
                FillControl<Categoria>(dropCategorias, lc);
                BindGrid(0);
            }
        }

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["idCategoria"] = dropCategorias.SelectedValue;
            ViewState["name"] = txtName.Text;
            BindGrid(0);
        }

        protected void gridProdutos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idProduto = Convert.ToInt32(((HiddenField)gridProdutos.Rows[e.RowIndex].FindControl("hdnIdProduto")).Value);
            Produto p = Produto.Get(idProduto);            
            Produto.Delete(idProduto);
            if (!p.nmImagem.IsNull)
                File.Delete(Server.MapPath(string.Format("~/Images/{0}", p.nmImagem.Value)));

            BindGrid(gridProdutos.PageIndex);
        }

        protected void gridProdutos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindGrid(e.NewPageIndex);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroProduto.aspx", false);
        }

        protected void BindGrid(int pageIndex)
        {
            int idCategoria = 0;
            idCategoria = 0;
            int.TryParse((string)ViewState["idCategoria"], out idCategoria);
            int t = 0;
            ListaProdutos lp = ListaProdutos.ListByFilter(idCategoria, (string)ViewState["name"], pageIndex, 10, out t);
            gridProdutos.VirtualCount = t;
            lblRecords.Text = string.Format("Qtde de Registros: {0}", t);
            gridProdutos.DataSource = lp;
            gridProdutos.DataBind();
        }

        protected void imgEdit_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgEdit = (ImageButton)sender;
            Response.Redirect(string.Format("CadastroProduto.aspx?id={0}", imgEdit.CommandArgument), false);
        }
    }
}