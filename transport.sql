CREATE TABLE transport(
	id int primary key identity(1,1),
	transport_type varchar(80) not null,
	start_id int null,
	end_id int null,
	duration int null,
	constraint fk_start_loc foreign key (start_id) references locations(id),
	constraint fk_end_loc foreign key (end_id) references locations(id)
);