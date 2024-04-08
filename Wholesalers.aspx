<%@ Page Title="Справочник оптовиков" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Wholesalers.aspx.cs" Inherits="OptBaseNew.Wholesalers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="warning-container">
        <asp:Label ID="Label_Warning" runat="server" Text="У вас недостаточно прав для просмотра данной страницы." CssClass="warning" Visible="False"></asp:Label>
    </div>
    <form id="form1" runat="server" visible="False">
        <div id="materialContainer" class="wholesalers-container">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Фотография">
                        <ItemTemplate>
                            <img class="wholesalers-prof-pic" src='<%# Eval("UserPicUrl") %>' alt="Изображение профиля" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Информация">
                        <ItemTemplate>
                            <div>
                                <b>Логин в базе:</b> <%# Eval("UserLogin") %><br />
                                <b>Полное ФИО:</b> <%# Eval("UserFullName") %><br />
                                <b>Дата рождения:</b> <%# Eval("FormattedUserBirthdate") %>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="addUserForm">
            <h2>Создать учетную запись для нового оптовика</h2>
            <table class="wholesalers-add-table">
                <tr>
                    <td>Логин:</td>
                    <td><asp:TextBox ID="txtUserLogin" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Пароль:</td>
                    <td><asp:TextBox ID="txtUserPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Полное имя:</td>
                    <td><asp:TextBox ID="txtUserFullName" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Ссылка на изображение:</td>
                    <td><asp:TextBox ID="txtUserPicUrl" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Дата рождения:</td>
                    <td><asp:TextBox ID="txtUserBirthdate" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" class="user-submit">
                        <button type="button" id="Button_AddUser">Зарегистрировать</button>
                    </td>
                </tr>
                <tr style="display:none;" id="Label_Message_Box">
                    <td colspan="2" class="user-submit">
                        <asp:Label ID="Label_Message" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>
