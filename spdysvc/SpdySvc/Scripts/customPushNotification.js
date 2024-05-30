$("#pushForm").validate({
    submitHandler: function () {
        Submit();
    },
    rules: {

        txtMessage: {
            required: true,
        }

    },
    messages: {
        txtMessage: {
            required: "Please provide Message",
            minlength: 15
        },
       
    }
});


function Submit() {
    var urlObj = new ServiceUrl("PushNotification", "SendEmail");

    var itemM = {};
    itemM["ProfileId"] = $("#pIDdl").val();
    itemM["Message"] = $("#txtMessage").val();
    
    AjaxPageLoader();
    $.ajax({
        url: urlObj.GenerateUrl(),
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        data: JSON.stringify({ notificationmdl: JSON.stringify(itemM) }),
        success: function (s) {
            AjaxPageLoader();
            if (s.contains("Success")) {
                $("#lblMsg").text(s);
                $("#lblMsg").css("color", "Green");
            }


        }
    });
}