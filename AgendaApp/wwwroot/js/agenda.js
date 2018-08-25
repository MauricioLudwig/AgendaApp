$(document).ready(function () {

    var agendaContainer = $('#agenda-container');

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
            error: function (exception) {
                agendaContainer.empty();
                alert(exception);
            }
        });
    }

    addAgendaBtn.on('click', function () {

        var data = {};
        data.title = titleInput.val();
        data.deadline = dateInput.val();

        $.ajax({
            url: 'Agenda/Create',
            type: 'POST',
            data: { 'model': data },
            success: function () {
                getAgendas();
            },
            error: function (exception) {
                alert(exception);
            }
        });

    });

});