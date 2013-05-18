
$(document).ready(
    function () {
        $('form#form-create-note').on('submit', function (e) {
            e.preventDefault();
            $('input#button-create-note').attr('disabled', 'disabled');

            /*
            $.ajax(
            {
            cache: false,
            async: true,
            type: "POST",
            url: $(this).attr('action'),
            data: $(this).serialize(),
            success: function (data) {
            //$('#result').empty();
            //$('#result').html(data);
            },
            complete: function () {
            $('input#button-create-note').removeAttr('disabled');
            }
            }
            );
            */

            /*
            var viewModel = new Object();
            viewModel.Data = "fawsrfawergsaergserfg";
            viewModel.IdNoteType = 1;
            console.log(JSON.stringify({ "Data": "ewrgerg", IdNoteType: "1" }));

            $.ajax({
            url: $(this).attr('action'),
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(viewModel),
            dataType: 'json',
            success: function (result) {
            // TODO:
            },
            complete: function () {
            $('input#button-create-note').removeAttr('disabled');
            }
            });
            */

            var data = $("textarea#Data").val();
            var idNoteType = $("select#IdNoteType :selected").val();

            $.ajax({
                url: $(this).attr('action'),
                type: 'POST',
                dataType: 'json',
                data: { "data": data, "idNoteType": idNoteType },
                success: function (result) {
                    // TODO:
                },
                complete: function () {
                    $('input#button-create-note').removeAttr('disabled');
                }
            });

        }
        );
    }
);