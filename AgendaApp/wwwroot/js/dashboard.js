$(document).ready(function () {

    var dashboardContainer = $('#dashboard-container');
    var ajaxLoader = $('.ajax-loader');

    getAgendas();

    function getAgendas() {

        ajaxLoader.show();

        $.ajax({
            url: 'Profile/GetAgendas',
            type: 'GET',
            success: function (result) {
                ajaxLoader.hide();
                dashboardContainer.html(result);
            },
            error: function () {
                ajaxLoader.hide();
                dashboardContainer.empty();
                alert('Something went wrong');
            }
        });
    }

    $(document).on('change', '.item-checkbox', function () {
        var id = $(this).prop('id');
        checkItem(id);
    });

    function checkItem(id) {
        $.ajax({
            url: 'Item/Check/' + id,
            type: 'POST',
            success: function (result) {
                getAgendas();
            },
            error: function () {
                alert('Something went wrong');
            }
        });
    }

});