import { Country } from "./country.model";
import { Timezone } from "./timezone.model";

export interface Address {
  addressId: number,
  street: string,
  houseNumber: string,
  postalCode: string,
  town: string,
  addressAddition: string,
  countryId: number
  country: Country,
  timeZoneId: number
  timezone: Timezone
}
