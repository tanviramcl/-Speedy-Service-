
function SendUserData() {
    
    createListToChange();
    

}

function UserData()
{
   
    this.UserRole=$('#role').val();

}

function createListToChange() {

    ShowModal();


}



function changeUserRoles()
{

    var userIdsArray = [];
    var roleArray = [];

    var userIdList_jsonString;
    var role_json;


    var UserRole = $('#role').val();

    roleArray.push(UserRole);

    $('table tr').each(function (i, row) {

        // reference all the stuff we need first
        var $row = $(row),

            $checkedBoxes = $row.find('td:nth-child(1)').find('input:checked');

        $checkedBoxes.each(function (i, checkbox) {

            //To take OrderID
            var userId = $row.find('td:nth-child(2)').html();

            userIdsArray.push(userId);
          
            //end
        });

    });


    userIdList_jsonString = JSON.stringify(userIdsArray);

    role_json = JSON.stringify(roleArray);


    AjaxChangeUserRoles(userIdList_jsonString,role_json);

}

function AjaxChangeUserRoles(userIdList_jsonString, role_json)
{
    $.ajax({
        url: '/Users/AssignPermission',
        type: 'POST',
        dataType: 'json',
        data: { userId_list: userIdList_jsonString, role: role_json },
        success: function (data) {
            $('#myDialog').modal('hide');
            location.reload();
            $('input[type="checkbox"]').prop('checked', false);

        },
        error: function () {
            alert('error');
        }
    });


}

function ShowModal() {
    var selected = [];
    $('input:checked').each(function () {
        selected.push($(this).attr('type'));
    });
    if (selected.length > 0) {
        $('#myDialog').find('.modal-body').html(CreateDialog());
        $('#myDialog').modal('show');
    }
    else {
        alert(" Please Select at least one  Row ");
    }

}

function CreateDialog() {
    
   var UserRole = $('#role').val();
   var selectedUserCount=0;
   var roles = ["Admin","Approver","Requestor"];
   var template;
    var middleTemplate = "";
    $('table tr').each(function (i, row) {

        // reference all the stuff we need first
        var $row = $(row),

            $checkedBoxes = $row.find('td:nth-child(1)').find('input:checked');

        $checkedBoxes.each(function (i, checkbox) {

            //To take OrderID

            var userId = $row.find('td:nth-child(2)').html();
            var FirstName = $row.find('td:nth-child(3)').html();
            var Lastname = $row.find('td:nth-child(4)').html();
            var AssignedRole = $row.find('td:nth-child(6)').html();
           
            selectedUserCount++;
           
            middleTemplate += "  <tr> " +
              "  <td>" + userId + "</td> " +
              "  <td>" + FirstName + "</td> " +
              "  <td>" + Lastname + "</td> " +
              "  <td>" + AssignedRole + "</td> " +
              "</tr>";

            //end
        });
    });

    if (selectedUserCount > 0) {

        template = "<div class='container'>" +
          "<h2></h2>" +
         " <p>Are you sure that you want to change assigned role as <font color='#00c'> [ " + roles[UserRole-1] + " ] </font> for the Following Users: " +
         " <table class='table table-striped' style=' width: 69%;'> " +
          "  <thead>" +
            "  <tr> " +
             "   <th>User ID</th> " +
              "  <th>First Name</th> " +
              "  <th>Last Name</th> " +
                  "<th>Currently assigned Role</th>" +
             " </tr> " +
          "  </thead> " +
          "  <tbody> " +


          middleTemplate +



            " </tbody> " +
         " </table> " +

      "  </div> ";

        $('#myDialog').find('.modal-footer').prepend("<button type='button' class='btn btn-default' onclick='changeUserRoles()'>Ok</button>");

    }
    else {

        template = "<div class='container'>" +
          "<h2></h2>" +
         " <p>Please select at least one user.</p>" +

      "  </div> ";
    }
 

    return template;
}

$('body').on('hidden.bs.modal', '.modal', function () {
    $(this).removeData('bs.modal');
    window.location.reload();
});
