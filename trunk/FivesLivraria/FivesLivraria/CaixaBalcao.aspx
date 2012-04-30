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
    <p>
        <asp:Label ID="Label1" runat="server" Text="Label" 
        style="position:absolute; top: 191px; left: 83px; width: 136px; height: 15px;"> Escolha o Item: </asp:Label>
    </p>
    <p>
        <select id="listProdutosTeste" runat="server"
            
            style="position:absolute; width: 88px; margin-left: 0px; top: 188px; left: 228px;">
            <option> 1 </option>
            <option> 2 </option>
            <option> 3 </option>
            <option> 4 </option>
            <option> 5 </option>
        </select>
        <textarea id="area_Cupom" name="S1" readonly="readonly" rows="1000" cols="50" 
            title="Cupons Impressos" runat="server"
            style="background-color: #FFFFCC; position: absolute; top: 146px; left: 735px; width: 464px; height: 344px;">
       </textarea>
            
       <asp:Button id="btn_Acao" runat="server" Text="Ação" style="position:absolute; top: 190px; left: 349px;" OnClick="clk_itemPedido" />
            </p>
    <p>
        <br />
    </p>
        <asp:RadioButtonList ID="ListFrmPgto" runat="server" 
        style="position:absolute; top: 223px; left: 111px; margin-top: 4px;">
            <asp:ListItem Value="1" Selected="True">Dinheiro</asp:ListItem>
            <asp:ListItem Value="2">Crédito</asp:ListItem>
            <asp:ListItem Value="3">Débito</asp:ListItem>
        </asp:RadioButtonList>
    <p>
        &nbsp;</p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
        <textarea id="area_TEF" name="S2" cols="30" readonly="readonly" rows="1000" runat="server"
            style="background-color: #66CCFF; position: absolute; top: 503px; left: 821px; height: 115px;" 
            title="Comprovantes TEF"></textarea></p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>
