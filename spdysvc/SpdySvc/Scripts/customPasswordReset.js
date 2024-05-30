
$("#regForm").validate({
    submitHandler: function () {
        Submit();
    },
    rules: {
  
        txtnewpass: {
            required: true,
         
        },
        txtconfirmpass: {
            required: true,
            equalTo: "#txtnewpass"
        },

    },
    messages: {
        txtnewpass: {
            required: "Please provide Password",
            minlength: 5
        },
        txtconfirmpass: {
            required: "Please provide again password",
            equalTo: "Password doesn't Match"
        },
    }
});


function Submit() {
    var urlObj = new ServiceUrl("Account", "ChangePassword");

    var itemM = {};
    itemM["Password"] = $("#txtnewpass").val();
    itemM["ConfirmPassword"] = $("#txtconfirmpass").val();
    itemM["Email"] = $("#txtEmail").val();
    var datas = new AccResetModel();
     $.ajax({
        url: urlObj.GenerateUrl(),
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        data: JSON.stringify({ changepasswordmdl: JSON.stringify(datas) }),
        success: function (s) {
            if (s.contains("Sucess")) {
                $("#lblMsg").text("Successfully Updated your Password");
                $("#lblMsg").css("color", "Green");
            }


        }
    });
}

function AccResetModel()
{
    this.Password=$("#txtnewpass").val();
    this.ConfirmPassword=$("#txtconfirmpass").val();
    this.Email = $("#txtEmail").val();
}
