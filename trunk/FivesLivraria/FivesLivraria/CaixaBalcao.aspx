<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CaixaBalcao.aspx.cs" Inherits="FivesLivraria.CaixaBalcao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <p>
        <br />
    </p>
    <p>
        <asp:Label ID="lblNome" 
            style="position:absolute; top: 227px; left: 121px; width: 65px;" runat="server" 
            Text="Nome: " ></asp:Label>
        <asp:Label ID="lblArea_Cupom" 
            style="position:absolute; top: 181px; left: 848px;" runat="server" 
            Text="Simulação Cupom"></asp:Label>
    </p>
    <p>
        <asp:TextBox ID="box_NomeCliente" 
            style="position:absolute; top: 225px; left: 185px;" runat="server"></asp:TextBox>
        <asp:TextBox ID="box_CPFCliente" 
            style="position:absolute; top: 259px; left: 183px;" runat="server"></asp:TextBox>
    <asp:RadioButtonList ID="ListFrmPgto" runat="server" AutoPostBack="True" 
        Height="83px" Width="97px" 
        style="position:absolute; top: 220px; left: 359px;" 
            onselectedindexchanged="ListFrmPgto_SelectedIndexChanged">
        <asp:ListItem Selected="True" Value="0">Dinheiro</asp:ListItem>
        <asp:ListItem Value="1">Crédito</asp:ListItem>
        <asp:ListItem Value="2">Débito</asp:ListItem>
    </asp:RadioButtonList>
        
        <asp:Label ID="lblFrmPgto" style="position:absolute; top: 200px; left: 352px; margin-top: 0px;" 
            runat="server" Text="Forma de Pagamento"></asp:Label>
        <asp:Label ID="lblCartao" style="position:absolute; top: 200px; left: 578px;" 
            runat="server" Text="Dados Cartão" Visible="False"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblCPF" 
            style="position:absolute; top: 259px; left: 119px; width: 62px;" runat="server" 
            Text="CPF"></asp:Label>
        <asp:Label ID="lblNumCartao" 
            style="position:absolute; top: 229px; left: 499px;" runat="server" 
            Text="Numero Cartão" Visible="False"></asp:Label>
        <asp:Label ID="lblCodCartao" 
            style="position:absolute; top: 262px; left: 500px; height: 15px; width: 103px;" 
            runat="server" Text="Codigo Cartão" Visible="False"></asp:Label>

        <asp:TextBox ID="txtNumCartao" 
            style="position:absolute; top: 229px; left: 601px; width: 196px;" 
            runat="server" MaxLength="16" Rows="1" Visible="False" ></asp:TextBox>
        <asp:TextBox ID="txtCodCartao" 
            style="position:absolute; top: 258px; left: 604px; width: 59px; margin-top: 0px;" 
            runat="server" MaxLength="3" Rows="1" Visible="False"></asp:TextBox>

    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:DropDownList ID="listProdutosTeste" 
            style="position:absolute; top: 327px; left: 233px; width: 210px;" 
            runat="server" >
            <asp:ListItem Value="1">Produto 01</asp:ListItem>
            <asp:ListItem Value="2">Produto 02</asp:ListItem>
            <asp:ListItem Value="03">Produto 03</asp:ListItem>
            <asp:ListItem Value="04">Produto 04</asp:ListItem>
            <asp:ListItem Value="05">Produto 05</asp:ListItem>
        </asp:DropDownList>
        
        <textarea id="area_Cupom" runat="server" rows="1000" cols="60"
            style="position:absolute; height: 215px; top: 197px; left: 815px; width: 420px;" 
            name="S1"></textarea>
        
        <asp:Button ID="btnPedido" style="position:absolute; top: 327px; left: 619px;" 
            runat="server" Text="Finalizar" 
            Width="105px" onclick="btnPedido_Click" />


        <asp:Button ID="btnItem" runat="server" 
            style="position:absolute; top: 326px; left: 477px;" Text="Adicionar" 
            onclick="btnItem_Click" />

    </p>
        <textarea id="area_TEF" runat="server" rows="1000" cols="60"
        style="position:absolute; top: 453px; left: 815px; width: 420px; height: 99px;" 
        name="S1"></textarea>
        
        <asp:Table ID="tblItensTeste" style="position:absolute; top: 381px; left: 208px; width: 517px;" 
        runat="server" GridLines="Horizontal">
            <asp:TableHeaderRow> 
                <asp:TableHeaderCell Text="Código"> </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="Produto"> </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="Valor"> </asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>


<br />
<br />

<br />
<br />
<br />
    <asp:Label ID="lblArea_TEF" style="position:absolute; top: 434px; left: 849px;" 
        runat="server" Text="Simulação TEF"></asp:Label>
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />

</asp:Content>
