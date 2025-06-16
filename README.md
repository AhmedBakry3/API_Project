# E-Commerce API Backend

This project is the **backend API** designed to serve an Angular e-commerce frontend. It provides RESTful endpoints for user authentication, product catalog management, orders, payments, and caching — forming the core server-side of a modern e-commerce system.

---

## Overview

The API follows a clean **Onion Architecture**, ensuring a clear separation of concerns, maintainability, and testability. It leverages industry best practices and modern .NET 8 technologies to build a scalable and secure backend.

---

## Architecture

The solution is organized into multiple projects following the **Onion Architecture** pattern, grouped into **Core**, **Infrastructure**, **Presentation**, and **Shared** layers:

- **Core Layer**  
  - **Domain Project**: Contains core business entities, domain exceptions, interfaces, and specifications.  
  - **Service Project**: Contains service abstractions (interfaces) and DTO definitions.  
  - **ServiceImplementation Project**: Implements business logic, AutoMapper profiles, and service orchestration.

- **Infrastructure Layer**  
  - **Persistence Project**: Implements data access, Entity Framework Core DbContexts, repositories, data seeding, and ASP.NET Core Identity setup.  
  - **Presentation Project**: Contains ASP.NET Core Web API controllers, middleware (exception handling, caching), and API configuration.

- **Shared Project**: Contains shared data transfer objects (DTOs), enums, and utility classes used across layers.

- **E-Commerce Project**: Hosts the ASP.NET Core Web API startup and program configuration.

- **Cross-Cutting Concerns** (applied throughout):  
  Dependency Injection, Caching (Redis), Logging, Exception Handling, Configuration Extensions.

---

## Key Technologies

- ASP.NET Core Web API (.NET 8)  
- Entity Framework Core (SQL Server provider)  
- ASP.NET Core Identity for user and role management  
- Redis (via StackExchange.Redis) for distributed caching  
- JWT Bearer Authentication for secure token-based access  
- AutoMapper for object mapping between entities and DTOs  
- Stripe.NET for payment processing integration  
- Swagger / Swashbuckle for API documentation and interactive testing  
- Postman can also be used for manual API endpoint testing  
- Dependency Injection for managing service lifetimes  
- EF Core Tools for database migrations  

---

## Design Patterns & Concepts

- **Repository Pattern** abstracts data access logic  
- **Unit of Work Pattern** coordinates transactional operations  
- **Specification Pattern** for flexible query definitions  
- **Service Manager with Factory Delegates** for service resolution  
- **DTOs** for request and response data encapsulation  
- **Pagination and Query Parameters** for efficient data retrieval  
- **Middleware for Centralized Exception Handling**  
- **Custom Caching Attribute** to cache API responses with Redis  
- **Extension Methods** for modular and maintainable service registration  
- **Global Using Directives** to simplify usings across the solution  
- **Onion Architecture** for clean layer separation  

---

## Features

- Secure user registration, login, and role management  
- Product management with brands, types, and image URL resolution  
- Order processing with delivery methods and payment integration  
- Basket management using Redis cache for fast performance  
- Integration with Stripe for payment intents and confirmation  
- Automatic data seeding of products, brands, delivery methods, users, and roles  
- API documentation and interactive testing through Swagger UI  
- Global exception handling with detailed error responses  

---

## Getting Started

1. Clone the repository:  
   ```bash
   git clone https://github.com/AhmedBakry3/ECommerceAPI.git
