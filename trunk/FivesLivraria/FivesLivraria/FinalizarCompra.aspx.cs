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
    public partial class FinalizarCompra : System.Web.UI.Page
    {
        private int idUsuario
        {
            get { return Current.UserId; }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaGrid(idUsuario);
            carregaParcelas();
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
            double total = 0;
            foreach (GridViewRow row in gvCarrinho.Rows)
            {
                total = total + Convert.ToDouble(row.Cells[3].Text.Replace(".", ","));
            }
            txtTotal.Text = total.ToString("N2");
        }
       
    
        protected void btnContinuarComprando_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }

        protected void carregaParcelas()
        {
            List<string> parcelas = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                string parcela = string.Format("{0}x sem juros de \tR${1}", i.ToString(), ((Double)Convert.ToDouble(txtTotal.Text) / i).ToString("N2"));
                parcelas.Add(parcela);
            }

            rblParcelas.DataSource = parcelas;
            rblParcelas.DataBind();
        }

        protected void btnfinalizarCompra_Click(object sender, EventArgs e)
        {
            if (rbBoleto.Checked)
            {
                Pedido.finalizaPedido(rbBoleto.Value, idUsuario, 0);
            }
            if (rbCredito.Checked)
            {
                Pedido.finalizaPedido(rbCredito.Value, idUsuario, rblParcelas.SelectedIndex + 2);
            }
        }

        protected void btnGerarBoleto_Click(object sender, EventArgs e)
        {

        }
    }
}