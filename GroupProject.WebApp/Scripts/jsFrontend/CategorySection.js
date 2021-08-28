//Populate DropDown Menu With All Categories
$.ajax({
    type: "GET",
    contentType: "application/json; charset=utf-8",
    url: "https://localhost:44369/api/Category",
    dataType: "json",
    success: function (response) {
        response.forEach(ShowCategories);
    }
});

function ShowCategories(category) {
    let temp =
        `<a id="${category.CategoryId}" onclick="GetCategory(this.id)" class="dropdown-item" href="#">${category.Type}</a>`;
    let ele = $(temp);
    $(".dropdown-menu").append(ele);
}

//Get the Category which the user Clicks
function GetCategory(id) {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "https://localhost:44369/api/Category",
        dataType: "json",
        data: { "id": id },
        success: function (response) {
            $("#BodyContent").empty();
            MainCatTemplate(response);
            //Show this category Games
            response.Games.forEach(ViewGamesByCategory);
        }
    });
}

function MainCatTemplate(cat) {
    let mainTemplate = `
                       <div class="main-content mt-5 mb-5">
                             <section class="container-fluid seasons">
                                 <div class="tab-content">
                                     <div class="tab-pane fade active show" role="tabpanel">
                                         <div class="block-space">
                                           <div>
                                             <nav aria-label="breadcrumb">
                                              <ol class="breadcrumb bg-dark">
                                                 <li class="breadcrumb-item"><a class="text-danger" href="/game/singlePlayer">Back</a></li>
                                                 <li class="breadcrumb-item active" aria-current="page">${cat.Type}</li>
                                              </ol>
                                           </nav>
                                               <p class="mb=3">${cat.Description}</p>
                                             <div id="categorySection" class="row mb-5">

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

function ViewGamesByCategory(game) {
    let catTemplate = `
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
                                             <span class="text-white">${game.ReleaseDate}</span>
                                             <img src="https://localhost:44369/${game.Pegi}" width="20" />
                                         </div>
                                             <h6 class="epi-name text-white mb-0">
                                                ${game.Description}
                                             </h6>
                                     </div>
                                 </div>
                             </div>
                                   `;

    let catElement = $(catTemplate);
    $("#categorySection").append(catElement);
}
