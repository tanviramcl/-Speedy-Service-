$("#collarnotificationForm").validate({
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
    alert();
    var urlObj = new ServiceUrl("PushNotification", "SendCollar");

    var CollarId = $("#clrDdl").val();
    var Message = $("#txtMessage").val();
   
    $.ajax({
        url: urlObj.GenerateUrl(),
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        data: JSON.stringify({ CollarId: CollarId,Message:Message }),
        success: function (s) {
            
            alert();
            if (s.contains("Success")) {
                $("#lblMsg").text(s);
                $("#lblMsg").css("color", "Green");
            }


        }
    });
}

