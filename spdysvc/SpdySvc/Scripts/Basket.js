$(document).ready(function () {
    $(".button-remove a").on('click', function () {
        var _url = $(this).attr('href');
        var _el = $(this);
        AjaxPageLoader();
        $.ajax({
            url: _url,
            type: 'GET',
            success: function (s) {
                if (s == 'Ok') {
                    _el.closest("tr").remove();
                    getOrderSummery();
                    if ($('table.table-bordered tbody tr').length == 0) {
                        $('table.table-bordered').remove();
                        $('.basket-item').append('<p>Your Basket is empty!</p>');
                    }
                }

            }
        });
        return false;
    });

    //Update Qty to the basket
    $(".quantity").on('keyup', function () {
        var _id = $.trim($(this).attr('id').replace('qty_', ""));
        var _el = $(this);
        var _qty = parseInt($(this).val());
        if (!isInt(_qty)) { alert('Please enter Integer Only!'); return false;}        
        if (_qty <= 0) {
            $(this).val(1);
            return false;
        }
        
    });
    $(".quantity").on('change', function () {
        var _id = $.trim($(this).attr('id').replace('qty_', ""));
        var _el = $(this);
        var _qty = parseInt($(this).val());
        if (_qty <= 0) {
            $(this).val(1);
        }
        var jsonObj = [];
        var mdl = new basketModel(_el);
        jsonObj.push(mdl);
        AjaxPageLoader();
        $.ajax({
            url: "/basket/update",
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            cache: false,
            data: JSON.stringify({ basketMdl: JSON.stringify(mdl) }),
            success: function (s) {
                $('#totalprice_' + _id).html('£' + s);
                getOrderSummery();
            }
        });
        return false;
    });

    function getOrderSummery() {
        $.ajax({
            url: '/basket/orderSummery',
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            cache: false,
            success: function (s) {
                $('#TotalHireValueLabel').html(s.SubTotal);
                $('#TotalCostLabel').html(s.GrandTotal);
            }
        });
    }

    function basketModel(_e) {
        this.ITEM_ID = $.trim(_e.attr('id').replace('qty_', ""));;
        this.Quantity = _e.val();
    }

    getOrderSummery();

});

function isInt(value) {
    return !isNaN(value) && (function (x) { return (x | 0) === x; })(parseFloat(value))
}