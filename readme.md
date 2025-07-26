Pizza Shop API - Technical Assessment

## Task Overview

Build a RESTful API for pizza ordering

We'll discuss your approach and decisions in a follow-up meeting.

## Technical Requirements

### Must Use

- .NET 9
- Entity Framework Core with SQLite database
- MediatR pattern for all business operations
- Clean Architecture principles
- DTOs for API communication (no direct entity exposure)

### Getting Started

- Set up your solution structure using Clean Architecture
- Research MediatR basics: Commands, Queries, and Handlers
- Use Code-First approach with EF Core migrations
- Implement proper data seeding

## Core Features

### Pizza Menu System

- List available pizzas with filtering options
- Get detailed pizza information
- Support for different pizza sizes and pricing

### Topping Management

- Pizzas have default toppings (included in base price)
- Customers can add extra toppings (additional cost)
- Track topping availability

### Order Processing

- Create orders with custom pizza configurations
- Calculate accurate pricing including size and extra toppings
- Order status tracking
- View order details

## Domain Requirements

Design entities to support:

- Pizzas with different sizes and default toppings
- Available toppings with individual pricing
- Orders containing multiple pizza items
- Many-to-many relationships where appropriate

## API Endpoints

Design RESTful endpoints for:

- Pizza catalog browsing
- Topping selection
- Order creation and retrieval
- Order status management

## Sample Data

Your seeded data should include:

- At least 3 different pizzas with varying base prices by size
- At least 6 toppings with different prices
- Logical default topping assignments

## DTO Example

```csharp
public class CreateOrderDto
{
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public List<OrderItemDto> Items { get; set; }
}
```

## Business Rules

- Pizza pricing varies by size (implement size-based multipliers)
- Default toppings are included in base price
- Extra toppings add to the total cost
- Orders should have status tracking capability

## Deliverables

1. complete solution that runs with `dotnet run`
2. SQLite database with proper migrations and seeding
3. README with setup instructions
4. Brief explanation of your architectural decisions


**Focus on:** Clean code, proper separation of concerns, and demonstrating your understanding of the required patterns. Completing all features is less important than showing good coding practices and architectural thinking.

---

if there is any questions you can reach me at: 
+31 6 371 95 387
