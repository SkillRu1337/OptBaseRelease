$(document).ready(function () {
    $('#MainContent_Button_Login').click(function (e) {
        console.log("Button clicked");
        e.preventDefault();
        
        var username = $('#MainContent_Text_Username').val();
        var password = $('#MainContent_Text_Password').val();

        $.ajax({
            type: "POST",
            url: "Login.aspx/CheckLogin",
            data: JSON.stringify({ username: username, password: password }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                console.log("Success response:", response);
                if (response.hasOwnProperty('d') && response.d === "true") {
                    window.location.href = "Index.aspx";
                } else {
                    $('#MainContent_Label_ErrorMessage').show();
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log("Error:", xhr, textStatus, errorThrown);
                alert('Ошибка: ' + textStatus);
            }
        });
    });
    $("#Button_AddMaterial").click(function () {
        var matName = $("#MainContent_txtMatName").val();
        var matDesc = $("#MainContent_txtMatDesc").val();
        var matWeight = $("#MainContent_txtMatWeight").val();
        var matSizes = $("#MainContent_txtMatSizes").val();
        var matBuyPrice = $("#MainContent_txtMatBuyPrice").val();
        var matSellPrice = $("#MainContent_txtMatSellPrice").val();
        var matOptSellPrice = $("#MainContent_txtMatOptSellPrice").val();

        $.ajax({
            type: "POST",
            url: "Index.aspx/AddMaterial",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                matName: matName,
                matDesc: matDesc,
                matWeight: matWeight,
                matSizes: matSizes,
                matBuyPrice: matBuyPrice,
                matSellPrice: matSellPrice,
                matOptSellPrice: matOptSellPrice
            }),
            dataType: "json",
            success: function (response) {
                if (response.d === "true") {
                    location.reload();
                } else {
                    $("#MainContent_Label_Message").text("Ошибка добавления. Проверьте корректность ввода данных (пустые поля, соответствие типа вводимых данных).");
                    $("#Label_Message_Box").show();
                }
            },
            error: function () {
                $("#MainContent_Label_Message").text("Ошибка выполнения AJAX-запроса.");
                $("#Label_Message_Box").show();
            }
        });
    });
    $("#Button_AddUser").click(function () {
        var userLogin = $("#MainContent_txtUserLogin").val();
        var userPassword = $("#MainContent_txtUserPassword").val();
        var userFullName = $("#MainContent_txtUserFullName").val();
        var userPicUrl = $("#MainContent_txtUserPicUrl").val();
        var userBirthdate = $("#MainContent_txtUserBirthdate").val();

        $.ajax({
            type: "POST",
            url: "Wholesalers.aspx/AddUser",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                userLogin: userLogin,
                userPassword: userPassword,
                userFullName: userFullName,
                userPicUrl: userPicUrl,
                userBirthdate: userBirthdate
            }),
            dataType: "json",
            success: function (response) {
                if (response.d === "true") {
                    location.reload();
                } else {
                    $("#MainContent_Label_Message").text("Ошибка добавления пользователя. Проверьте корректность ввода данных (пустые поля, соответствие типа вводимых данных).");
                    $("#Label_Message_Box").show();
                }
            },
            error: function () {
                $("#MainContent_Label_Message").text("Ошибка выполнения AJAX-запроса.");
                $("#Label_Message_Box").show();
            }
        });
    });
    $("#Button_ShipMaterial").click(function () {
        var shipMaterial = $("#MainContent_DropDownList1").prop("selectedIndex");
        var shipCount = $("#MainContent_txtShipCount").val();
        var shipPartner = $("#MainContent_txtShipPartner").val();

        $.ajax({
            type: "POST",
            url: "Shipment.aspx/ShipMaterial",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                shipMaterial: shipMaterial,
                shipCount: shipCount,
                shipPartner: shipPartner
            }),
            dataType: "json",
            success: function (response) {
                if (response.d === "true") {
                    alert('Материалы успешно отгружены.');
                    location.reload();
                } else if (response.d === "error1") {
                    $("#MainContent_Label_Message").text("Заполнены не все поля. Проверьте корректность заполнения.");
                    $("#Label_Message_Box").show();
                } else if (response.d === "error2") {
                    $("#MainContent_Label_Message").text("Количество отгружаемого материала превышает остаток на складе.");
                    $("#Label_Message_Box").show();
                } else if (response.d === "error3") {
                    $("#MainContent_Label_Message").text("Количество отгружаемого материала не может быть 0 и не должно быть меньше 0.");
                    $("#Label_Message_Box").show();
                } else {
                    $("#MainContent_Label_Message").text("Ошибка: "+response.d);
                    $("#Label_Message_Box").show();
                }
            },
            error: function () {
                $("#MainContent_Label_Message").text("Ошибка выполнения AJAX-запроса.");
                $("#Label_Message_Box").show();
            }
        });
    });
    $("#Button_AcceptMaterial").click(function () {
        var acceptMaterial = $("#MainContent_DropDownList1").prop("selectedIndex");
        var acceptCount = $("#MainContent_txtAcceptCount").val();
        var acceptProvider = $("#MainContent_txtAcceptProvider").val();

        $.ajax({
            type: "POST",
            url: "Acceptance.aspx/AcceptMaterial",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                acceptMaterial: acceptMaterial,
                acceptCount: acceptCount,
                acceptProvider: acceptProvider
            }),
            dataType: "json",
            success: function (response) {
                if (response.d === "true") {
                    alert('Материалы успешно приняты.');
                    location.reload();
                } else if (response.d === "error1") {
                    $("#MainContent_Label_Message").text("Заполнены не все поля. Проверьте корректность заполнения.");
                    $("#Label_Message_Box").show();
                } else if (response.d === "error2") {
                    $("#MainContent_Label_Message").text("Не удалось обновить количество материала в базе.");
                    $("#Label_Message_Box").show();
                } else if (response.d === "error3") {
                    $("#MainContent_Label_Message").text("Количество принимаемого материала не может быть 0 и не должно быть меньше 0.");
                    $("#Label_Message_Box").show();
                } else {
                    $("#MainContent_Label_Message").text("Ошибка: " + response.d);
                    $("#Label_Message_Box").show();
                }
            },
            error: function () {
                $("#MainContent_Label_Message").text("Ошибка выполнения AJAX-запроса.");
                $("#Label_Message_Box").show();
            }
        });
    });
});