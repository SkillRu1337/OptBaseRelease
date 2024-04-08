<%@ Page Title="Отгрузка материалов" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Shipment.aspx.cs" Inherits="OptBaseNew.Shipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="warning-container">
        <asp:Label ID="Label_Warning" runat="server" Text="У вас недостаточно прав для просмотра данной страницы." CssClass="warning" Visible="False"></asp:Label>
    </div>
    <form id="form1" runat="server" visible="False">
        <div id="materialContainer">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <%# Eval("MatID") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Название материала">
                        <ItemTemplate>
                            <%# Eval("MatName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Описание">
                        <ItemTemplate>
                            <%# Eval("MatDescription") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Вес, кг">
                        <ItemTemplate>
                            <%# Eval("MatWeight") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Размеры, мм">
                        <ItemTemplate>
                            <%# Eval("MatSizes") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Остаток на базе, шт">
                        <ItemTemplate>
                            <%# Eval("MatRemainder") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="addMaterialForm">
            <h2>Отгрузка материалов</h2>
            <table class="ship-material-table">
                <tr>
                    <td>Материал:</td>
                    <td><asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Количество:</td>
                    <td><asp:TextBox ID="txtShipCount" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Кому отгружаем:</td>
                    <td><asp:TextBox ID="txtShipPartner" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" class="material-submit">
                        <button type="button" id="Button_ShipMaterial">Отгрузить материалы</button>
                    </td>
                </tr>
                <tr style="display:none;" id="Label_Message_Box">
                    <td colspan="2" class="material-submit">
                        <asp:Label ID="Label_Message" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>
