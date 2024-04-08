<%@ Page Title="Авторизация" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OptBaseNew.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <div>
            <table class="auth-table">
                <tr class="auth-info">
                    <td colspan="2">
                        <h1>Форма авторизации работника</h1>
                    </td>
                </tr>
                <tr class="auth-info">
                    <td colspan="2">
                        Логин и пароль чувствительны к регистру.
                    </td>
                </tr>
                <tr>
                    <td class="auth-lefttext">
                        <asp:Label ID="Label1" runat="server" Text="Логин:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Text_Username" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auth-lefttext">
                        <asp:Label ID="Label2" runat="server" Text="Пароль:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Text_Password" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr class="auth-info">
                    <td colspan="2">
                        <asp:Button ID="Button_Login" runat="server" Text="Авторизоваться" OnClientClick="return false;" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="auth-errormsg">
                        <asp:Label ID="Label_ErrorMessage" runat="server" Text="Ошибка ввода логина или пароля!" style="display:none;"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>