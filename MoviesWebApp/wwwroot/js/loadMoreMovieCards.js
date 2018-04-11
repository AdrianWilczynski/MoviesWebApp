let page = 1;

$(function () {
    page = $("#movieCardsContainer").data("starting-page");
});

$("#loadMoreMovieCardsButton").click(function () {
    $.ajax({
        url: `/Page/${++page}`, success: function (result) {
            $("#movieCardsContainer").append(result);
        }
    });
});