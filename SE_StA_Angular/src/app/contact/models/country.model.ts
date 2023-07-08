import { Timezone } from "./timezone.model";

export interface Country {
  countryId: number,
  name: string,
  language: string,
  iso2: string,
  timezones: Timezone[]
}
