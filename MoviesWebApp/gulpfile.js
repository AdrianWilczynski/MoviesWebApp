/// <binding BeforeBuild='on-build' />
"use strict";

let gulp = require("gulp");
let rimraf = require("rimraf");
let concat = require("gulp-concat");
let cssmin = require("gulp-cssmin");

gulp.task("clean", function (cb) {
    rimraf("./wwwroot/lib", cb);
});

gulp.task("copy-fontawesome-css", ["clean"], function () {
    return gulp.src([
        "./node_modules/@fortawesome/fontawesome-free-webfonts/css/fa-solid.css",
        "./node_modules/@fortawesome/fontawesome-free-webfonts/css/fontawesome.css"])
        .pipe(gulp.dest("./wwwroot/lib/fontawesome/css/"));
});

gulp.task("copy-fontawesome-fonts", ["clean"], function () {
    return gulp.src("./node_modules/@fortawesome/fontawesome-free-webfonts/webfonts/*")
        .pipe(gulp.dest("./wwwroot/lib/fontawesome/webfonts/"));
});

gulp.task("bundle-and-minify-fontawesome", ["copy-fontawesome-css"], function () {
    return gulp.src("./wwwroot/lib/fontawesome/css/*.css")
        .pipe(concat("fontawesome-all.min.css"))
        .pipe(cssmin())
        .pipe(gulp.dest("./wwwroot/lib/fontawesome/css/"));
})

gulp.task("handle-fontawesome",
    ["clean", "copy-fontawesome-css", "copy-fontawesome-fonts", "bundle-and-minify-fontawesome"]);

gulp.task("copy-jquery", ["clean"], function () {
    return gulp.src("./node_modules/jquery/dist/jquery.min.js")
        .pipe(gulp.dest("./wwwroot/lib/jquery/js"));
});

gulp.task("on-build", ["handle-fontawesome", "copy-jquery"]);