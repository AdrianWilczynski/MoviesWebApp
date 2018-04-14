let page = 1;

$(function () {
    page = $("#movieCardsContainer").data("starting-page");
});

$("#loadMoreMovieCardsButton").click(function () {
    $.ajax({
        url: `/Page/${++page}`,
        success: function (result) {
            $("#movieCardsContainer").append(result);
        },
        error: function (xhr) {
            if (xhr.status === 404) {
                $("#loadMoreMovieCardsButton").hide();

                let $allLoadedInfo = $("<div></div>")
                    .text("Wczytano wszystkie filmy, zajrzyj tu ponownie za jakiś czas...")
                    .addClass("all-loaded-info");

                $("#mainContainer").append($allLoadedInfo);
            }
        }
    });
});