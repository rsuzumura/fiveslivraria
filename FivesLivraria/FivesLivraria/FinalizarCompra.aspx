<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="FinalizarCompra.aspx.cs" Inherits="FivesLivraria.FinalizarCompra" %>
<%@ Import Namespace="System.Data.SqlTypes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" HeaderText="Atenção: verifique os seguintes itens:"
        CssClass="validationMessage" ValidationGroup="Encerrar" />

    <div align="left" style="width: 100%; float: left; margin: 30px">
        <div align="left" style="width: 50%; float: left">
            <div align="center">
                <asp:GridView ID="gvCarrinho" runat="server" AutoGenerateColumns="False" DataKeyNames="idCarrinho"
                    OnRowDataBound="gvCarrinho_RowDataBound" ShowFooter="true" Width="90%">
                    <FooterStyle Font-Bold="true" />
                    <Columns>
                        <asp:TemplateField HeaderText="Descrição">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <table style="margin-left: 10px;">
                                    <tr>
                                        <td>
                                            <asp:Image ID="imagem" runat="server" Text="Imagem" ImageUrl='<%#"~/Images/"+ Eval("nmImagem") %>'
                                                Width="80px" />
                                        </td>
                                        <td>
                                            <%#Eval("nmTitulo")%>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="nrQtdProduto" HeaderText="Quantidade" SortExpression="idProduto" />
                        <asp:TemplateField HeaderText="Preço Unitário">
                            <FooterTemplate>
                                Total:
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# String.Format("{0:C}", ((SqlDecimal)Eval("vlPreco")).Value) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Final">
                            <ItemTemplate>
                                <%# String.Format("{0:C}", ((SqlDecimal)Eval("vlFinal")).Value) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        Não foi encontrado nenhum produto</EmptyDataTemplate>
                </asp:GridView>
                <br />
                <asp:Button ID="btnContinuarComprando" runat="server" Text="Continuar Comprando"
                    CssClass="buttonAcao" OnClick="btnContinuarComprando_Click" CausesValidation="false"
                    Width="200px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnfinalizarCompra" runat="server" Text="Finalizar Compra" CssClass="buttonAcao"
                    OnClick="btnfinalizarCompra_Click" Width="200px" ValidationGroup="Encerrar" />
                <asp:HiddenField ID="hdnTotal" runat="server" Value="" />
            </div>
        </div>
        <div style="width: 50%; float: left">
            <div style="float: left">
                &nbsp;<asp:Label ID="lblFormaPagamento" runat="server" Text="Forma de Pagamento:" Font-Bold="true" />
                <br />
                <br />
                <input type="radio" id="rbBoleto" name="pagamento" value='Boleto' runat="server"
                    onclick="seleciona(this)" checked />
                <label for="Boleto">
                    Boleto</label>
                <br />
                <input type="radio" id="rbDebito" name="pagamento" value="Debito" runat="server"
                    onclick="seleciona(this)" />
                <label for="Debito">
                    Debito</label>
                <br />
                <input type="radio" id="rbCredito" name="pagamento" value="Credito" runat="server"
                    onclick="seleciona(this)" />
                <label for="Credito">
                    Credito</label>
                <%--<asp:RadioButtonList ID="rblFormaPagamento" runat="server">
                    <asp:ListItem Value="Boleto">Boleto</asp:ListItem>
                    <asp:ListItem>Debito</asp:ListItem>
                    <asp:ListItem>Cartão de Crédito</asp:ListItem>
                </asp:RadioButtonList>--%>
            </div>
            <br />
            <div id="divBoleto" style="float: left; margin-left: 50px;">
                <asp:Button ID="btnGerarBoleto" runat="server" Text="Imprimir Boleto" CssClass="buttonAcao" Width="150px"
                    OnClick="btnGerarBoleto_Click" />
            </div>
            <div id="divDebito" style="float: left; display: none; margin-left: 50px; ">
                <asp:RadioButtonList ID="rblBancos" runat="server">
                    <asp:ListItem>Bradesco</asp:ListItem>
                    <asp:ListItem>Itau</asp:ListItem>
                    <asp:ListItem>Santander</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div id="divCredito" style="float: left; display: none; margin-left: 50px">
                <table>
                    <tr>
                        <td>
                            Banco:
                        </td>
                        <td>
                            <asp:TextBox ID="txtNrBanco" runat="server" MaxLength="100" Width="200px"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo: Banco Obrigatorio" Text="*" ControlToValidate="txtNrBanco" EnableClientScript="False" ValidationGroup="Encerrar"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Nome do Titular:
                        </td>
                        <td>
                            <asp:TextBox ID="txtnmTitular" runat="server" MaxLength="100" Width="200px"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Campo: Nome do Titular Obrigatorio" Text="*" ControlToValidate="txtnmTitular" EnableClientScript="False" ValidationGroup="Encerrar"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Nr. Cartão:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCartao" runat="server" MaxLength="16" Width="150px" onkeypress="return validateInt(this, event);"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Campo: Nr. Cartão Obrigatorio" Text="*" ControlToValidate="txtCartao" EnableClientScript="False" ValidationGroup="Encerrar"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Digito de Segurança:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDigito" runat="server" MaxLength="3" Width="30px" onkeypress="return validateInt(this, event);"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Campo: Digito de Segurança Obrigatorio" Text="*" ControlToValidate="txtDigito" EnableClientScript="False" ValidationGroup="Encerrar"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Label ID="lblParcelas" runat="server" Text="Parcelas:" />
                <br />
                <br />
                <asp:RadioButtonList ID="rblParcelas" runat="server" >
                </asp:RadioButtonList>
            </div>
            <br />
        </div>
    </div>
    <div>
        <table style="width: 90%; " class="data">
            <colgroup>
                <col width="20%" />
                <col width="80%" />
            </colgroup>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvEntrega" runat="server" ControlToValidate="ddlEntrega"
                        ErrorMessage="O endereço de entrega é obrigatório" Text="*" ValidationGroup="Encerrar"></asp:RequiredFieldValidator>
                    Endereço de Entrega:
                </td>
                <td>
                    <asp:DropDownList ID="ddlEntrega" runat="server" Width="90%" DataTextField="dsEndereco"
                        DataValueField="idEndereco">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvCobranca" runat="server" ControlToValidate="ddlCobranca"
                        ErrorMessage="O endereço de cobrança é obrigatório" Text="*" ValidationGroup="Encerrar"></asp:RequiredFieldValidator>
                    Endereço de Cobrança:
                </td>
                <td>
                    <asp:DropDownList ID="ddlCobranca" runat="server" Width="90%" DataTextField="dsEndereco"
                        DataValueField="idEndereco">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        function seleciona(id) {
            $("#divBoleto").hide();
            $("#divCredito").hide();
            $("#divDebito").hide();
            $("#div" + id.value).show();
        }

        function validateInt(obj, evt) {
            if (typeof obj == 'object') {
                var key = evt.keyCode;
                if (key < 48 || key > 57)
                    return false;
                else
                    return true;
            }
        }
    </script>
</asp:Content>
