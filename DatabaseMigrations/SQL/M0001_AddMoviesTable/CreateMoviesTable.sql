CREATE TABLE Movies (
	MovieId int IDENTITY(1,1) PRIMARY KEY,
	Title varchar(255) NOT NULL UNIQUE,
	Description varchar(max) NOT NULL,
	PosterPath varchar(max) NOT NULL,
	ReleaseDate date NOT NULL
);