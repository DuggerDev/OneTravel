export default interface IFlightModel {
  id: number;
  airline: string;
  origin?: string;
  originIata?: string;
  originCity?: string;
  originCountry?: string;
  destination?: string;
  destinationIata?: string;
  destinationCity?: string;
  destinationCountry?: string;
  departing: string;
  arrival: string;
  stops: number;
  price: number;
}