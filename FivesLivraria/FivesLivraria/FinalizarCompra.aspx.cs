using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data.Classes;
using System.Data;
using System.IO;
using System.Data.SqlTypes;
using FivesLivraria.Data;

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
            Cliente cli = Cliente.Get(Current.UserId);
            if (!Page.IsPostBack)
            {
                CarregaGrid(idUsuario);
                carregaParcelas();
                EnderecoCollection ec = EnderecoCollection.ListByCliente(cli.idCliente.Value);
                FillControl<Endereco>(ddlEntrega, ec);
                FillControl<Endereco>(ddlCobranca, ec);
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
            //foreach (GridViewRow row in gvCarrinho.Rows)
            //{
            //    total = total + Convert.ToDouble(row.Cells[3].Text.Replace(".", ","));
            //}
            //txtTotal.Text = total.ToString("N2");
        }
       
    
        protected void btnContinuarComprando_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }

        protected void carregaParcelas()
        {
            decimal total = Convert.ToDecimal(hdnTotal.Value);
            for (int i = 1; i <= 12; i++)
            {
                string parcela = string.Format("{0}x sem juros de \tR${1}", i.ToString(), ((Double)Convert.ToDouble(total) / i).ToString("N2"));
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
                FivesLivraria.Data.Classes.Pedido.finalizaPedido(rbBoleto.Value, idUsuario, 0, Convert.ToInt32(ddlEntrega.SelectedValue), Convert.ToInt32(ddlCobranca.SelectedValue));
                ShowMessage(MessageType.Info, "Compra realizada com sucesso", "Parabéns", "Principal.aspx");
            }
            if (rbCredito.Checked)
            {
                if (Page.IsValid)
                {
                    FivesLivraria.Data.Classes.Pedido.finalizaPedido(rbCredito.Value, idUsuario, rblParcelas.SelectedIndex + 1);
                    ShowMessage(MessageType.Info, "Compra realizada com sucesso", "Parabéns", "Principal.aspx");
                }
            }
        }

        protected void btnGerarBoleto_Click(object sender, EventArgs e)
        {

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

                e.Row.Cells[3].Text = string.Format("{0:C}", total);
                hdnTotal.Value = total.ToString();
            }
        }

    }
}