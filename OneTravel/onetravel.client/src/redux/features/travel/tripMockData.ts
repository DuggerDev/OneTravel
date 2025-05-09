import IFlightModel from "../../../models/IFlightModel";
import IHotelModel from "../../../models/IHotelModel";
import ITripModel from "../../../models/ITripModel";

export const tripList: ITripModel[] = Array.from({ length: 10 }, (_, i) => {
    const baseDate = new Date();
    const departing = new Date(baseDate);
    departing.setDate(baseDate.getDate() + i);
  
    const arrival = new Date(departing);
    arrival.setHours(arrival.getHours() + 7 + i);
  
    const checkInDate = new Date(arrival);
    const checkOutDate = new Date(checkInDate);
    checkOutDate.setDate(checkInDate.getDate() + 7);
  
    const flight: IFlightModel = {
      id: i,
      airline: ['Delta', 'American Airlines', 'Iberia', 'United', 'JetBlue'][i % 5],
      origin: 'JFK',
      originIata: 'JFK',
      originCity: 'New York',
      originCountry: 'United States',
      destination: 'MAD',
      destinationIata: 'MAD',
      destinationCity: 'Madrid',
      destinationCountry: 'Spain',
      departing,
      arrival,
      stops: i % 3,
      price: 200 + i * 20,
    };
  
    const hotel: IHotelModel = {
      name: ['Comfort Inn', 'Marriott', 'Hilton', 'Eurostars', 'Hostal Central'][i % 5],
      city: 'Madrid',
      country: 'Spain',
      checkInDate,
      checkOutDate,
      price: 400 + i * 10,
    };
  
    return {
      flight,
      hotel,
      totalPrice: flight.price + hotel.price,
    };
  });
  