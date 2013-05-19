
$(document).ready(
    function () {
        $('form#form-create-note').on('submit', function (e) {
            e.preventDefault();
            $('input#button-create-note').attr('disabled', 'disabled');

            //var val = $('form#form-create-note').validate();
            //alert(val.showErrors());

            var data = $("textarea#Data").val();
            var idNoteType = $("select#IdNoteType :selected").val();

            $("#add-note-errors").html("");

            $.ajax({
                url: $(this).attr('action'),
                type: 'POST',
                dataType: 'json',
                data: { "data": data, "idNoteType": idNoteType },
                success: function (result) {
                    if (result.status == "success") {
                        var content = "";
                        if ($.trim(result.noteType) == "link") {
                            content = "<a href=\"" + result.data + "\">" + result.data + "</a>";
                        } else {
                            content = result.data;
                        }

                        var tr = '<tr><td>' + content + '</td><td><form method="post" class="form-change-node-status-to-history" action="/Note/ChangeNodeStatusToHistory" novalidate="novalidate"><input type="hidden" value="' + result.idNote + '" name="idNote" class="idNote"><input type="submit" value="В историю" class="btn btn-danger"></form></td></tr>';
                        $('table.table-striped tr:last').after(tr);
                        $("textarea#Data").val("");
                    } else {
                        var errors = "<ul>";
                        $(result.errors[0].Errors).each(function (index, element) {
                            errors += "<li>";
                            errors += element.ErrorMessage
                            errors += "</li>";
                        });
                        errors += "</ul>";
                        $("#add-note-errors").html(errors);
                        alert("Ошибка добавления заметки!");
                    }
                },
                complete: function () {
                    $('input#button-create-note').removeAttr('disabled');
                }
            });
        }
        );

        $(document).on('submit', 'form.form-change-node-status-to-history', function (e) {
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

        $('form#form-clear-history-note').on('submit', function (e) {
            e.preventDefault();

            $.ajax({
                url: $(this).attr('action'),
                type: 'POST',
                dataType: 'json',
                success: function (result) {
                    if (result.status == "success") {
                        $("table.table-striped").find("tr").remove();
                    } else {
                        alert("Заметок в истории нет!");
                    }
                }
            });
        }
        );
    }
);