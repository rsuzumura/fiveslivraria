using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data;

namespace FivesLivraria
{
    public partial class ViewCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData(0);
            }
        }

        protected void LoadData(int pageIndex)
        {
            Cliente c = Pessoa.Get(Current.UserId);
            if (c != null)
            {
                ltLabelTaxId.Text = "CPF:";
                ltTaxId.Text = ((Pessoa)c).cpf.Value;
                ltNome.Text = c.nmCliente.Value;
            }
            else
            {
                c = Empresa.Get(Current.UserId);
                ltLabelTaxId.Text = "CNPJ:";
                ltTaxId.Text = ((Empresa)c).cnpj.Value;
                ltNome.Text = c.nmCliente.Value;
            }
            int t = 0;
            ListaPedido lp = ListaPedido.List(c.idCliente.Value, pageIndex, gridPedidos.PageSize, out t);
            gridPedidos.DataSource = lp;
            gridPedidos.DataBind();
        }

        protected void gridPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadData(e.NewPageIndex);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/CadastroCliente.aspx?idcliente={0}", Current.UserId), false);
        }
    }
}