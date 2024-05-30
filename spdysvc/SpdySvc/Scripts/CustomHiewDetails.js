
$('.rowDatepicker').each(function () {
    $(this).datepicker();
});
function chageCost(obj) {
    var OutQty = parseInt($(obj).val());
    if ($.isNumeric($(obj).val())) {
        var cost = parseFloat($(obj).parent().prev().prev().html());
        var UnitQty = parseInt($(obj).parent().prev().prev().prev().html());
        var total = cost * UnitQty * OutQty;
        total = total.toFixed(2);
        $(obj).parent().next().next().html(total);
    }
    else {
        alert(" Value Must be  Numeric");
        $(obj).val($(obj).parent().next().next().next().next().next().html());
    }
    var gTotal = 0.00;
    $('.table tr').each(function (key, value) {
        if (key > 0) {
            gTotal = parseFloat($(this).find("td:nth-child(6)").html());
            $("#lblGTotal").html($(this).find("td:nth-child(6)").html());
        }
    });
}


$('#dateRangePicker')
    .datepicker({
        format: 'mm/dd/yyyy',
        startDate: '01/01/2010',
        endDate: '12/30/2020'
    })
    .on('changeDate', function (e) {
        dateChange();
        $('#dateRangeForm').formValidation('revalidateField', 'date');
    });

$('#dateRangePicker2')
    .datepicker({
        format: 'mm/dd/yyyy',
        startDate: '01/01/2010',
        endDate: '12/30/2020'
    })
    .on('changeDate', function (e) {
        dateChange();
        $('#dateRangeForm').formValidation('revalidateField', 'date');
    });
$('#dateRangeForm').formValidation({
    framework: 'bootstrap',
    icon: {
        valid: 'glyphicon glyphicon-ok',
        invalid: 'glyphicon glyphicon-remove',
        validating: 'glyphicon glyphicon-refresh'
    },
    fields: {
        date: {
            validators: {
                notEmpty: {
                    message: 'The date is required'
                },
                date: {
                    format: 'MM/DD/YYYY',
                    min: '01/01/2010',
                    max: '12/30/2020',
                    message: 'The date is not a valid'
                }
            }
        }
    }
});

function dateChange() {
    var d1 = new Date($("#d1").val());
    var d2 = new Date($("#d2").val());
    var d = d2 - d1;
    var minutes = 1000 * 60;
    var hours = minutes * 60;
    var days = hours * 24;
    var years = days * 365;
    var gtotal = 0;
    var y = Math.round(d / days);
    if (y > 0) {
        $('.table tr').each(function (key, value) {
            if (key > 0) {
                var qty = parseFloat($(this).find("td:nth-child(4)").find('input[type=text]').val());
                var cost = parseFloat($(this).find("td:nth-child(2)").html());
                var total = qty * cost * y;
                gtotal = gtotal + total;
                $(this).find("td:nth-child(1)").html(y);
                $(this).find("td:nth-child(5)").html(y + " " + "day");
                $(this).find("td:nth-child(6)").html(total);
                $("#lblGTotal").html(gtotal);
            }
        });
    }
    else {
        alert("Hire date Must Be less then Hire off date");
    }

}


function Submit() {
    var gTotal = parseFloat($("#lblGTotal").html());
    var hLmt = parseFloat($("#txtHLmt").val());
    if (hLmt > gTotal) {
        var master = new TransectionMaster();
        var details = new TransectionDetails();
        AjaxPageLoader();
        $.ajax({

            url: "../Hire/AmmendHireDetails",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            cache: false,
            data: JSON.stringify({ masterTransection: JSON.stringify(master), detailsTransection: JSON.stringify(details) }),
            success: function (s) {
                $("#lblMsg").text("Values are Ammended Successfully");
                $("#lblMsg").css("color", "Green");
                $("html, body").animate({ scrollTop: 0 }, "slow");

            },
            error: function (s) {
                alert("");
            }
        });
    }
    else {
        alert(" Grand Total Can't be exceed then Hire Limit");
    }

}

function TransectionMaster() {
    var tnObj = new TransectionModel();
    var jsonObj = [];
    jsonObj.push(tnObj);
    return jsonObj;
}
function TransectionDetails() {
    var jsonObj = [];
    $('table tr').each(function (key, value) {
        if (key > 0) {
            var objMdl = new TransectionDetailsModel(this);
            jsonObj.push(objMdl);
        }
    });
    return jsonObj;
}

function TransectionModel() {
    this.OrderId = $("#hdnId").val();
    this.OnHireDate = $("#d1").val();
    this.OffHireDate = $("#d2").val();
    this.UserId = $('#UserId').val();
    this.TotalHire = $("#lblGTotal").html();
}

function TransectionDetailsModel(obj) {

    this.OrderId = $("#hdnId").val();
    this.Quantity = $(obj).find('td:nth-child(4)').find('input[type=text]').val();
    this.ProductCode = $(obj).find('td:nth-child(7)').html();
    this.ProductName = $(obj).find('td:nth-child(3)').html();
    this.Value = $(obj).find('td:nth-child(6)').html();
    this.Cost = $(obj).find('td:nth-child(6)').html();
    this.FromDate = $(obj).find('td:nth-child(10)').html();
    this.ToDate = $(obj).find('td:nth-child(11)').find('input[type=text]').val();
}
//Added by Anis 16/4/2015


//visual delete
function RemoveRow(aRow) {
    if ($(".table tr").length > 2) {
        $(aRow).parent().parent().remove();
    }
    else {
        alert(" Your must order with at least one item");
    }
}

//Anis 21/04/2015
$('.rowDatepicker')
 .datepicker({
     format: 'mm/dd/yyyy',
     startDate: '01/01/2010',
     endDate: '12/30/2020'
 })
  .on('changeDate', function (e) {
   dateChange();
   $('#dateRangeForm').formValidation('revalidateField', 'date');
   });

function OnQuantityOrToDateChange(thisRow)
{

    var objItem = new AnItem(thisRow);

    AjaxPageLoader();
    $.ajax({
        url: "../Hire/AcceptData",
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        data:JSON.stringify({objItem: JSON.stringify(objItem)}),
        success: function (s) {
            $("#lblGTotal").html(s);

        },
        error: function (s) {
            alert("");
        }
    });
    
}

function AnItem(CurrentRowElement)
{

    this.ProductCode = $(CurrentRowElement).closest('tr').find('td:nth-child(7)').html();;
    this.Quantity = $(CurrentRowElement).closest('tr').find('td:nth-child(4)').find('input[type=text]').val();
    this.Value = $(CurrentRowElement).closest('tr').find('td:nth-child(6)').html();
    this.ToDate = $(CurrentRowElement).closest('tr').find('td:nth-child(11)').find('input[type=text]').val();
    this.FromDate = $(CurrentRowElement).closest('tr').find('td:nth-child(10)').html();

}