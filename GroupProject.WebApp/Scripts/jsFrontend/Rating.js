$(".addRating").click(function () {
    let element = $(this);
    AddRating(userId, element.data('id'), element.attr('value') , element);
});

function AddRating(userId, gameId, rating, element) {

    let ratedGame = {
        Rating: rating,
        ApplicationUser: {
            Id : userId
        },
        Game: {
            GameId: gameId
        }
    }

    let url = 'https://localhost:44369/api/UserGameRatings/AddRating=';
    $.ajax({
        type: "POST",
        headers: { 'Authorization': `Bearer ${localStorage.getItem('User-Token')}` },
        url: url,
        dataType: "json",
        data: ratedGame,
        success: function(response) {
            element.addClass('text-success');
            $(element.parent().parent().siblings('.count-box').removeAttr('hidden'));
            $(".show-rating").text(response.Rating);
            element.siblings().removeClass('addRating');
            element.siblings().removeClass('btn-link');
            element.parent().parent().attr('style',`--text:'Successfully rated! Your rating :${response.Rating}'`);
        }
    });
}
