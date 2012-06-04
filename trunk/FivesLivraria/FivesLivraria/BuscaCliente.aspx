<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuscaCliente.aspx.cs" Inherits="FivesLivraria.BuscaCliente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    Tipo de Busca:&nbsp;
                    <asp:DropDownList ID="dropSearchType" runat="server">
                        <asp:ListItem Text="CPF/CNPJ" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Nome" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtSearchField" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="grdClientes" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnIdCliente" runat="server" Value='<%# Eval("idCliente") %>' />
                                    <%# Eval("nmCliente") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
