import IFlightModel from "./IFlightModel";
import IHotelModel from "./IHotelModel";

export default interface ITripModel {
    flight: IFlightModel;
    hotel: IHotelModel;
    totalPrice: number;
  }