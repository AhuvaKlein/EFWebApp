$(() => {

    $('#like').on('click', function () {

        console.log('hello');
        $.post('/home/IncreaseLike', { id: $(this).data('id') }, function () {
            $('#like').prop('disabled', true);
        });

    });

    setInterval(() => {
        $.get('/home/numberoflikes', { id: $('#number-of-likes').data('id') }, function (likes) {
            document.getElementById('number-of-likes').innerHTML = likes;
        })
    }, 1000);

    


})