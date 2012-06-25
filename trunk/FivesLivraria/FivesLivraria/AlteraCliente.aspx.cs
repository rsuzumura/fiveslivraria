using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data;
using System.Web.Security;

namespace FivesLivraria
{
    public partial class AlteraCliente : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillControl<Estado>(dropEstado, EstadoCollection.List());
                FillControl<Municipio>(dropMunicipio, null);
                if (!string.IsNullOrEmpty(Request.QueryString["idUsuario"]))
                {
                    int idUsuario = Convert.ToInt32(Request.QueryString["idUsuario"]);
                    Cliente cli = Pessoa.Get(idUsuario);
                    if (cli != null)
                    {
                        pnlCustomer.Visible = true;
                        pnlCompany.Visible = false;
                        txtUser.Text = GetString(((Pessoa)cli).nmCliente);
                        txtCPF.Text = GetString(((Pessoa)cli).cpf);
                        txtRG.Text = GetString(((Pessoa)cli).rg);
                        datePickerNascimento.SelectedDateString = GetString(((Pessoa)cli).dtNascimento);
                        txtNmMae.Text = GetString(((Pessoa)cli).nmMae);
                    }
                    else
                    {
                        cli = Empresa.Get(idUsuario);
                        pnlCustomer.Visible = false;
                        pnlCompany.Visible = true;
                        txtRazaoSocial.Text = GetString(((Empresa)cli).nmRazaoSocial);
                        txtCNPJ.Text = GetString(((Empresa)cli).cnpj);
                        txtInscricaoMunicipal.Text = GetString(((Empresa)cli).inscricaoMunicipal);
                        txtInscricaoEstadual.Text = GetString(((Empresa)cli).inscricaoEstadual);
                    }

                    EnderecoCollection ec = EnderecoCollection.ListByCliente(cli.idCliente.Value);
                    ViewState["enderecos"] = ec;
                    gridEnderecos.DataSource = ec;
                    gridEnderecos.DataBind();

                    MembershipUser mu = Membership.GetUser();
                    txtEmailAddress.Text = mu.Email;
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("cadastro");
            if (Page.IsValid)
            {
                try
                {
                    int idUsuario = Convert.ToInt32(Request.QueryString["idUsuario"]);
                    if (pnlCustomer.Visible)
                    {
                        Pessoa pes = Pessoa.Get(idUsuario);
                        pes.rg = txtRG.Text;
                        pes.dtNascimento = datePickerNascimento.SelectedDate;
                        pes.nmMae = txtNmMae.Text;
                        pes.Enderecos = (EnderecoCollection)ViewState["enderecos"];
                        pes.Save();
                    }
                    else
                    {
                        Empresa emp = Empresa.Get(idUsuario);
                        emp.inscricaoMunicipal = txtInscricaoMunicipal.Text;
                        emp.inscricaoEstadual = txtInscricaoEstadual.Text; ;
                        emp.Enderecos = (EnderecoCollection)ViewState["enderecos"];
                        emp.Update();
                    }
                    Cliente.ChangeEmail(User.Identity.Name, txtEmailAddress.Text);
                    Response.Redirect("~/ViewCliente.aspx", false);
                }
                catch (Exception ex)
                {
                    ShowMessage(MessageType.Error, ex.Message, "Erro");
                }
            }
        }

        protected void btnAddEndereco_Click(object sender, EventArgs e)
        {
            Page.Validate("Endereco");
            if (Page.IsValid)
            {
                EnderecoCollection ec = null;
                if (ViewState["enderecos"] != null)
                    ec = (EnderecoCollection)ViewState["enderecos"];
                else
                    ec = new EnderecoCollection();

                Endereco end = new Endereco()
                {
                    dsEndereco = txtEnderecoCliente.Text,
                    nrEndereco = txtNumero.Text,
                    compEndereco = txtComplemento.Text,
                    cep = txtCEP.Text,
                    dsBairro = txtBairro.Text
                };
                if (!string.IsNullOrEmpty(dropMunicipio.SelectedValue))
                    end.idMunicipio = Convert.ToInt32(dropMunicipio.SelectedValue);
                ec.Add(end);
                ViewState["enderecos"] = ec;

                gridEnderecos.DataSource = ec;
                gridEnderecos.DataBind();
                txtEnderecoCliente.Text = txtNumero.Text = txtBairro.Text = txtComplemento.Text = txtCEP.Text = string.Empty;
                dropMunicipio.SelectedIndex = dropEstado.SelectedIndex = -1;
            }
        }

        protected void cvEndereco_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (gridEnderecos.Rows.Count == 0)
                args.IsValid = false;
        }

        protected void cvNascimento_ServerValidate(object source, ServerValidateEventArgs args)
        {
            TimeSpan ts = DateTime.Now.Subtract(datePickerNascimento.SelectedDate);
            DateTime idade = (new DateTime() + ts);
            if (idade.Year > 1)
                idade = idade.AddYears(-1);
            if (idade.Year < 18)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        protected void dropEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dropEstado.SelectedValue))
                FillControl<Municipio>(dropMunicipio, MunicipioCollection.List(Convert.ToInt32(dropEstado.SelectedValue)));
            else
                FillControl<Municipio>(dropMunicipio, null);
        }

        protected void gridEnderecos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            EnderecoCollection ec = (EnderecoCollection)ViewState["enderecos"];
            ec.Remove(ec[e.RowIndex]);
            gridEnderecos.DataSource = ec;
            gridEnderecos.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ViewCliente.aspx", false);
        }
    }
}