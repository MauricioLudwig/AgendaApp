$(document).ready(function () {

    var itemsContainer = $('.itemsContainer');
    var agendaId = itemsContainer.prop('id');
    var descriptionInput = $('#descriptionInput');
    var categorySelect = $('#categorySelect');
    var addItem = $('.addItem');

    getAgendaItems();

    addItem.on('click', function () {
        var id = $(this).prop('id');
        console.log(id);
        postNewItem(id);
    });

    $(document).on('click', '.removeItem', function () {

        var id = $(this).prop('id');

        $.ajax({
            url: '/Item/Delete/' + id,
            type: 'POST',
            success: function (result) {
                getAgendaItems();
            },
            error: function () {
                alert('Something went wrong');
            }
        });

    });

    function postNewItem(id) {

        var data = {};
        data.agendaid = id;
        data.description = descriptionInput.val();
        data.category = categorySelect.val();

        $.ajax({
            url: '/Item/Create',
            type: 'POST',
            data: { 'model': data },
            success: function (res) {
                getAgendaItems();
            },
            error: function () {

            }
        });

    }

    function getAgendaItems() {

        $.ajax({
            url: '/Agenda/GetItems/' + agendaId,
            type: 'POST',
            success: function (result) {
                itemsContainer.html(result);
            },
            error: function () {

            }
        });

    }

});