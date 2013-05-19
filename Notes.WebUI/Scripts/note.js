
$(document).ready(
    function () {
        $('form#form-create-note').on('submit', function (e) {
            e.preventDefault();
            $('input#button-create-note').attr('disabled', 'disabled');

            //var val = $('form#form-create-note').validate();
            //alert(val.showErrors());

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

        $('form.form-change-node-status-to-history').on('submit', function (e) {
            e.preventDefault();

            var form = $(this).parent();
            var idNote = form.find('input:hidden').val();

            $.ajax({
                url: $(this).attr('action'),
                type: 'POST',
                dataType: 'json',
                data: { "idNote": idNote },
                success: function (result) {
                    if (result.status == "success") {
                        var tr = form.closest('tr');
                        tr.remove();
                    } else {
                        alert("Отказано в доступе!");
                    }
                }
            });

        }
        );
    }
);