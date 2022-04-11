create table if not exists tourUser (
	username varchar(32) primary key,
	password varchar(64) not null
);

create table if not exists tour (
	name varchar(32) primary key,
	--owner varchar(32),
	description varchar(256),
	startpoint varchar(128),
	endpoint varchar(128),
	transportType varchar(32),
	distance int,
	tourTime time,
	info varchar(256),
	imageLocation varchar(64)
	--foreign key (owner) references tourUser(username)
);