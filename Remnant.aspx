<%@ Page Title="Просмотр остатков" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Remnant.aspx.cs" Inherits="OptBaseNew.Remnant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="warning-container">
        <asp:Label ID="Label_Warning" runat="server" Text="У вас недостаточно прав для просмотра данной страницы." CssClass="warning" Visible="False"></asp:Label>
    </div>
    <form id="form1" runat="server" visible="False">
        <div id="materialContainer">
            <h2>Просмотр остатков материалов</h2>
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
                    <asp:TemplateField HeaderText="Остаток на базе, шт">
                        <ItemTemplate>
                            <%# Eval("MatRemainder") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</asp:Content>
