$(document).ready(function () {

    var descriptionInput = $('#descriptionInput');
    var categorySelect = $('#categorySelect');
    var addItem = $('#addItem');

    addItem.on('click', function () {

    });

    function postNewItem() {

        var data = {};
        data.description = descriptionInput.val();
        data.categorySelect = categorySelect.val();

        $.ajax({
            url: 'Item/Create',
            type: 'POST',
            success: function (res) {

            },
            error: function () {

            }
        });

    }

    function getAgendaItems() {
        $.ajax({
            url: 'Item/GetAll/',
            type: 'POST'
        })
    }

});