$(document).ready(function () {

    var dashboardContainer = $('#dashboard-container');

    getAgendas();

    function getAgendas() {
        $.ajax({
            url: 'Profile/GetAgendas',
            type: 'GET',
            success: function (result) {
                dashboardContainer.html(result);
            },
            error: function (exception) {
                dashboardContainer.empty();
                alert(exception);
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
            success: function () {
                getAgendas();
            },
            error: function (exception) {
                alert(exception);
            }
        });
    }

});