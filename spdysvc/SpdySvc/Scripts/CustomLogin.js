$(document).ready(function () {
    $("#regForm").validate({
        submitHandler: function () {
            LogInFrm();
        },
        rules: {
            userName: {
                required: true,
            },
            password: {
                required: true,
            }
        },
        messages: {
            userName: {
                required: "Please enter a username"
            },
            password: {
                required: "Please provide a password"
            }
        }
    });
});

function LogInFrm() {   
    var urlObj = new ServiceUrl("Account", "Login");
    var urlObj2 = new ServiceUrl("Home", "Index");
    var jsonObj = [];
    var item = {};
    AjaxPageLoader();
    var mdl = new loginModel();
    jsonObj.push(mdl);
    $.ajax({
        url: "../Account/Login",
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        data: JSON.stringify({ loginMdl: JSON.stringify(mdl), url: JSON.stringify(mdl) }),
        success: function (s) {
            if (s.contains("SUCCESS")) {
                 window.location.href ="../"+s.split('-')[1]//urlObj2.GenerateUrl();
            } else {
                $("#lblMsg").text("ERROR::Invalid credential").fadeIn(500);
            }


        }
    });
}

function loginModel() {
    this.UserID = $("#userName").val();
    this.Password = $("#password").val();
}



