﻿//Get Data on Input from Mobile search



$('#MainSearchMobile').on('input', function () {
        let searchInput = $('#MainSearchMobile').val();
        $("#MainContent").empty();

        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "https://localhost:44384/api/Games",
            dataType: "json"
        }).done((data) => StartSearch(data, searchInput)).fail((error) => alert(error));
    });

function Error(error) {
    console.log(error);
    window.location.replace("https://localhost:44384/Error/PageNotFound/");
}


//Get Data on Input from Main search
$('#MainSearch').on('input',
    function () {
        let searchInput = $('#MainSearch').val();
        $("#MainContent").empty();
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "https://localhost:44384/api/Games",
            dataType: "json"
        }).done((data) => StartSearch(data, searchInput));

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
                                  <div class="main-content mt-5">
                                        <section class="container-fluid seasons">
                                            <div class="tab-content">
                                                <div id="episodes" class="tab-pane fade active show" role="tabpanel">
                                                    <div class="block-space">
                                                        <div id="gamesSection" class="row">


                                                       </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </section>
                                   </div>
                               `;
    let element = $(mainTemplate);
    $("#MainContent").append(element);
}


//Return Filtered Games
function ViewGames(game) {
    let gamesTemplate = `
                             <div class="col-1-5 col-md-6 iq-mb-30">
                                 <div class="epi-box">
                                     <div class="epi-img position-relative">
                                         <img src="${game.Photo}" class="img-fluid img-zoom" alt="">
                                         <div class="episode-number text-center">${game.Title}</div>
                                         <div class="episode-play-info">
                                             <div class="episode-play">
                                                 <a href="/game/details/${game.GameId}">
                                                     <i class="fa fa-info"></i>
                                                 </a>
                                             </div>
                                         </div>
                                     </div>
                                     <div class="epi-desc p-3">
                                         <div class="d-flex align-items-center justify-content-between">
                                             <span class="text-white">${game.ReleaseDate}</span>
                                             <img src="${game.Pegi}" width="20" />
                                         </div>
                                         <a href="/game/details/${game.GameId}">
                                             <h6 class="epi-name text-white mb-0">
                                                ${game.Descripition}
                                             </h6>
                                         </a>
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

    $("#MainContent").append(element);
}