CREATE TABLE hotels(
	id int primary key identity(1,1),
	hotel_name varchar(80) not null,
	hotel_address varchar(320) null,
	stars int null,
	loc_id int not null,
	constraint fk_hotel_loc foreign key (loc_id) references locations(id)
);