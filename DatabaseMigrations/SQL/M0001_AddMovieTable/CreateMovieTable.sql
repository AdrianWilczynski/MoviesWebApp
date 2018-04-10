CREATE TABLE Movie (
    MovieId int IDENTITY(1,1) PRIMARY KEY,
    Title varchar(255),
	Description varchar(max),
	PosterPath varchar(max),
    ReleaseDate date
);