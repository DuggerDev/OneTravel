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
  departing?: Date;
  arrival?: Date;
  stops?: number;
  price: number;
}