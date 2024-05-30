$("#regForm").validate({
    submitHandler: function () {
        Submit();
    },
    rules: {

        txtFirstName: {
            required: true,

        },
        txtLastName: {
            required: true,
 
        },
        txtPhone:{
            required:true,
        }
    },
    messages: {
        txtFirstName: {
            required: "Please provide Your FirstName ",
       
        },
        txtLastName: {
            required: "Please provide LastName",
           
        },
        txtPhone: {
            required: "Please provide Phone",
        },
    }
});


function Submit() {
    var urlObj = new ServiceUrl("Account", "UpdateAccount");

    var itemM = {};
    itemM["FirstName"] = $("#txtFirstName").val();
    itemM["LastName"] = $("#txtLastName").val();
    itemM["Phone"] = $("#txtPhone").val();

    AjaxPageLoader();
    $.ajax({
        url: urlObj.GenerateUrl(),
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        data: JSON.stringify({ updateaccountmdl: JSON.stringify(itemM) }),
        success: function (s) {
         //   AjaxPageLoader();
            if (s.contains("Success")) {
                $("#lblMsg").text(s);
                $("#lblMsg").css("color", "Green");
            }


        }
    });
}

