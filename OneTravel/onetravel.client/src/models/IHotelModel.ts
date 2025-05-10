export default interface IHotelModel {
  name: string;
  city: string;
  country?: string;
  checkInDate: string;
  checkOutDate: string;
  price: number;
}