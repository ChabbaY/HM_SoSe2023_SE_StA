# Needed Endpoints
{{base_url}} = https://localhost:50001

## Account Module
### Account Service
- POST - {{base_url}}/auth/register
- POST - {{base_url}}/auth/login

## Contact Module
### Contact Service
- GET - {{base_url}}/api/contacts/:id
    - Contact ( Address[] ( Country ( Timezone[] ), Timezone ) )

## Customer Module
### Booking Service
- GET - {{base_url}}/api/customers/:customerId/bookings
    - Booking[]
### Customer Service
- GET - {{base_url}}/api/customers
    - Customer[]

## Hotel Module
### Hotel Service
- GET - {{base_url}}/api/hotels
    - Hotel[]
### Room Service
- GET - {{base_url}}/api/hotels/:hotelId/rooms
    - Room[] ( RoomType )

## Service Module
### Service Service
- GET - /api/services/flights
    - Flight[] ( Service ( ServiceType ) )
- GET - /api/services/cars
    - RentalCar[] ( Service ( ServiceType ) )
- GET - /api/services/wellnesses
    - Wellness[] ( Service ( ServiceType ) )
