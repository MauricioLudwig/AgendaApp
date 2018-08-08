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
            error: function () {
                agendaContainer.empty();
                alert('Something went wrong.');
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
            success: function (result) {
                getAgendas();
            },
            error: function (error) {
                alert('Something went wrong.');
            }
        });

    });

});