﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesWebApp.ViewModels
{
    public class MovieDetailsViewModel
    {
        private string _posterPath;

        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }

        public string PosterPath { get => _posterPath; set => _posterPath = value.Replace(@"\", "/"); }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public bool? Liked { get; set; }
    }
}
