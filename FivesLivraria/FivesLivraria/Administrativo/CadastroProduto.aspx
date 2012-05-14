<%@ Page Title="" Language="C#" MasterPageFile="~/Administrativo/Admin.Master" AutoEventWireup="true" CodeBehind="CadastroProduto.aspx.cs" Inherits="FivesLivraria.Administrativo.CadastroProduto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function upload(e) {
            var bt = document.getElementById('ContentPlaceHolder1_btnUpload');
            if (typeof bt == 'object') {
                bt.click();
                return true;
            }
        }

        function validateValue(obj, evt) {
            if (navigator.userAgent.indexOf('Internet Explorer') > (-1)) {
                if (typeof obj == 'object') {
                    var key = evt.keyCode;
                    if ((key < 48 || key > 57) && key != 44)
                        return false;
                    else {
                        if (key == 44) {
                            if (obj.value.indexOf(',') > -1)
                                return false;
                            else
                                return true;
                        }
                        else
                            return true;
                    }
                }
                return true;
            } else if (navigator.userAgent.indexOf('Firefox') > (-1)) {
                if (typeof obj == 'object') {
                    var key = evt.charCode;
                    if ((key < 48 || key > 57) && (key != 44) && (key != 0))
                        return false;
                    else {
                        if (key == 44) {
                            if (obj.value.indexOf(',') > -1)
                                return false;
                            else
                                return true;
                        }
                        else
                            return true;
                    }
                }
                return true;
            }
        }

        function validateInt(obj, evt) {
            if (navigator.userAgent.indexOf('Internet Explorer') > (-1)) {
                if (typeof obj == 'object') {
                    var key = evt.keyCode;
                    if (key < 48 || key > 57)
                        return false;
                    else
                        return true;
                }
            } else if (navigator.userAgent.indexOf('Firefox') > (-1)) {
                if (typeof obj == 'object') {
                    var key = evt.charCode;
                    if ((key < 48 || key > 57) && key != 0)
                        return false;
                    else
                        return true;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="toolbar">
        <asp:Button ID="btnSave" runat="server" Text="Cadastrar" CssClass="button" OnClick="btnSave_Click"
            CausesValidation="false" />
        <asp:Button ID="btnCancel" runat="server" Text="Voltar" CausesValidation="false" ToolTip="Criar um novo usuário"
            OnClick="btnCancel_Click" />
    </div>
    <br />
    <div class="divForm" style="height: 570px;">
        <div class="divFormHeader">
            &nbsp;&nbsp;Cadastro de Produtos
        </div>
        <table class="data">
            <colgroup>
                <col width="14%" />
                <col width="38%" />
                <col width="14%" />
                <col width="34%" />
            </colgroup>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ErrorMessage="O nome do produto é obrigatório."
                        ControlToValidate="txtNmTitulo" Text="*"></asp:RequiredFieldValidator>
                    Nome do&nbsp;<br />Produto:&nbsp;
                </td>
                <td colspan="3" style="text-align: left; ">
                    <asp:TextBox ID="txtNmTitulo" runat="server" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    Título Original:&nbsp;
                </td>
                <td colspan="3" style="text-align: left; ">
                    <asp:TextBox ID="txtOriginalTitle" runat="server" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    Descrição:&nbsp;
                </td>
                <td colspan="3" style="text-align: left; ">
                    <asp:TextBox ID="txtDescription" runat="server" Rows="6" TextMode="MultiLine" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    Categoria:&nbsp;
                </td>
                <td style="text-align: left; ">
                    <asp:DropDownList ID="dropCategorias" DataTextField="dsCategoria" DataValueField="idCategoria"
                        runat="server" Width="70%">
                    </asp:DropDownList>
                </td>
                <td class="name">
                    ISBN:&nbsp;
                </td>
                <td style="text-align: left; ">
                    <asp:TextBox ID="txtISBN" runat="server" Width="74%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    Autor:&nbsp;
                </td>
                <td colspan="3" style="text-align: left; ">
                    <asp:TextBox ID="txtAutor" runat="server" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    Editora:&nbsp;
                </td>
                <td colspan="3" style="text-align: left; ">
                    <asp:TextBox ID="txtEditora" runat="server" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvYear" runat="server" ErrorMessage="O ano de publicação é obrigatório."
                        ControlToValidate="txtYear" Text="*"></asp:RequiredFieldValidator>
                    Ano de&nbsp;<br />
                    Publicação:&nbsp;
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtYear" runat="server" Width="70%" onkeypress="return validateInt(this, event);" MaxLength="4"></asp:TextBox>
                </td>
                <td class="name">
                    Nº da Edição:&nbsp;
                </td>
                <td style="text-align: left; ">
                    <asp:TextBox ID="txtEdition" runat="server" Width="74%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvValue" runat="server" ControlToValidate="txtValue"
                        Text="*" ErrorMessage="O preço é obrigatório."></asp:RequiredFieldValidator>
                    Valor:&nbsp;
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtValue" runat="server" Width="70%" onkeypress="return validateValue(this, event);" MaxLength="10"></asp:TextBox>
                </td>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="A quantidade do produto é obrigatória."
                        ControlToValidate="txtQuantity" Text="*"></asp:RequiredFieldValidator>
                    Quantidade:&nbsp;
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtQuantity" runat="server" Width="74%" onkeypress="return validateInt(this, event);"
                        MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 130px; ">
                <td class="name">
                    <asp:CustomValidator ID="cvImage" runat="server" ErrorMessage="" Text="*" 
                        onservervalidate="cvImage_ServerValidate"></asp:CustomValidator>
                    Imagem:&nbsp;
                </td>
                <td style="text-align: left; ">
                    <asp:FileUpload ID="FileUploadImage" Width="90%" runat="server" onchange="return upload(event);" />
                    <asp:ImageButton ID="btnUpload" runat="server" Width="0px" Height="0px" CausesValidation="false"
                        OnClick="btnUpload_Click" />
                </td>
                <td colspan="2">
                    <asp:Image ID="imgPhoto" runat="server" Width="120px" Height="188px"  />
                </td>

            </tr>
        </table>
    </div>
    <div style="float: right; height: 20px; clear: both;"></div>
</asp:Content>
