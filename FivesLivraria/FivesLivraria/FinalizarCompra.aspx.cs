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
    public partial class FinalizarCompra : BasePage
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
                carregaParcelas();
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
            for (int i = 1; i <= 12; i++)
            {
                string parcela = string.Format("{0}x sem juros de \tR${1}", i.ToString(), ((Double)Convert.ToDouble(txtTotal.Text) / i).ToString("N2"));
                rblParcelas.Items.Add(new ListItem(parcela, i.ToString()));
            }
            //rblParcelas.DataSource = parcelas;
            rblParcelas.SelectedIndex = 0;
            rblParcelas.DataBind();
        }

        protected void btnfinalizarCompra_Click(object sender, EventArgs e)
        {
            if (rbBoleto.Checked)
            {
                RequiredFieldValidator1.Enabled = false;
                RequiredFieldValidator2.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
                RequiredFieldValidator4.Enabled = false;
                Pedido.finalizaPedido(rbBoleto.Value, idUsuario, 0);
                ShowMessage(MessageType.Info, "Compra realizada com sucesso", "Parabéns", "Principal.aspx");
            }
            if (rbCredito.Checked)
            {
                if (Page.IsValid)
                {
                    Pedido.finalizaPedido(rbCredito.Value, idUsuario, rblParcelas.SelectedIndex + 1);
                    ShowMessage(MessageType.Info, "Compra realizada com sucesso", "Parabéns", "Principal.aspx");
                }
            }
        }

        protected void btnGerarBoleto_Click(object sender, EventArgs e)
        {

        }

    }
}