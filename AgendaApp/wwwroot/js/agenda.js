$(document).ready(function () {

    var agendaContainer = $('#agendaContainer');

    var titleInput = $('#titleInput');
    var dateInput = $('#dateInput');
    var addAgendaBtn = $('#addAgendaBtn');

    getAgendas();

    function getAgendas() {
        $.ajax({
            url: 'Agenda/GetUserAgendas',
            type: 'GET',
            success: function (result) {
                agendaContainer.html(result);
            },
            error: function () {
                agendaContainer.html('');
                alert('Something went wrong.');
            }
        });
    }

    addAgendaBtn.on('click', function () {

        var data = {};
        data.title = titleInput.val();
        data.dateInput = dateInput.val();

        $.ajax({
            url: 'Agenda/Create',
            type: 'POST',
            data: { 'model': data },
            success: function (result) {
                getAgendas();
            },
            error: function (error) {
                alert('Something went wrong.');
            }
        });

    });

});