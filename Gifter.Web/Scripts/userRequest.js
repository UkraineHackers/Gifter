$(document).ready(function () {
    $('#submit').click(function (e) {
        e.preventDefault();
        var $form = $(this).closest('form');
        $.post($form.attr('action'), $form.serialize(),
            function (data) {
                data = data.result;
                $("#results").empty();
                for (var i = 0; i < data.length; i++) {
                    var $cloned = $(".progressTemplate").clone();
                    $cloned.removeClass('progressTemplate hidden');
                    $cloned.find('.progress-bar').attr('style', "width: " + data[i].percentage + "%");
                    $("#results").append($cloned);
                    $cloned.find('.progress-bar').text(data[i].name);
                }
            }
        );
    });
});