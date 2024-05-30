$("#hirevalue").validate({
    submitHandler: function () {
        Submit();
    },
    rules: {
        HireValue: {
            number: true,
            required: true,
        },
        LimitPercentage: {
            number: true,
            required: true,
        }

    },
       
    messages: {
        HireValue: {
            required: "Please enter This Field",
            number:"Please Enter a numeric value"
        },
        LimitPercentage: {
            required: "Please enter This Field",
            number: "Please Enter a numeric value"
        }
    }
});



function Submit() {
    var usr = new User();
    AjaxPageLoader();
    $.ajax({

        url: "../Requestor/HireValueChange",
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        data: JSON.stringify({ hireMdl: JSON.stringify(usr) }),
        success: function (s) {
            if (s.contains("Success")) {
                $("#lblMsg").text("Value Updated  Successfully").fadeIn(500);
                $("html, body").animate({ scrollTop: 0 }, "slow");

            }
           
        },
        error: function (s) {
        }
    });
}

function User() {
    this.UserID = $("#hdnUID").val();
    this.HireLimit = $("#HireValue").val();
    this.LimitPercentage = $("#LimitPercentage").val();
}