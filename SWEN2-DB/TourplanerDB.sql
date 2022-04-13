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
	distance real,
	tourTime time,
	info varchar(256),
	imageLocation varchar(64)
	--foreign key (owner) references tourUser(username)
);

insert into tour(name, description, startpoint, endpoint, transportType, distance, tourTime, info, imageLocation) values('name', 'description', 'startpoint', 'endpoint', 'transportType', 1.001, '10:00:33', 'info', 'imageLocation');
insert into tour(name, description, startpoint, endpoint, transportType, distance, tourTime, info, imageLocation) values('Best tour ever!', 'Really the best!', 'Vienna, AT', 'Graz, AT', 'foot', 1.001, '12:45:53', 'Best info!', 'imageLocation');

curl -X POST https://localhost:7221/api/Tour -H "Content-Type: application/json" -d "{\"name\":\"SuperAwesomeTour\",\"description\":\"desc\",\"from\":\"Vienna, AT\",\"to\":\"Graz, AT\",\"routetype\":\"bicycle\",\"info\":\"info\",\"imagelocation\":\"loc\"}"