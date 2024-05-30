//Anis 19/04/2015

function ChangeStatus(OrderIDListJsonString, OpCode) {
    var selected = [];
    $('input:checked').each(function () {
        selected.push($(this).attr('type'));
    });
    if (selected.length > 0)
    {
        $('#myDialog').find('.btn-default').css("display","none");
        $('#myDialog').find('.modal-body').html("Processing...............");
        AjaxPageLoader();
        $.ajax({
            url: '/Hire/ChangeHireStatus',
            type: 'POST',
            dataType: 'json',
            data: { OrderIDListJsonString: OrderIDListJsonString, OpCode: OpCode },
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
    else
    {
        alert(" Please Select at least one  Row ");
    }

}

//Trigger to Approve
function approveAction()
{
    var selected = [];
    $('input:checked').each(function () {
        selected.push($(this).attr('type'));
    });
    if (selected.length > 0) {
        $('#myDialog').find('.modal-body').html(CreateDialog("Approve"));
        $('#myDialog').modal('show');
    }
    else {
        alert(" Please Select at least one  Row ");
    }
    
}

//Trigger to Cancel
function rejectAction()
{
    var selected = [];
    $('input:checked').each(function () {
        selected.push($(this).attr('type'));
    });
    if (selected.length > 0) {
        $('#myDialog').find('.modal-body').html(CreateDialog("Cancel"));
        $('#myDialog').modal('show');
    }
    else {
        alert(" Please Select at least one  Row ");
    }
}
function offHireAction() {
    var selected = [];
    $('input:checked').each(function () {
        selected.push($(this).attr('type'));
    });
    if (selected.length > 0) {
        $('#myDialog').find('.modal-body').html(CreateDialog("OffHire"));
        $('#myDialog').modal('show');
    }
    else {
        alert(" Please Select at least one  Row ");
    }


}
//Batch Approve /Unapprove list Create and Do Action
function createListApprove() {
    var orderIdsArray = [];
    var OderIdList_jsonString;
    var Orders = "Orders You Want To delete </br>";
    //run through each row
    $('table tr').each(function (i, row) {

        // reference all the stuff we need first
        var $row = $(row),

            $checkedBoxes = $row.find('input:checked');

        $checkedBoxes.each(function (i, checkbox) {

            //To take OrderID
            var orderId = $row.find('td:nth-child(2)').html();

            orderIdsArray.push(orderId);
            Orders += " </br>" + orderId;
            //end
        });

    });
   
  
    OrderIdList_jsonString = JSON.stringify(orderIdsArray);
    ChangeStatus(OrderIdList_jsonString, 1);



}
function createListReject() {
    var orderIdsArray = [];
    var OderIdList_jsonString;
    //run through each row
    $('table tr').each(function (i, row) {

        // reference all the stuff we need first
        var $row = $(row),

            $checkedBoxes = $row.find('input:checked');

        $checkedBoxes.each(function (i, checkbox) {

            //To take OrderID
       
            var orderId = $row.find('td:nth-child(2)').html();

            orderIdsArray.push(orderId);

            
            //end
        });

    });

    OrderIdList_jsonString = JSON.stringify(orderIdsArray);
    ChangeStatus(OrderIdList_jsonString, 2);

}

function createListOffHire() {
    var orderIdsArray = [];
    var OderIdList_jsonString;
    var Orders = "Orders You Want To delete </br>";
    //run through each row
    $('table tr').each(function (i, row) {

        // reference all the stuff we need first
        var $row = $(row),

            $checkedBoxes = $row.find('input:checked');

        $checkedBoxes.each(function (i, checkbox) {

            //To take OrderID
            var orderId = $row.find('td:nth-child(2)').html();

            orderIdsArray.push(orderId);
            Orders += " </br>" + orderId;
            //end
        });

    });


    OrderIdList_jsonString = JSON.stringify(orderIdsArray);
    ChangeStatus(OrderIdList_jsonString, 3);

}

//Select All
function SelectAllRow() {

    //run through each row

    if ($('#SelAll').is(':checked')) {
       
        $('input[type="checkbox"]').prop('checked', true);
    }
    else
        $('input[type="checkbox"]').prop('checked', false);


}

function CreateDialog(status)
{
    //alert($('#myDialog').find('.btn-default').length);
    //$('#myDialog').find('.modal-footer').find(".btn-default:nth-child(1)").remove();
    var middleTemplate="";
    $('table tr').each(function (i, row) {

        // reference all the stuff we need first
        var $row = $(row),

            $checkedBoxes = $row.find('input:checked');

        $checkedBoxes.each(function (i, checkbox) {

            //To take OrderID
       
            var orderId = $row.find('td:nth-child(2)').html();
            var hiredate = $row.find('td:nth-child(4)').html();
            var hireOff = $row.find('td:nth-child(5)').html();
            var requester = $row.find('td:nth-child(7)').html();
            var status = $row.find('td:nth-child(6)').html();
            var totalCost = $row.find('td:nth-child(8)').html();
            middleTemplate+=  "  <tr> "+
              "  <td>" + orderId + "</td> " +
              "  <td>"+ hiredate+"</td> "+
              "  <td>" + hireOff + "</td> " +
              "  <td>" + requester + "</td> " +
              "  <td>" + totalCost + "</td> " +
               "  <td>" + status + "</td> " +
              "</tr>";
            
            //end
        });
    });

    var resonTemplate = "";
    if (status == "Cancel")
    {
        resonTemplate = " <p>Why do you cancel this Order? </br></br><textarea rows='3'></textarea></p>";
    }

    var template = "<div class='container'>" +
      "<h2></h2>" +
     " <p>Are You Sure You want To "+status+" Those Following Orders::  " +
     " <table class='table table-striped' style=' width: 64%;'> " +
      "  <thead>" +  
        "  <tr> " +
         "   <th>Order Reference</th> " +
          "  <th>Hire Date</th> " +
          "  <th>Hire Off date</th> " +
              "  <th>Requester </th>" +
                "  <th>Total Cost </th>" +
                "  <th>Status</th>" +
         " </tr> " +
      "  </thead> " +
      "  <tbody> " +


      middleTemplate +



        " </tbody> " +
     " </table> " +
     resonTemplate+
  "  </div> ";
        if (status == "Approve")
        {
            $('#myDialog').find('.modal-footer').prepend("<button type='button' class='btn btn-default' onclick='createListApprove()'>Ok</button>");
        }
        else if(status == "Cancel")
        {
            $('#myDialog').find('.modal-footer').prepend("   <button type='button' class='btn btn-default' onclick='createListReject()'>Ok</button>");
        }
        else if (status == "OffHire") {
            $('#myDialog').find('.modal-footer').prepend("   <button type='button' class='btn btn-default' onclick='createListOffHire()'>Ok</button>");
        }

     
        return template;
}

$('body').on('hidden.bs.modal', '.modal', function () {
    $(this).removeData('bs.modal');
    window.location.reload();
});
//Send Mail


