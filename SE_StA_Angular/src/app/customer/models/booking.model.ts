export interface Booking {
    id: number,
    nr: string,
    date: string,
    price: number,
    customerId: number,
    invoiceId: number,
    paymentMethodId: number,
    statusId: number
}