/// <binding BeforeBuild='on-build' />
"use strict";

let gulp = require("gulp");
let rimraf = require("rimraf");
let concat = require("gulp-concat");
let cssmin = require("gulp-cssmin");
let rename = require("gulp-rename");
let uglify = require("gulp-uglify");
let babel = require("gulp-babel");

gulp.task("clean-lib", function (cb) {
    rimraf("./wwwroot/lib", cb);
});

gulp.task("clean-js", function (cb) {
    rimraf("./wwwroot/js/*.min.js", cb);
});

gulp.task("copy-fontawesome-css", ["clean-lib"], function () {
    return gulp.src([
        "./node_modules/@fortawesome/fontawesome-free-webfonts/css/fa-solid.css",
        "./node_modules/@fortawesome/fontawesome-free-webfonts/css/fontawesome.css"])
        .pipe(gulp.dest("./wwwroot/lib/fontawesome/css/"));
});

gulp.task("copy-fontawesome-fonts", ["clean-lib"], function () {
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
    ["clean-lib", "copy-fontawesome-css", "copy-fontawesome-fonts", "bundle-and-minify-fontawesome"]);

gulp.task("copy-jquery", ["clean-lib"], function () {
    return gulp.src("./node_modules/jquery/dist/jquery.min.js")
        .pipe(gulp.dest("./wwwroot/lib/jquery/js"));
});

gulp.task("minify-site-css", function () {
    return gulp.src("./wwwroot/css/site.css")
        .pipe(cssmin())
        .pipe(rename({ suffix: ".min" }))
        .pipe(gulp.dest("./wwwroot/css"));
});

gulp.task("minify-and-transpiler-js-scripts", ["clean-js"], function () {
    return gulp.src("./wwwroot/js/*.js")
        .pipe(babel({
            presets: ['env']
        }))
        .pipe(uglify())
        .pipe(rename({ suffix: ".min" }))
        .pipe(gulp.dest("./wwwroot/js"));
});

gulp.task("on-build", ["handle-fontawesome", "copy-jquery",
    "minify-site-css", "minify-and-transpiler-js-scripts"]);