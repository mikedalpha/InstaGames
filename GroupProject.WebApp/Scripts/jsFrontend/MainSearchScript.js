﻿//Get Data on Input from Mobile search
$('#MainSearchMobile').on('input', function () {
    let searchInput = $('#MainSearchMobile').val();
    $("#BodyContent").empty();

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        headers: { 'Authorization': `Bearer ${localStorage.getItem('User-Token')}` },
        url: "https://localhost:44369/api/Game",
        dataType: "json"
    }).done((data) => StartSearch(data, searchInput)).fail((error) => Error(error));
});


function Error(error) {
    console.log(error);
    window.location.replace("https://localhost:44384/Error/PageNotFound/");
}

//Get Data on Input from Main search
$('#MainSearch').on('input',
    function () {
        let searchInput = $('#MainSearch').val();
        $("#BodyContent").empty();

        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            headers: { 'Authorization': `Bearer ${localStorage.getItem('User-Token')}` },
            url: "https://localhost:44369/api/Game",
            dataType: "json"
        }).done((data) => StartSearch(data, searchInput)).fail((error) => Error(error));

    });

//Filter
function StartSearch(data, searchInput) {
    let games = data;
    let filteredGames = games;

    if (searchInput) {
        filteredGames = filteredGames.filter(g => g.Title.toUpperCase().includes(searchInput.toUpperCase()));
    }


    if (filteredGames.length > 0) {
        MainTemplate();
        filteredGames.forEach(ViewGames);
    } else {
        NoGamesAvailable();
    }

}

//Make a new HTML View
function MainTemplate() {
    let mainTemplate = `
                       <div class="main-content mt-5 mb-5">
                             <section class="container-fluid seasons">
                                 <div class="tab-content">
                                     <div class="tab-pane fade active show" role="tabpanel">
                                         <div class="block-space">
                                            <nav aria-label="breadcrumb">
                                              <ol class="breadcrumb bg-dark">
                                                 <li class="breadcrumb-item"><a class="text-danger" href="/home/Index">Back</a></li>
                                                 <li class="breadcrumb-item active" aria-current="page">All Games</li>
                                              </ol>
                                           </nav>
                                             <a class="iq-view-all mt-5 mb-5 text-primary" href="/game/singleplayer">View All</a>
                                             <div id="gamesSection" class="row mt-2 mb-5">

                                             </div>
                                          </div>
                                      </div>
                                  </div>
                              </section>
                         </div>
                      `;
    let element = $(mainTemplate);
    $("#BodyContent").append(element);
}


//Return Filtered Games
function ViewGames(game) {
    let gamesTemplate = `
                             <div class="col-1-5 col-md-6 iq-mb-30">
                                 <div class="epi-box">
                                     <div class="epi-img position-relative">
                                         <img src="https://localhost:44369/${game.Photo}" class="img-fluid img-zoom" alt="">
                                         <div class="episode-number text-center">${game.Title}</div>
                                         <div class="episode-play-info">
                                            
                                                ${PlayButtonMainSearch(game)}
                                            
                                         </div>
                                     </div>
                                     <div class="epi-desc p-3">
                                         <div class="d-flex align-items-center justify-content-between">
                                             <span class="text-white">${ShowYear(game.ReleaseDate)}</span>
                                             <img src="https://localhost:44369/${game.Pegi}" width="20" />
                                         </div>
                                             <h6 class="epi-name text-white mb-0">
                                                ${game.Description}
                                             </h6>
                                     </div>
                                 </div>
                             </div>
                                   `;

    let gameElement = $(gamesTemplate);
    $("#gamesSection").append(gameElement);

}

//Return No Data View
function NoGamesAvailable() {
    let template = `
                    <div class="iq-breadcrumb-one  iq-bg-over iq-over-dark-50" style=" background-image: url(/Content/images/about-us/about.jpg);">
                            <div class="container-fluid">
                              <div class="row align-items-center">
                                  <div class="col-sm-12">
                                      <nav aria-label="breadcrumb" class="text-center iq-breadcrumb-two">
                                          <h2 class="title text-danger">Sorry no games available with this title :(</h2>
                                          <ol class="breadcrumb main-bg">
                                              <li class="breadcrumb-item"><a class="btn btn-danger text-white" href="/home/index">Back</a></li>
                                          </ol>
                                      </nav>
                                  </div>
                              </div>
                           </div>
                      </div>`;

    let element = $(template);

    $("#BodyContent").append(element);
}

//Play Button Show or Not
function PlayButtonMainSearch(game) {

    if (game.IsReleased || game.IsEarlyAccess) {
        return `<div id = "playBtn" class="episode-play">
                       <a href="/game/play/${game.GameId}"><i class="fa fa-play"></i></a>
                  </div>`;
    } else {
        return '';
    }

}
       
function ShowYear(date) {
    date = new Date();
    let year = date.getFullYear();
    return year;
}
