$(".addRating").click(function () {
    let element = $(this);
    AddRating(userId, element.data('id'), element.attr('value') , element);
});

function AddRating(userId, gameId, rating, element) {

    let url = `https://localhost:44369/api/UserGameRatings/AddRating?userId=${userId}&gameId=${gameId}&rating=${rating}`;
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        success: function(response) {
            element.addClass('text-success');
            $(element.parent().parent().siblings('.count-box').removeAttr('hidden'));
            $(".show-rating").text(response.Rating);
        }
    });
}
