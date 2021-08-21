$(".addRating").click(function () {
    let element = $(this);
    AddRating(userId, element.data('id'), element.attr('value'));
});

function AddRating(userId, gameId, rating) {

    let url = `https://localhost:44369/api/UserGameRatings/AddRating?userId=${userId}&gameId=${gameId}&rating=${rating}`;
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json"
    });
}