import { CustomerComponent } from './customer.component'

describe('CustomerComponent', () => {
  it('should mount', () => {
    cy.mount(CustomerComponent)
  })
})