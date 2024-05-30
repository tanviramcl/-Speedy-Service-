$("#forgetpasswordForm").validate({
    submitHandler: function () {
        Submit();
    },
    rules: {

        txtEmail: {
            required: true,
            email: true,
            remote: {

                url: "/Account/EmailCheck",
                type: "post",
                data: {
                    Email: function () {
                        return $("#txtEmail").val();
                    }
                }

            }
        }

    },
    messages: {
        txtEmail: {
            required: "Please provide a email",
            remote: "This is not a Valid Email."
            
        },

    }
});

function Submit() {
  
   // var urlObj = new ServiceUrl("Account", "ForgetPassword");
    var itemM = {};
    itemM["Email"] = $("#txtEmail").val();
   

    $.ajax({
        url: '/Account/ForgetPassword',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        data: JSON.stringify({ forgetPasswordFormmdl: JSON.stringify(itemM) }),
        success: function (s) {
          //  AjaxPageLoader();
            if (s.contains("Sucess")) {
                $("#lblMsg").text("Link Send by this Email");
                $("#lblMsg").css("color", "Green");
            }


        }
    });
}

