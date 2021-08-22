
jQuery('.addToList').click(function (e) {
    let element = $(this);
    e.preventDefault();
    AddToList(userId, $(this).data('id'), element);
});

function AddToList(id, gameId, element) {
  
    let url = `https://localhost:44369/api/Account/?id=${id}&gameid=${gameId}`;
    $.ajax({
        type: "PUT",
        url: url,
        dataType: "json",
        success: function (response) {

            if (element.find('i').hasClass('ri-add-line')) {

                element.find('i').removeClass('ri-add-line');
                element.find('i').addClass('fa fa-check-circle-o');
              
            } else {
                element.find('i').removeClass('fa fa-check-circle-o');
                element.find('i').addClass('ri-add-line');
            }
        }
    });
}