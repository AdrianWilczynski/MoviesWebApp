CREATE TABLE Users (
	UserId int IDENTITY(1,1) PRIMARY KEY,
	Email varchar(255) NOT NULL UNIQUE,
	UserName varchar(255) NOT NULL,
	PasswordHash varchar(max) NOT NULL,
);