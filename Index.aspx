<%@ Page Title="Справочник материалов" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="OptBaseNew.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="warning-container">
        <asp:Label ID="Label_Warning" runat="server" Text="У вас недостаточно прав для просмотра данной страницы." CssClass="warning" Visible="False"></asp:Label>
    </div>
    <form id="form1" runat="server" visible="False">
        <div id="materialContainer">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
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
                    <asp:TemplateField HeaderText="Закупочная цена, руб">
                        <ItemTemplate>
                            <%# Eval("MatBuyPrice") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Цена продажи, руб">
                        <ItemTemplate>
                            <%# Eval("MatSellPrice") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Оптовая цена продажи, руб">
                        <ItemTemplate>
                            <%# Eval("MatOptSellPrice") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Остаток, шт">
                        <ItemTemplate>
                            <%# Eval("MatRemainder") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="addMaterialForm">
            <h2>Добавление нового материала</h2>
            <table class="addmaterial-table">
                <tr>
                    <td>Название:</td>
                    <td><asp:TextBox ID="txtMatName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Описание:</td>
                    <td><asp:TextBox ID="txtMatDesc" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Вес, кг:</td>
                    <td><asp:TextBox ID="txtMatWeight" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Размеры (ШxВxГ), мм:</td>
                    <td><asp:TextBox ID="txtMatSizes" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Закупочная цена, руб:</td>
                    <td><asp:TextBox ID="txtMatBuyPrice" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Цена продажи, руб:</td>
                    <td><asp:TextBox ID="txtMatSellPrice" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Цена оптовая, руб:</td>
                    <td><asp:TextBox ID="txtMatOptSellPrice" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" class="material-submit">
                        <button type="button" id="Button_AddMaterial">Добавить материал</button>
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