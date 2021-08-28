//Toggle Modal
jQuery(".detailsView").click(function (e) {
    e.preventDefault();
    $("#detailsModal").modal('toggle');
    FindGame($(this).data('id'));
});

//Find Game
function FindGame(id) {
    let url = "https://localhost:44369/api/game/?";
    $.ajax({
        type: "Get",
        url: url,
        data: { "id": id },
        dataType: "json",
        success: function (response) {
            ModalBodyTemplating(response);
        }
    });
}

//video template
function VideoTemplate(game) {
    let videoTemplate = `
                       <div class="iq-card iq-card-block iq-card-stretch iq-card-height iq-mb-3">
                                       <div id="video">
                                           <video id="bgvid"autoplay loop muted playsinline>
                                               <source src="https://localhost:44369/${game.Trailer}" type="video/mp4"/>
                                           </video>
                                       </div>
                                       <div class="iq-card-body">
                             <h1 class="slider-text big-title title text-uppercase" data-animation-in="fadeInLeft"
                                    data-delay-in="0.6">
                                  ${game.Title}
                           </h1>
                        </div>
                      </div>
                                     `;

    let videoEle = $(videoTemplate);
    return videoEle;
}

//photo template
function ImageTemplate(game) {
    let imageTemplate = `
                     <div class="iq-card iq-card-block iq-card-stretch iq-card-height iq-mb-3">
                              <img src="https://localhost:44369/${game.Photo}" class="w-100" alt="">
                                 <div class="iq-card-body">
                       <h1 class="slider-text big-title title text-uppercase" data-animation-in="fadeInLeft"
                                 data-delay-in="0.6">
                               ${game.Title}
                        </h1>
                     </div>
                   </div>
                        `;

    let imageEle = $(imageTemplate);
    return imageEle;
}


function ModalBodyTemplating(game) {

    let element;
    if (game.Trailer != null) {
        element = VideoTemplate(game);
        $("#videoSection").html(element);
    } else {
        element = ImageTemplate(game);
        $("#videoSection").html(element);
    }


    //Main Modal Content
    let mainModalContent = `
                                       <div class="row align-items-center  h-100">
                                         <div class="col-xl-6 col-lg-12 col-md-12">
                                          <a href="javascript:void(0);">
                                           <div class="channel-logo" data-animation-in="fadeInLeft" data-delay-in="0.5">
                                            <img src="/Content/images/logo.png" class="c-logo" alt="InstaGames">
                                            </div>
                                           </a>

                                         <div class="d-flex flex-wrap align-items-center fadeInLeft animated" data-animation-in="fadeInLeft" style="opacity: 1;">
                                         <div class="slider-ratting d-flex align-items-center mr-4 mt-2 mt-md-3">
                                         <ul class="ratting-start p-0 m-0 list-inline text-primary d-flex align-items-center justify-content-left">
                                          ${DisplayRatingStars(game.Rating)}
                                         </ul>
                                       ${CheckGameRating(game.Rating)}
                                    </div>
                                    <!--Rating Logic End -->

                                    <div class="d-flex align-items-center mt-2 mt-md-3">
                                        <span class="ml-3">Pegi &nbsp;</span>
                                        <img src="https://localhost:44369/${game.Pegi}" alt="Alternate Text" class="p-2 " width="50" />
                                    </div>
                                </div>

                                <p data-animation-in="fadeInUp" data-delay-in="1.2">
                                   ${game.Description}
                                </p>
                                <div class="trending-list" data-wp_object-in="fadeInUp" data-delay-in="1.2">
                                    <div class="text-primary title starring">
                                        Developed By:
                                        <span class="text-body">
                                           ${game.Developers.map(d => `<text>${d.Name}&nbsp;</text>`).join("")}
                                        </span>
                                    </div>
                                    <div class="text-primary title genres">
                                        Categories:
                                        <span class="text-body">
                                            ${game.Categories.map(c => `<text>${c.Type}&nbsp;</text>`).join("")}
                                        </span>
                                    </div>
                                    <div class="text-primary title tag">

                                        Tag: <span class="text-body">${game.Tag}</span>
                                    </div>
                                    <div class="text-primary title tag">

                                        Released: <span class="text-body">${ShowYear(game.ReleaseDate)}</span>
                                    </div>
                                </div>
                                <div class="d-flex align-items-center r-mb-23" data-animation-in="fadeInUp" data-delay-in="1.2">
                                   ${PlayButtonDetailsView(game)}
                                </div>
                            </div>
                        </div>
                                       `;
    let mainModalEle = $(mainModalContent);
    $("#MainModalContent").html(mainModalEle);

    //More Like This template
    $.ajax({
        type: "GET",
        url: "https://localhost:44369/api/game/",
        dataType: "json",
        success: function (response) {
            $("#MoreLikeThis").empty();

            //Filter Logic here
            let filteredGamesByPegi = response.filter(function (g) {
                return g.Pegi == game.Pegi;
            });
            filteredGamesByPegi.forEach(MoreLikeThisTemplate);
        }
    });


}

function MoreLikeThisTemplate(game) {
    let template = `
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
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                               `;
    let MoreLikeThisEle = $(template);
    $("#MoreLikeThis").append(MoreLikeThisEle);
}

//Stars Logic
function DisplayRatingStars(rating) {
    if (rating > 0) {
        let temp = '';
        for (let i = 0; i < Math.floor(rating); i++) {
            temp += '<li><i class="fa fa-star" aria-hidden="true"></i></li>';
        }

        for (let j = rating; j < 5; j++) {
            temp += '<li> <i class="fa fa-star-o" aria-hidden="true"></i></li>';
        }
        return temp;
    } else {
        return ` <li> <i class="fa fa-star-o" aria-hidden="true"></i></li>
                 <li> <i class="fa fa-star-o" aria-hidden="true"></i></li>
                 <li> <i class="fa fa-star-o" aria-hidden="true"></i></li>
                 <li> <i class="fa fa-star-o" aria-hidden="true"></i></li>
                 <li> <i class="fa fa-star-o" aria-hidden="true"></i></li>`;
    }
}

function CheckGameRating(rating) {
    if (rating == 0) {
        return '<span class="text-white ml-2">Unrated</span>';
    } else {
        return `<span class="text-white ml-2">${rating.toFixed(1)}<small style="font-size: 10px">(User Score)</small></span>`;
    }
}

//PlayButton
function PlayButtonDetailsView(game) {

    if (game.IsRealeased) {
        return `<a href="/game/play/${game.GameId}" class="btn btn-hover iq-button">
                                        <i class="fa fa-play mr-2"
                                           aria-hidden="true"></i>Play Now
                                    </a>`;
    }
    else if (game.IsEarlyAccess) {
        return `<a href="/game/play/${game.GameId}" class="btn btn-hover iq-button">
                                        <i class="fa fa-play mr-2"
                                           aria-hidden="true"></i>Play Demo
                                    </a>`;
    } else {
        return '';
    }

}

