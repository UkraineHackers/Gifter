$(document).ready(function () {
    $('#submit').click(function (e) {
        e.preventDefault();
        var $form = $(this).closest('form');
        $.post($form.attr('aciton'), $form.serialize(),
            function (data) {
                console.log(data);
            }
        );
    });
});