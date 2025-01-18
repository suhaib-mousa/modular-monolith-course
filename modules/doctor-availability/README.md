# Doctor Availability Module

## Overview
The Doctor Availability module is part of the Doctor Appointment System, designed using Traditional Layered Architecture. This module manages doctor time slots, allowing doctors to create and manage their availability slots for patient appointments.

## Architecture
This module follows Traditional Layered Architecture with the following layers:
- **Domain Layer**: Contains entities and domain models
- **Data Layer**: Handles data persistence using Entity Framework Core
- **Business Layer**: Implements business logic and services
- **API Layer**: Provides REST endpoints for slot management

### Project Structure
```
DoctorAvailability/
├── DoctorAvailability.Domain/        # Entities and domain models
├── DoctorAvailability.Data/          # Data access and EF Core implementation
├── DoctorAvailability.Business/      # Business logic and services
└── DoctorAvailability.API/           # REST API endpoints
```

## Features
- Create availability slots
- List all slots
- List available slots only
- Update slot details
- Delete slots
- Automatic slot ID generation
- Persistence using SQL Server

## Technology Stack
- .NET 9.0
- Entity Framework Core
- SQL Server
- ASP.NET Core Web API

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- SQL Server (Local or LocalDB)

### Setup
1. Clone the repository
2. Update connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DoctorAvailabilityDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```
3. Run migrations:
```bash
cd DoctorAvailability.Data
dotnet ef database update
```

### API Endpoints
- GET `/api/slots` - Get all slots
- GET `/api/slots/available` - Get available slots only
- GET `/api/slots/{id}` - Get specific slot
- POST `/api/slots` - Create new slot
- PUT `/api/slots/{id}` - Update slot
- DELETE `/api/slots/{id}` - Delete slot

### Sample Request/Response

Create Slot Request:
```json
POST /api/slots
{
  "time": "2024-01-20T14:30:00",
  "doctorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "doctorName": "Dr. Suhaib",
  "cost": 100.00
}
```

Response:
```json
{
  "id": "8a1c5cfd-4b4c-4678-9b08-9374a9727bc1",
  "time": "2024-01-20T14:30:00",
  "doctorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "doctorName": "Dr. Suhaib",
  "isReserved": false,
  "cost": 100.00
}
```
