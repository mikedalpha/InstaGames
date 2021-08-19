
jQuery('.addToList').click(function (e) {
    let icon = $(this);
    e.preventDefault();
    AddToList(userId, $(this).data('id'), icon);
});

function AddToList(id, gameId, icon) {
  
    let url = `https://localhost:44369/api/Account/?id=${id}&gameid=${gameId}`;
    $.ajax({
        type: "PUT",
        url: url,
        dataType: "json",
        success: function (response) {

            if (icon.find('i').hasClass('ri-add-line')) {

                icon.find('i').removeClass('ri-add-line');
                icon.find('i').addClass('fa fa-check-circle-o');
              
            } else {
                icon.find('i').removeClass('fa fa-check-circle-o');
                icon.find('i').addClass('ri-add-line');
               
            }
        }
    });
}