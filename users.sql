CREATE TABLE users(
	id int primary key identity(1,1),
	ime varchar(80) not null,
	email varchar(80) not null
);