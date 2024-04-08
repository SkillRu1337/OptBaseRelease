<%@ Page Title="Архив операций" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Archive.aspx.cs" Inherits="OptBaseNew.Archive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="warning-container">
        <asp:Label ID="Label_Warning" runat="server" Text="У вас недостаточно прав для просмотра данной страницы." CssClass="warning" Visible="False"></asp:Label>
    </div>
    <form id="form1" runat="server" visible="False">
        <div id="materialContainer">
            <h2>Просмотр архива операций</h2>
            <table class="archive-table">
                <tr>
                    <td>
                        Выберите период, за который нужна информация:
                    </td>
                    <td>
                        <asp:TextBox ID="DateFrom" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="DateTo" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="ArchiveRequest" runat="server" Text="Выгрузить архив" OnClick="ArchiveRequest_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Visible="false">
                <Columns>
                    <asp:TemplateField HeaderText="Тип операции">
                        <ItemTemplate>
                            <asp:Label ID="lblOperationType" runat="server" Text='<%# Eval("OperationType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Дата операции">
                        <ItemTemplate>
                            <asp:Label ID="lblOperationDate" runat="server" Text='<%# Eval("OperationDate", "{0:dd.MM.yyyy HH:mm:ss}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Выполнил операцию">
                        <ItemTemplate>
                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Материал">
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialName" runat="server" Text='<%# Eval("MaterialName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Партнер">
                        <ItemTemplate>
                            <asp:Label ID="lblPartner" runat="server" Text='<%# Eval("Partner") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Количество">
                        <ItemTemplate>
                            <asp:Label ID="lblCount" runat="server" Text='<%# Eval("Count") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div id="ErrorMessageBox" class="archive-error-box" runat="server" Visible="false">
                <asp:Label ID="ErrorMessage" runat="server" Text="" CssClass="archive-error"></asp:Label>
            </div>
        </div>
    </form>
</asp:Content>
