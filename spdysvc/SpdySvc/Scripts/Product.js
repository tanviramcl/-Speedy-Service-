$(document).ready(function () {    

    var days = 1;
    var lastDate = $('#reservation').val().split('-');
    var startDate = $.trim(lastDate[0]);
    var endDate = $.trim(lastDate[1]);
     

    $('#product_tab a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    })

    //Date range picker
    $('#reservation').daterangepicker({
        locale: {
            fromLabel: 'On hire date',
            toLabel: 'off-hire date'
        }
    }, function (start, end, label) {
        //console.log(start.toISOString(), end.toISOString(), label);
        //console.log(start.format('YYYY-MM-DD'));
        //console.log(end.format('YYYY-MM-DD'));            
        //   var days = DateDiff(start.format('YYYY-MM-DD'), end.format('YYYY-MM-DD'));
        // console.log(days);
        // $('#totaldays').html(days);
    });

    $('#reservation').on('apply.daterangepicker', function (ev, picker) {
        // console.log(picker.startDate.format('YYYY-MM-DD'));
        //console.log(picker.endDate.format('YYYY-MM-DD'));
        var _strt = picker.startDate.format('MM/DD/YYYY');
        var _end = picker.endDate.format('MM/DD/YYYY');
        days = DateDiff(_strt, _end);
        $('#lbltotaldays').html('Total days for hire : <span id="total">' + days + '</span>');
        $('#onhire').val(_strt);
        $('#offhire').val(_end);
        calculatePrice();
    });

    $('#qty').on('blur', function (e) {

        if (!isInt($(this).val()) || $(this).val() <= 0) {
            $("#qty").val(1);
        }
        calculatePrice();
    });

    $("#productForm").on('submit', function () {
        var _id = $('#id').val();
        var _onhire = $('#onhire').val();
        var _offhire = $('#offhire').val();
        var _daterange = $('#reservation').val();
        var _unitprice = parseInt($('#unitprice').val());
        var _totaldays = parseInt($('#totaldays').val());
        var _qty = parseInt($('#qty').val());
        var _totalPrice = _unitprice;
        var jsonObj = [];
        if (_daterange == '') {
            alert('Please select on hire and off hire date!');
            return false;
        }
        if (_qty == '' || _qty == 0) {
            alert('Please enter Quantity!');
            return false;
        }
                
        var mdl = new productModel();
        jsonObj.push(mdl);
        AjaxPageLoader();
        $.ajax({
            url: "/basket/add",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            cache: false,
            data: JSON.stringify({ productMdl: JSON.stringify(mdl)}),
            success: function (s) {
                getTotalBasketItem();
                return false;
            }
        });
        return false;
    });

    calculatePrice();

});


function DateDiff(date1, date2) {
    var date1 = new Date(date1);
    var date2 = new Date(date2);
    date1.setHours(0);
    date1.setMinutes(0, 0, 0);
    date2.setHours(0);
    date2.setMinutes(0, 0, 0);
    var datediff = Math.abs(date1.getTime() - date2.getTime()); // difference 
    var _days = parseInt(datediff / (24 * 60 * 60 * 1000), 10); //Convert values days and return value      
    var days = _days + 1;
    $('#totaldays').val(days);
    return days;
}

function calculatePrice() {

    
    var _id = $('#id').val();
    var _onhire = $('#onhire').val();
    var _offhire = $('#offhire').val();
    var _unitprice = parseInt($('#unitprice').val());
    var _totaldays = parseInt($('#totaldays').val());
    var _qty = parseInt($('#qty').val());
    var _totalPrice = _unitprice;

    if (_onhire && _offhire) {
        days = DateDiff($.trim(_onhire), $.trim(_offhire));
        $('#lbltotaldays').html('Total days for hire : <span id="total">' + days + '</span>');
    } else {
        $('#lbltotaldays').html('');
    }
   

    if (_totaldays > 0) {
        _totalPrice = _unitprice * _totaldays;
    }
    if (_qty > 0) {
        _totalPrice = _totalPrice * _qty;
    }
    //console.log(_totalPrice);

    if (_totalPrice > 0) {
        $('#totalHirePrices').html('£' + _totalPrice)
    }

}

function isInt(value) {
    return !isNaN(value) && (function (x) { return (x | 0) === x; })(parseFloat(value))
}

function productModel() {
    this.ITEM_ID = $('#id').val();
    this.Quantity = $("#qty").val();
    this.FromDate = $("#onhire").val();
    this.ToDate = $("#offhire").val();
    this.Totaldays = $("#totaldays").val();   
}