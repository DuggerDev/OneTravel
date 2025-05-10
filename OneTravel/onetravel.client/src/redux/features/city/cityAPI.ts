export default interface ICityModel {
  id: number;
  name: string;
  country: string;
}

export async function fetchCities(): Promise<{ data: string[] }> {
  const response = await fetch("https://localhost:7212/City");

  if (!response.ok) {
    throw new Error("Failed to fetch trips");
  }

  const cities: ICityModel[] = await response.json();
  const formatted = cities.map(cities => `${cities.name}, ${cities.country}`);

  return { data: formatted };
}