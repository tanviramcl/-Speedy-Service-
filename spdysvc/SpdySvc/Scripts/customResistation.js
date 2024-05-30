$("#regForm").validate({
    submitHandler: function () {
        Submit();
    },
    rules: {
        txtfNm: {
            required: true,

        },
        txtlNm: {
            required: true,
        },
        txtemail: {
            required: true,
            email: true,
            remote: {

                url: "/Account/EmailOk",
                type: "post",
                data: {
                    Email: function () {
                        return $("#txtemail").val();
                    }
                }

            }
        },
        txtpass: {
        required: true,

        },
        txtconfirmpass: {
            required: true,
            equalTo: "#txtpass"
        },
        txtphone: {
            required: true,
        },
        txtUname: {
                required: true,
                remote: {
                            
                    url: "/Account/UserOk",
                        type: "post",
                        data: {
                        UserName: function() {
                            return $("#txtUname").val();
                        }
                        }

                }
            }

    },
    messages: {
        txtfNm: {
              required: "Please enter FirstName"
        },
        txtlNm: {
            required: "Please enter LastName",
        },
        txtemail: {
            required: "Please provide a email",
            remote: "This Email is already in use."
        },
        txtcpass: {
            required: "Please provide a Password",
            minlength: 5
        },
        txtconfirmpass: {
            required: "Please provide again password",
            equalTo: "Password doesn't Match"
        },
        txtphone: {
            required: "Please provide a Phone",
        },
        txtUname: {
                    required: "Please enter a username",
                    remote: "This username is already in use."
                }


    }
});


function Submit() {

   // var urlObj = new ServiceUrl("Account", "Resiatation");
    var jsonObjM = [];
    var firstname = $("#txtfNm").val();
    var lastname = $("#txtlNm").val();
    var email = $("#txtemail").val();
    var password = $("#txtpass").val();
    var phone = $("#txtphone").val();
    var uName = $("#txtUname").val();
    var UserRole = $('#role').val();

    var itemM = {};
    itemM["FirstName"] = firstname;
    itemM["LastName"] = lastname;
    itemM["Email"] = email;
    itemM["Password"] = password;
    itemM["PhoneNumber"] = phone;
    itemM["Address"] = "";
    itemM["Role_ID"] = UserRole;

    jsonObjM.push(itemM);




    AjaxPageLoader();
    $.ajax({

        url: '../Account/Registration',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        data: JSON.stringify({ resistationmdl: JSON.stringify(jsonObjM) }),
        success: function (s) {
            // AjaxPageLoader();
            if (s.contains("SUCCESS")) {
                $("#lblMsg-error").hide();
                $("#lblMsg-success").text(s).fadeIn(500);
                //$("#lblMsg").css("color", "Green");

            }
            else {
                $("#lblMsg-success").hide();
                $("#lblMsg-error").text("ERROR:: Email Already Exist").fadeIn(500);
                //$("#lblMsg").css("color", "Red");

            }
        }
    });
}

//Edit user

$("#editForm").validate({
    submitHandler: function () {
        SaveUser();
    },
    rules: {
        txtfNm: {
            required: true,

        },
        txtlNm: {
            required: true,
        },
        txtemail: {
            required: true,
            email: true,
            remote: {

                url: "/Account/EmailOk",
                type: "post",
                data: {
                    Email: function () {
                        return $("#txtemail").val();
                    }
                }

            }
        },
        txtpass: {
            required: true,

        },
        txtconfirmpass: {
            required: true,
            equalTo: "#txtpass"
        },
        txtphone: {
            required: true,
        },
        txtUname: {
            required: true,
            remote: {

                url: "/Account/UserOk",
                type: "post",
                data: {
                    UserName: function () {
                        return $("#txtUname").val();
                    }
                }

            }
        }

    },
    messages: {
        txtfNm: {
            required: "Please enter FirstName"
        },
        txtlNm: {
            required: "Please enter LastName",
        },
        txtemail: {
            required: "Please provide a email",
            remote: "This Email is already in use."
        },
    
        txtcphone: {
            required: "Please provide a Phone",
        },
        txtHireLimit: {
            required: "Please enter a hire limit",
            
        }


    }
});

function SaveUser() {


    var jsonObjUser = [];
    var firstname = $("#txtfNm").val();
    var lastname = $("#txtlNm").val();
    var email = $("#txtemail").val();
    var jobTilte = $("#txtjobTitle").val();
    var phone = $("#txtphone").val();
    var hireLimit = $("#txtHireLimit").val();
    var userId = $("#txtUId").val();

    var itemM = {};
    itemM["FirstName"] = firstname;
    itemM["LastName"] = lastname;
   
    itemM["PhoneNumber"] = phone;
    itemM["Address"] = "";
    itemM["HireLimit"] = hireLimit;
    itemM["JobTitle"] = jobTilte;
    itemM["UserID"] = userId;

    jsonObjUser.push(itemM);




    AjaxPageLoader();
    $.ajax({

        url: '../Users/UpdateUser',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        data: JSON.stringify({ editUser: JSON.stringify(jsonObjUser) }),
        success: function (s) {
       
            if (s.contains("SUCCESS")) {
                $("#lblMsg-error").hide();
                $("#lblMsg-success").text(s).fadeIn(500);
            

            }
            else {
                $("#lblMsg-success").hide();
                
                

            }
        }
    });
}
