CREATE TABLE countries (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL UNIQUE
);

CREATE TABLE cities (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL UNIQUE,
    country_id INTEGER,
    FOREIGN KEY (country_id) REFERENCES countries(id)
);

CREATE TABLE airports (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    iata_code TEXT NOT NULL UNIQUE,
    name TEXT,
    city_id INTEGER,
    FOREIGN KEY (city_id) REFERENCES cities(id)
);

create table airlines (
	id integer primary key autoincrement, 
	airline text not null unique
);

create table flights2 (
	id integer primary key autoincrement,
	airline_id integer,
	origin_id integer,
	destination_id integer,
	departure_time text,
	arrival_time text,
	stops text,
	price real,
	foreign key (airline_id) references airlines(id),
	foreign key (origin_id) references airports(id),
	foreign key (destination_id) references airports(id)
);

create table hotels2 (
	id integer primary key autoincrement,
	name text,
	city_id integer,
	check_in_date text,
	check_out_date text,
	price real,
	foreign key (city_id) references cities(id)
);

INSERT OR IGNORE INTO countries (name)
SELECT DISTINCT origin_country FROM flights
UNION
SELECT DISTINCT destination_country FROM flights
UNION
SELECT DISTINCT country FROM hotels;

INSERT OR IGNORE INTO cities(name, country_id)
SELECT DISTINCT origin_city, c.id FROM flights f
JOIN countries c ON f.origin_country = c.name
UNION
SELECT DISTINCT destination_city, c.id FROM flights f
JOIN countries c ON f.destination_country = c.name
UNION
SELECT DISTINCT city, c.id FROM hotels h
JOIN countries c ON h.country = c.name;


INSERT OR IGNORE INTO airports(iata_code, name, city_id)
SELECT DISTINCT origin_iata AS iata_code,
				origin as name,
                c.id AS city_id
FROM flights f
JOIN cities c ON f.origin_city = c.name AND EXISTS (
    SELECT 1 FROM countries co WHERE co.id = c.country_id AND co.name = f.origin_country)
UNION
SELECT DISTINCT destination_iata AS iata_code,
				destination as name,
                c.id AS city_id
FROM flights f
JOIN cities c ON f.destination_city = c.name AND EXISTS (
    SELECT 1 FROM countries co WHERE co.id = c.country_id AND co.name = f.destination_country);

insert or ignore into airlines(airline)
select distinct f.airline from flights f;

insert
	or ignore
into
	flights2( airline_id, origin_id, destination_id, departure_time, arrival_time, stops, price )
select distinct
	al.id as airline_id,
	ap.id as origin_id,
	ap2.id as destination_id,
	f.departure_time,
	f.arrival_time,
	f.stops,
	f.price
from
	flights f
join airlines al on al.airline = f.airline
join airports ap on ap.iata_code = f.origin_iata
join airports ap2 on ap2.iata_code = f.destination_iata;


insert or ignore into hotels2 (name, city_id, check_in_date, check_out_date, price )
select distinct
	h.name as name,
	c.id as city_id,
	h.check_in_date as check_in_date,
	h.check_out_date as check_out_date,
	h.price as price
from hotels h
join cities c on c.name = h.city;

insert or ignore into hotels2 (name, city_id, check_in_date, check_out_date, price )
select distinct
	h.name as name,
	c.id as city_id,
	h.check_in_date as check_in_date,
	h.check_out_date as check_out_date,
	h.price as price
from hotels h
join cities c on c.name = h.city;

DROP hotels;
DROP flights;

ALTER TABLE hotels2 RENAME TO hotels;
ALTER TABLE flights2 RENAME TO flights;

