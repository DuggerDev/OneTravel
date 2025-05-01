# This program generates fake flights and hotel bookings using the Faker library.
# Then stores them in a SQLite database.

from faker import Faker
from faker_airtravel import AirTravelProvider
import random
import sqlite3
from datetime import datetime, timedelta
from tqdm import tqdm


hotels = [
    "Chateau Marmont",
    "Raffles",
    "The Waldorf Astoria",
    "Mandarin Oriental",
    "MGM Grand Hotel",
    "Hotel Sacher",
    "Aman Resorts",
    "Four Seasons",
    "Shangri-La Hotels",
    "Ritz-Carlton",
    "InterContinental Hotels",
    "Marriott International",
    "Hyatt Hotels",
    "Sheraton"
]

faker = Faker()
faker.add_provider(AirTravelProvider)

sqlite_file = 'fake_travel.db'
conn = sqlite3.connect(sqlite_file)
c = conn.cursor()

# Create a table for flights
flights_table = '''
    CREATE TABLE IF NOT EXISTS flights (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        airline TEXT,
        origin TEXT,
        origin_iata TEXT,
        origin_city TEXT,
        origin_country TEXT,
        destination TEXT,
        destination_iata TEXT,
        destination_city TEXT,
        destination_country TEXT,
        departure_time TEXT,
        arrival_time TEXT,
        stops TEXT,
        price REAL
    )
'''

hotels_table = '''
    CREATE TABLE IF NOT EXISTS hotels (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        name TEXT,
        city TEXT,
        country TEXT,
        check_in_date TEXT,
        check_out_date TEXT,
        price REAL
    )
'''

c.execute(flights_table)

# Create a table for hotels
c.execute(hotels_table)


# Function to generate fake hotels
def generate_hotel(arival_date):
    name = random.choice(hotels)
    check_in_date = faker.date_time_between(start_date=arival_date, end_date='+12h')
    check_out_date = check_in_date + timedelta(days=random.randint(2, 10))
    price = round(random.uniform(50, 500), 2)

    return (name, check_in_date, check_out_date, price)


# function to write the data to the database
def write_hotel_to_db(city, country, arrival_date):
    hotel = generate_hotel(arrival_date)
    record = (hotel[0], city, country, hotel[1].strftime('%Y-%m-%d %H:%M:%S'), hotel[2].strftime('%Y-%m-%d %H:%M:%S'), hotel[3])
    sql = str.format('''
        INSERT INTO hotels (name, city, country, check_in_date, check_out_date, price)
        VALUES ("{}", "{}", "{}", "{}",  "{}", {})
    ''', record[0], record[1], record[2], record[3], record[4], record[5])

    c.execute(sql)

def write_flight_to_db():
    flight = faker.flight()
    departure_date = faker.date_time_between(start_date='now', end_date='+90d')
    arrival_date = departure_date + timedelta(hours=random.randint(1, 12))
    record = (
        flight['airline'],
        flight['origin']['airport'],
        flight['origin']['iata'],
        flight['origin']['city'],
        flight['origin']['country'],
        flight['destination']['airport'],
        flight['destination']['iata'],
        flight['destination']['city'],
        flight['destination']['country'],
        # Random date within 2025
        departure_date.strftime('%Y-%m-%d %H:%M:%S'),
        arrival_date.strftime('%Y-%m-%d %H:%M:%S'),
        flight['stops'],
        round(random.uniform(50, 500), 2)
    )
    for i in range(10):
        write_hotel_to_db(flight['destination']['city'], flight['destination']['country'], arrival_date)

    sql = str.format('''
        INSERT INTO flights (airline, origin, origin_iata, origin_city, origin_country, destination, destination_iata, destination_city, destination_country, departure_time, arrival_time, stops, price)
        VALUES ( "{}", "{}", "{}", "{}", "{}", "{}", "{}", "{}", "{}", "{}", "{}", "{}", {} )
    ''', record[0], record[1], record[2], record[3], record[4], record[5], record[6], record[7], record[8], record[9], record[10], record[11], record[12])

    c.execute(sql)


for i in tqdm(range(100_000)):
    write_flight_to_db()
    if i % 1_000 == 0:
        conn.commit()

print("Data generation complete.")
# Close the database
conn.commit()
conn.close()

