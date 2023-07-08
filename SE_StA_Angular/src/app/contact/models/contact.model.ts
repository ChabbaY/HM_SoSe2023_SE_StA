import { Address } from "./address.model";

export interface Contact {
  contactId: number,
  salutation: string,
  phone: string,
  addresses: Address[]
}
