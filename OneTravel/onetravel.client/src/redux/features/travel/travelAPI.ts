// A mock function to mimic making an async request for data

import IFlightModel from "../../../models/IFlightModel";
import IHotelModel from "../../../models/IHotelModel";
import ITripModel from "../../../models/ITripModel";

const YEAR_MONTH_DATE_INDEX = 10;

export async function generateTrips(originCity: string, destinationCity: string, startDate: Date): Promise<{ data: ITripModel[] }> {
  const flights = (await fetchFlights(originCity, destinationCity, startDate)).data;

  const trips: ITripModel[] = [];
  let hotelDateMap = new Map();


  for (let flight of flights) {
    let arrivalTime = flight.arrival.substring(0, YEAR_MONTH_DATE_INDEX)

    let hotels: IHotelModel[];

    if (arrivalTime in hotelDateMap) {
      hotels = hotelDateMap.get(arrivalTime);
    } else {
      hotels = (await fetchHotels(destinationCity, flight.arrival)).data;
      hotelDateMap.set(arrivalTime, hotels)
    }

    for (var hotel of hotels) {
      const totalPrice = flight.price + hotel.price;
      trips.push({ hotel, flight, totalPrice });
    }
  }


  return { data: trips };
}


export async function fetchFlights(origin: string, destination: string, departingDate: Date): Promise<{ data: IFlightModel[] }> {
  var originCity = origin.substring(0, origin.indexOf(","));
  var destinationCity = destination.substring(0, destination.indexOf(","));
  var departureTime = departingDate.toISOString();
  const response = await fetch("https://localhost:7212/Flight", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ originCity, destinationCity, departureTime }),
  });

  if (!response.ok) {
    throw new Error("Network response was not ok");
  }

  const data: IFlightModel[] = await response.json();
  return { data }
}

export async function fetchHotels(city: string, checkInDate: string): Promise<{ data: IHotelModel[] }> {
  city = city.substring(0, city.indexOf(","))
  // var checkInDate = checkDate.toISOString();

  const response = await fetch("https://localhost:7212/Hotel", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ city, checkInDate }),
  });

  if (!response.ok) {
    throw new Error("Network response was not ok");
  }

  const data: IHotelModel[] = await response.json();
  return { data }
}
