export default interface IHotelModel {
  name: string;
  city?: string;
  country?: string;
  checkInDate?: Date;
  checkOutDate?: Date;
  price: number;

}