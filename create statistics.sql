CREATE TABLE statistics_prijevoz(
	id int primary key identity(1,1),
	users_id int null,
	hotel_id int null,
	naziv varchar(80) null
);