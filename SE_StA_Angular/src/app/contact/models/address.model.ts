import { Country } from "./country.model";
import { Timezone } from "./timezone.model";

export interface Address {
  id: number,
  street: string,
  houseNr: string,
  postcode: string,
  city: string,
  addition: string,
  country: Country,
  timezone: Timezone
}
