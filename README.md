# Parcel Tracking System

## Overview

Parcel Tracking System is a full-stack logistics application built with ASP.NET Core Web API, Angular, Entity Framework Core, and SQL Server.

The system allows logistics operators to create parcels, track shipments, manage parcel statuses, and monitor delivery progress through a dashboard.

---

## Features

### Parcel Management

* Create Parcel
* View All Parcels
* Update Parcel Status
* Delete Parcel
* Search Parcel by Tracking Number

### Parcel Workflow

The system enforces the following delivery workflow:

```text
Pending
   ↓
Collected
   ↓
In Transit
   ↓
Delivered
```

Invalid status transitions are prevented.

### Dashboard

* Total Parcels
* Pending Parcels
* Collected Parcels
* In Transit Parcels
* Delivered Parcels

### Tracking

Users can search for a parcel using its tracking number and view:

* Tracking Number
* Sender
* Receiver
* Status
* Delivery Information

---

## Technology Stack

### Backend

* ASP.NET Core 9 Web API
* Entity Framework Core
* SQL Server
* Repository Pattern
* Swagger/OpenAPI

### Frontend

* Angular
* TypeScript
* HttpClient
* FormsModule
* Standalone Components

### Database

* SQL Server

---

## Project Structure

```text
ParcelTrackingSystem
│
├── Controllers
├── Data
├── Models
├── Repositories
├── Migrations
│
└── parcel-tracking-ui
    ├── src
    ├── services
    ├── models
    └── components
```

---

## API Endpoints

### Parcels

| Method | Endpoint                            | Description      |
| ------ | ----------------------------------- | ---------------- |
| GET    | /api/Parcels                        | Get all parcels  |
| GET    | /api/Parcels/{id}                   | Get parcel by ID |
| POST   | /api/Parcels                        | Create parcel    |
| DELETE | /api/Parcels/{id}                   | Delete parcel    |
| GET    | /api/Parcels/track/{trackingNumber} | Track parcel     |

### Status Updates

| Method | Endpoint                     |
| ------ | ---------------------------- |
| PUT    | /api/Parcels/{id}/collect    |
| PUT    | /api/Parcels/{id}/in-transit |
| PUT    | /api/Parcels/{id}/deliver    |

---

## Getting Started

### Backend

```bash
dotnet restore
dotnet build
dotnet run
```

### Frontend

```bash
cd parcel-tracking-ui

npm install

ng serve
```

Angular runs on:

```text
http://localhost:4200
```

API runs on:

```text
http://localhost:5289
```

---

## Future Enhancements

* Driver Management
* Driver Assignment
* Client Portal
* Driver Portal
* Authentication & Authorization (JWT)
* Delivery Proof
* Failed Delivery Workflow
* Address Change Requests
* Notification System
* Logistics Analytics Dashboard

---

## Author

Pilato Mmatshipyane

Software Developer | ASP.NET Core | Angular | SQL Server | Laravel
