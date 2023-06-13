import { Address } from "./address.model";

export interface Contact {
  id: number,
  title: string,
  telefone: string,
  addresses: Address[]
}
