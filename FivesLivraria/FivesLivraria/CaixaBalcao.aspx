<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CaixaBalcao.aspx.cs" Inherits="FivesLivraria.CaixaBalcao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #TextArea1
        {
            width: 524px;
            top: 151px;
            left: 687px;
            height: 266px;
        }
        #areaTEF
        {
            top: 553px;
            left: 797px;
            height: 213px;
        }
    </style>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="Pedido">
        <asp:Label ID="lbl_NomeCliente" runat="server"  
            style="position:absolute; top: 221px; left: 15px; margin-top: 0px;"> Nome: </asp:Label>

        <asp:TextBox ID="box_NomeCliente" runat="server" 
            style="position:absolute; top: 220px; left: 63px; margin-top: 0px;"> </asp:TextBox>
        
        <asp:Label ID="lbl_CPFCliente" runat="server" 
            style="position:absolute; top: 250px; left: 15px; margin-top: 20px;"> CPF: </asp:Label>

        <asp:TextBox ID="box_CPFCliente" runat="server" 
            style="position:absolute; top: 251px; left: 63px; margin-top: 20px;"> </asp:TextBox>
        
        <asp:Button ID="btnPedido" runat="server" Text="Gerar Pedido"
            style="position:absolute; top: 182px; left: 542px;" OnClick="btnPedido_onClick" />

        <asp:RadioButtonList ID="ListFrmPgto" runat="server" 
            style="position:absolute; top: 206px; left: 315px; margin-top: 4px;">
                <asp:ListItem Value="1" Selected="True">Dinheiro</asp:ListItem>
                <asp:ListItem Value="2">Crédito</asp:ListItem>
                <asp:ListItem Value="3">Débito</asp:ListItem>
        </asp:RadioButtonList>

        <asp:Label ID="lbl_Pgto" runat="server" Text="Forma de Pagamento" 
            style="position:absolute; top: 183px; left: 295px;"/>
</div>
<div id="itensPedido">
        <asp:Label ID="Label1" runat="server" Text="Label" 
            style="position:absolute; top: 332px; left: 35px; width: 136px; height: 15px; right: 833px;"> Escolha o Item: </asp:Label>

       <asp:Button id="btnItem" runat="server" Text="Adicionar Item" style="position:absolute; top: 326px; left: 328px;" 
            OnClick="btnItem_onClick" />

        <asp:DropDownList ID="listProdutosTeste" runat="server" 
                style="position:absolute; top: 332px; left: 184px;" >
            <asp:ListItem Selected="True" Value="1">Prod 01</asp:ListItem>
            <asp:ListItem Value="2">Prod 02</asp:ListItem>
            <asp:ListItem Value="3">Prod 03</asp:ListItem>
            <asp:ListItem Value="4">Prod 04</asp:ListItem>
            <asp:ListItem Value="5">Prod 05</asp:ListItem>
        </asp:DropDownList>

        <asp:Table id="tbl_Itens" runat="server" style="position:absolute; top: 405px; left: 16px; width: 558px;"
            CellPadding="0"
            GridLines="Horizontal"
            HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell> Código </asp:TableCell>
                <asp:TableCell> Produto </asp:TableCell>
                <asp:TableCell> Valor </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
</div>

<div id="areas">
        <textarea id="area_TEF" name="S2" cols="30" readonly="readonly" rows="1000" runat="server"
            style="background-color: #66CCFF; position: absolute; top: 503px; left: 821px; height: 115px;" 
            title="Comprovantes TEF" visible="False"></textarea>

        <textarea id="area_Cupom" name="S1" readonly="readonly" rows="1000" cols="50" 
            title="Cupons Impressos" runat="server"
            
            style="background-color: #FFFFCC; position: absolute; top: 146px; left: 735px; width: 464px; height: 344px;" 
            visible="False">
       </textarea>
</div>

</asp:Content>
