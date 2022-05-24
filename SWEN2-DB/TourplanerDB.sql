create table if not exists tour (
	name varchar(32) primary key,
	description varchar(256),
	startpoint varchar(128),
	endpoint varchar(128),
	transportType varchar(32),
	distance real,
	tourTime varchar(16),
	info varchar(256),
	imageLocation varchar(64)
);

create table if not exists tourLog (
	tourname varchar(32),
	tourdate varchar(256),
	tourcomment varchar(128),
	difficulty int,
	tourtime varchar(32),
	rating int,
	logID int,
	PRIMARY KEY (tourname, logID)
	foreign key (tourname) references tour(name)
);

insert into tour(name, description, startpoint, endpoint, transportType, distance, tourTime, info, imageLocation) values('name', 'description', 'startpoint', 'endpoint', 'transportType', 1.001, '10:00:33', 'info', 'imageLocation');
insert into tour(name, description, startpoint, endpoint, transportType, distance, tourTime, info, imageLocation) values('Best tour ever!', 'Really the best!', 'Vienna, AT', 'Graz, AT', 'foot', 1.001, '12:45:53', 'Best info!', 'imageLocation');

curl -X POST https://localhost:7221/api/Tour -H "Content-Type: application/json" -d "{\"name\":\"SuperAwesomeTour\",\"description\":\"desc\",\"from\":\"Vienna, AT\",\"to\":\"Graz, AT\",\"routetype\":\"bicycle\",\"info\":\"info\"}"
curl -X PUT https://localhost:7221/api/Tour/<tourname> -H "Content-Type: application/json" -d "{\"name\":\"SuperAwesomeTour\",\"description\":\"desc\",\"from\":\"Vienna, AT\",\"to\":\"Graz, AT\",\"routetype\":\"bicycle\",\"info\":\"info\",\"imagelocation\":\"loc\"}"

curl -X POST https://localhost:7221/api/TourLog -H "Content-Type: application/json" -d "{\"tourname\":\"SuperAwesomeTour\",\"date\":\"heuteLuL\",\"Comment\":\"War ganz ok\",\"difficulty\":\"2\",\"time\":\"2:31:31\",\"rating\":1}"