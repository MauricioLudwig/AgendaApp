﻿$(document).ready(function () {

    var dashboardContainer = $('#dashboard-container');

    getAgendas();

    function getAgendas() {
        $.ajax({
            url: 'Profile/GetAgendas',
            type: 'GET',
            success: function (result) {
                dashboardContainer.html(result);
            },
            error: function () {
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