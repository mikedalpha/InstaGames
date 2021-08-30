﻿$(".addRating").click(function () {
    let element = $(this);
    AddRating(userId, element.data('id'), element.attr('value') , element);
});

function AddRating(userId, gameId, rating, element) {

    let ratedGame = {
        ApplicationUser: {
            Id : userId
        },
        Game: {
            GameId: gameId
        },
        Rating:rating
    }

    let url = 'https://localhost:44369/api/UserGameRatings/AddRating';
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        data: ratedGame,
        success: function(response) {
            element.addClass('text-success');
            $(element.parent().parent().siblings('.count-box').removeAttr('hidden'));
            $(".show-rating").text(response.Rating);
        }
    });
}
