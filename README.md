# Telecom Billing and Consumption API

A RESTful API built with ASP.NET Core for managing telecom subscribers, tracking usage (calls, data, SMS), and generating monthly bills with business rules such as peak/off-peak pricing, roaming charges, bundle limits, and loyalty discounts.

## Features

- Subscriber management (CRUD)
- Telecom usage tracking (Call, Data, SMS)
- Monthly billing generation
- Bundle usage calculation
- Peak vs Off-Peak call pricing
- Roaming charges
- Loyalty discount after 2 years
- VAT calculation
- Dashboard statistics
- Pagination support
- Bulk usage insertion
- JWT authentication
- Role-based authorization (Admin / User)

## Tech Stack

#### Backend Framework
- ASP.NET Core 8 Web API

#### Architecture
- Clean Architecture
- CQRS Pattern using MediatR

#### Database
- SQL Server
- Entity Framework Core
- EF Core Migrations

#### Authentication & Authorization
- ASP.NET Identity
- JWT Authentication
- Role-based Authorization (Admin / User)

#### Validation
- FluentValidation

#### Object Mapping
- AutoMapper

#### Localization
- ASP.NET Core Localization
- Resource-based localization using `.resx` files

#### Caching
- In-memory caching for Tariff rules

#### API Documentation
- Swagger / OpenAPI

#### Containerization
- Docker
- Docker Compose

#### Development Tools
- Visual Studio
- Postman for API testing

## Architecture

The project follows Clean Architecture principles.

Layers:

API
Handles HTTP requests and responses.

Core
Contains business logic, CQRS commands/queries, DTOs, and validators.

Service
Contains application services implementing business rules.

Infrastructure
Handles database access, repositories, and external integrations.

Data
Contains entity models and database configurations.

## Database Design

Main Entities:

User
Application user managed by ASP.NET Identity.

Subscriber
Represents a telecom customer linked to a user account.

Plan
Defines bundle limits for calls, data, and SMS.

UsageRecord
Stores telecom activity including calls, data usage, and SMS.

TariffRule
Defines pricing rules based on usage type, roaming status, and peak hours.

Bill
Represents a monthly invoice generated for a subscriber.

BillDetail
Stores detailed breakdown of usage included in a bill.

## Business Rules

Call Pricing
Peak hours (08:00 – 20:00): higher rate
Off-Peak hours: lower rate

Data Pricing
Domestic usage has a lower rate
Roaming usage has a higher rate

SMS Pricing
Domestic SMS has a base rate
Roaming SMS has a higher rate

Bundle Limits
Subscribers have monthly bundles defined by their plan.

If usage exceeds bundle limits:
The extra usage is charged at double the normal tariff.

VAT
15% VAT is applied to the total bill.

Loyalty Discount
Subscribers active for more than 2 years receive a 5% discount.

## Authentication

JWT-based authentication is implemented.

Roles:
Admin
User

Admin Permissions
Manage subscribers
Manage plans
Insert usage records
Generate bills
View dashboard statistics

User Permissions
View personal usage
View personal bills

## API Endpoints

Authentication
POST /api/auth/register
POST /api/auth/login

Subscribers
POST /api/subscribers
GET /api/subscribers
PUT /api/subscribers/{id}
DELETE /api/subscribers/{id}

Usage
POST /api/usage
POST /api/usage/bulk
GET /api/usage/me
GET /api/usage/subscriber/{id}

Billing
POST /api/billing/{subscriberId}/{month}
GET /api/billing/{billId}
GET /api/billing/{subscriberId}/{month}

Dashboard
GET /api/statistics/top-consumers
GET /api/statistics/revenue
GET /api/statistics/usage

## Validation

FluentValidation is used to validate requests.

Examples:
- Phone number format validation
- Prevent duplicate subscribers
- Ensure plan exists before assignment
- Prevent billing future months
- Validate bulk usage records

## Database Seeding

The system automatically seeds sample data including:

- 1 Admin user
- 10 Subscribers
- Multiple Plans
- Tariff rules
- Sample usage records

This allows the application to be tested immediately after startup.


## API Workflow

The application follows a telecom billing lifecycle where subscriber activity is recorded and converted into monthly bills.

### 1. Subscriber Registration
An admin creates a subscriber and assigns a telecom plan.
Admin → Create Subscriber → Assign Plan

Each subscriber is linked to a **User account** in the system.

---

### 2. Usage Recording

Telecom activity is recorded as **UsageRecords**.

Supported usage types:

- Call
- Data
- SMS

Usage can be inserted individually or in bulk.
Network Activity → UsageRecord → Database


Each usage record includes:

- SubscriberId
- UsageType
- Timestamp
- Roaming status
- Peak / Off-Peak classification

---

### 3. Tariff Calculation

Each usage record is priced using **Tariff Rules** based on:

- Usage type (Call / Data / SMS)
- Roaming status
- Peak or Off-Peak hours

Example:

| Usage Type | Condition | Price |
|------------|-----------|-------|
| Call | Peak | Higher Rate |
| Call | Off-Peak | Lower Rate |
| Data | Roaming | Higher Rate |
| SMS | Domestic | Base Rate |

---

### 4. Bundle Consumption

Each subscriber plan contains monthly limits:

Example Premium Plan:
1000 Call Minutes
10 GB Data
500 SMS

When usage exceeds the bundle:
Extra usage → Charged at DOUBLE tariff rate

---

### 5. Monthly Billing

Bills are generated per subscriber and month.


Each bill contains:

- Plan Fee
- Usage Cost
- Extra Usage Cost
- Roaming Charges
- VAT (15%)
- Loyalty Discount (5% for subscribers active > 2 years)

---

### 6. Dashboard Analytics

The system provides analytics APIs including:

- Top consumers
- Monthly revenue
- Usage statistics
UsageRecords + Bills → Dashboard Statistics


---

### System Flow Summary
Subscriber → UsageRecord → Tariff Calculation → Billing → Dashboard



## Running the Application (Docker)
The project includes a Docker configuration that runs both:
- ASP.NET Core API
- SQL Server Database

### Requirements

- Docker
- Docker Compose
---
### Step 1: Clone the Repository
git clone https://github.com/yourusername/TelecomBillingAndConsumption.git

cd TelecomBillingAndConsumption
---

### Step 2: Run Docker Containers

Run the following command:


This will start:

| Service | Port |
|-------|------|
| API | 8080 |
| SQL Server | 1433 |

---

### Step 3: Database Initialization

When the application starts:

- EF Core migrations are applied automatically.
- The database is seeded with sample data including:
1 Admin user
10 Subscribers
Plans
Tariff rules
Sample usage records


---

### Step 4: Access the API

Open the API at: https://localhost:8080

Swagger documentation will be available if enabled.

---
### Step 5: Test Credentials

Admin Account:
Email: admin@telecom.com

Password: Admin@123

Test User:
Email: user1@telecom.com

Password: User@123

---

### Step 6: Stop Containers

To stop the system:
docker-compose down
If you want to remove the database volume as well:
docker-compose down -v


## System Architecture
Client
   │
   ▼
ASP.NET Core API
   │
   ├── Application Layer (CQRS + MediatR)
   │
   ├── Domain Services
   │
   └── Infrastructure (EF Core)
            │
            ▼
        SQL Server



## Test Credentials

Admin
email: admin@telecom.com
password: Admin@123

User
email: user1@telecom.com
password: User@123

## API Testing

A Postman collection is included in the repository to test all endpoints.

## Future Improvements

- Payment gateway integration
- Advanced analytics dashboard
- API rate limiting

## Author

Guirguis Hedia 
guirguishedia@gmail.com  
+01210404274
