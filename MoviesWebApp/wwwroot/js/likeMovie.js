$("#likeButton").click(
    function () {
        let $likeButton = $("#likeButton");

        let movieId = $likeButton.data("movie-id");
        let liked = $likeButton.data("liked");

        if (liked === "False") {
            $.ajax({
                type: "POST",
                url: `/Movie/Like`,
                data: { movieId: movieId },
                success: function (result) {
                    $likeButton.addClass("liked");
                    $likeButton.removeClass("unliked");

                    $likeButton.data("liked", "True");
                },
                error: function (xhr) {
                    if (xhr.responseText === "Movie already liked") {
                        $likeButton.addClass("liked");
                        $likeButton.removeClass("unliked");

                        $likeButton.data("liked", "True");
                    }
                }
            });
        }
        else if (liked === "True") {
            $.ajax({
                type: "POST",
                url: `/Movie/Unlike`,
                data: { movieId: movieId },
                success: function (result) {
                    $likeButton.addClass("unliked");
                    $likeButton.removeClass("liked");

                    $likeButton.data("liked", "False");
                }
            });
        }
    }
);