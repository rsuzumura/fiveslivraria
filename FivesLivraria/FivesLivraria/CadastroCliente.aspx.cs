﻿using System;   
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using FivesLivraria.Data;

namespace FivesLivraria
{
    public partial class CadastroCliente : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillControl<Estado>(dropEstado, EstadoCollection.List());
                FillControl<Municipio>(dropMunicipio, null);
                EnableFields(Convert.ToBoolean(rdoTipoCliente.SelectedValue));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate("cadastro");
            if (Page.IsValid)
            {                
                try
                {
                    MembershipCreateStatus s;
                    Membership.CreateUser(txtLogin.Text, txtPassword.Text, txtEmailAddress.Text, txtQuestion.Text, txtAnswer.Text, true, out s);
                    if (s != MembershipCreateStatus.Success)
                        ShowMessage(MessageType.Error, TraduzMensagem(s), "Erro");
                    else
                    {
                        Roles.AddUsersToRoles(new string[] { txtLogin.Text }, new string[] { "cliente" });
                        Usuario u = new Usuario()
                        {
                            nmUsuario = txtUser.Text,
                            dsLogin = txtLogin.Text
                        };
                        u.Insert();
                        if (Convert.ToBoolean(rdoTipoCliente.SelectedValue))
                        {
                            Empresa emp = new Empresa()
                            {
                                idUsuario = u.idUsuario,
                                nmCliente = txtRazaoSocial.Text,
                                nmRazaoSocial = txtRazaoSocial.Text,
                                cnpj = txtCNPJ.Text,
                                inscricaoMunicipal = txtInscricaoMunicipal.Text,
                                inscricaoEstadual = txtInscricaoEstadual.Text
                            };
                            if (ViewState["enderecos"] != null)
                                emp.Enderecos = (EnderecoCollection)ViewState["enderecos"];
                            emp.Insert();
                        }
                        else
                        {
                            Pessoa p = new Pessoa()
                            {
                                idUsuario = u.idUsuario,
                                nmCliente = txtUser.Text,
                                cpf = txtCPF.Text,
                                rg = txtRG.Text,
                                nmMae = txtNmMae.Text
                            };
                            if (!string.IsNullOrEmpty(datePickerNascimento.SelectedDateString))
                                p.dtNascimento = datePickerNascimento.SelectedDate;
                            if (ViewState["enderecos"] != null)
                                p.Enderecos = (EnderecoCollection)ViewState["enderecos"];
                            p.Insert();
                        }
                        Response.Redirect("Login.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage(MessageType.Error, ex.Message, "Erro");
                }
            }
        }   

        protected void rdoTipoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableFields(Convert.ToBoolean(rdoTipoCliente.SelectedValue));
        }

        protected void EnableFields(bool isCompany)
        {
            pnlCompany.Visible = isCompany;
            pnlCustomer.Visible = !isCompany;
        }

        protected void cvCNPJ_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                string taxId1 = txtCNPJ.Text.Replace(".", "").Replace("/", "").Replace("-", "");

                if (taxId1.Trim().Length.Equals(0))
                {
                    args.IsValid = true;
                    return;
                }

                int[] digits, sum, result;
                int nrDig;
                string ftmt;
                bool[] taxId1Ok;

                ftmt = "6543298765432";
                digits = new int[14];
                sum = new int[2];
                sum[0] = 0;
                sum[1] = 0;
                result = new int[2];
                result[0] = 0;
                result[1] = 0;
                taxId1Ok = new bool[2];
                taxId1Ok[0] = false;
                taxId1Ok[1] = false;

                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digits[nrDig] = int.Parse(taxId1.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        sum[0] += (digits[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1)));
                    if (nrDig <= 12)
                        sum[1] += (digits[nrDig] * int.Parse(ftmt.Substring(nrDig, 1)));
                }

                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    result[nrDig] = (sum[nrDig] % 11);
                    if ((result[nrDig] == 0) || (result[nrDig] == 1))
                        taxId1Ok[nrDig] = (digits[12 + nrDig] == 0);
                    else
                        taxId1Ok[nrDig] = (digits[12 + nrDig] == (11 - result[nrDig]));
                }
                args.IsValid = (taxId1Ok[0] && taxId1Ok[1]);
            }
            catch
            {
                args.IsValid = false;
            }
        }

        protected void cvCPF_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                bool ret = true;
                if (txtCPF.Text.Length > 0)
                {
                    string taxId = txtCPF.Text.Replace(".", "").Replace("-", "");

                    if (taxId.Length != 11)
                        ret = false;

                    bool equals = true;
                    for (int i = 1; i < 11 && equals; i++)
                        if (taxId[i] != taxId[0])
                            equals = false;

                    if (equals || taxId == "12345678909")
                        ret = false;

                    int[] numbers = new int[11];

                    for (int i = 0; i < 11; i++)
                        numbers[i] = int.Parse(taxId[i].ToString());

                    int sum = 0;
                    for (int i = 0; i < 9; i++)
                        sum += (10 - i) * numbers[i];

                    int result = sum % 11;

                    if (result == 1 || result == 0)
                    {
                        if (numbers[9] != 0)
                            ret = false;
                    }
                    else if (numbers[9] != 11 - result)
                        ret = false;

                    sum = 0;
                    for (int i = 0; i < 10; i++)
                        sum += (11 - i) * numbers[i];

                    result = sum % 11;

                    if (result == 1 || result == 0)
                    {
                        if (numbers[10] != 0)
                            ret = false;
                    }
                    else
                        if (numbers[10] != 11 - result)
                            ret = false;

                    args.IsValid = ret;

                }
                else
                    args.IsValid = true;
            }
            catch
            {
                args.IsValid = false;
            }
        }

        protected void dropEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dropEstado.SelectedValue))
                FillControl<Municipio>(dropMunicipio, MunicipioCollection.List(Convert.ToInt32(dropEstado.SelectedValue)));
            else
                FillControl<Municipio>(dropMunicipio, null);
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
                    dsEndereco   = txtEnderecoCliente.Text,
                    nrEndereco   = txtNumero.Text,
                    compEndereco = txtComplemento.Text,
                    cep          = txtCEP.Text,
                    dsBairro     = txtBairro.Text
                };
                if (!string.IsNullOrEmpty(dropMunicipio.SelectedValue))
                    end.idMunicipio = Convert.ToInt32(dropMunicipio.SelectedValue);
                ec.Add(end);
                ViewState["enderecos"] = ec;

                gridEnderecos.DataSource = ec;
                gridEnderecos.DataBind();
                ClearFields();
            }
        }

        protected void gridEnderecos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            EnderecoCollection ec = (EnderecoCollection)ViewState["enderecos"];
            ec.Remove(ec[e.RowIndex]);
            gridEnderecos.DataSource = ec;
            gridEnderecos.DataBind();
        }

        protected void ClearFields()
        {
            txtEnderecoCliente.Text = txtNumero.Text = txtBairro.Text = txtComplemento.Text = txtCEP.Text = string.Empty;
            dropMunicipio.SelectedIndex = dropEstado.SelectedIndex = -1;
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

        protected void lkbBuscaCEP_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCEP.Text.Trim()))
            {
                CEP cep = CEP.Get(txtCEP.Text.Trim());
                if (cep != null)
                {
                    txtEnderecoCliente.Text = cep.endereco.Value;
                    txtBairro.Text = cep.bairro.Value;
                    dropEstado.SelectedValue = cep.idEstado.Value.ToString();
                    FillControl<Municipio>(dropMunicipio, MunicipioCollection.List(Convert.ToInt32(dropEstado.SelectedValue)));
                    ListItem li = dropMunicipio.Items.FindByText(cep.cidade.Value);
                    if (li != null)
                        dropMunicipio.SelectedValue = li.Value;
                    txtNumero.Focus();
                }
            }
        }
    }
}