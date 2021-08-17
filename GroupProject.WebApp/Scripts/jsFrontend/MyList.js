

jQuery('.addToList').click(function (e) {
    e.preventDefault();
    AddToList(userId, $(this).data('id'));
});


function AddToList(id, gameId) {
    console.log(gameId);
    let url = `https://localhost:44369/api/Account/?id=${id}&gameid=${gameId}`;
    $.ajax({
        type: "PUT",
        url: url,
        dataType: "json",
        success: function (response) {
            
        }
    });
}