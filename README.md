# E-commerce Microservices Project / Proyecto de Microservicios para E-commerce

This project implements a microservices architecture for an e-commerce system using .NET 9, Docker, YARP as API Gateway, RabbitMQ for messaging, and specialized databases per service (MongoDB, Redis, PostgreSQL, MySQL).

Este proyecto implementa una arquitectura de microservicios para un sistema de e-commerce utilizando .NET 8, Docker, YARP como API Gateway, RabbitMQ para mensajería, y bases de datos especializadas por servicio (MongoDB, Redis, PostgreSQL, MySQL).

---

## Technologies Used / Tecnologías Utilizadas

- **.NET 8** (Web API, gRPC, MediatR)
- **YARP** (API Gateway)
- **RabbitMQ** (asynchronous messaging / mensajería asíncrona)
- **MongoDB** (Catalog Service)
- **Redis** (Basket Service)
- **PostgreSQL** (Discount Service)
- **MySQL** (Ordering Service)
- **Docker & Docker Compose**

---

## Architecture Overview / Arquitectura General

```
[ Client / Cliente / Frontend ]
        |
        v
[ API Gateway - YARP ]
   |        |        |        |
   v        v        v        v
Catalog  Basket  Discount  Ordering
  |        |        |         |
MongoDB  Redis  Postgre  RabbitMQ+MySQL
```

---

## Services / Servicios

### 1. Catalog.API
- Product CRUD / CRUD de productos
- Stored in **MongoDB** / Almacenado en **MongoDB**
- Main endpoint: `/catalog/api/products`

### 2. Basket.API
- User cart management / Manejo del carrito de compras por usuario
- Stored in **Redis**
- Integrates with `Discount.Grpc` to apply discounts / Integra con `Discount.Grpc` para aplicar descuentos
- Main endpoint: `/basket/api/basket/{username}`

### 3. Discount.Grpc
- gRPC service that returns discount per product / Servicio gRPC que retorna el descuento por producto
- Stored in **PostgreSQL**
- gRPC endpoint: `GetDiscount(productId)`

### 4. Ordering.API
- Registers orders / Registra pedidos
- Publishes `OrderPlaced` event to **RabbitMQ** / Publica evento `OrderPlaced` en **RabbitMQ**
- Stores orders in **MySQL** / Guarda los pedidos en **MySQL**
- Main endpoint: `/ordering/api/orders`

### 5. ApiGateway (YARP)
- Routes external traffic to microservices / Redirige el tráfico hacia los microservicios
- Entry port: `http://localhost:5000`

---

## Public Endpoints (via Gateway) / Endpoints expuestos (vía Gateway)

| Service / Servicio | Gateway Route / Ruta desde Gateway               |
|--------------------|--------------------------------------------------|
| Catalog            | `GET /catalog/api/products`                     |
| Basket             | `GET /basket/api/basket/{username}`            |
| Discount (gRPC)    | `gRPC: DiscountService.GetDiscount`            |
| Ordering           | `POST /ordering/api/orders`                    |

---

## Running the Project / Ejecución del Proyecto

1. Clone the repo / Clona el repositorio:
```bash
git clone https://github.com/your-user/ecommerce-microservices.git
cd ecommerce-microservices
```

2. Run Docker Compose / Ejecuta Docker Compose:
```bash
docker-compose up --build
```

3. Access services / Accede a los servicios:
- Gateway: [http://localhost:5000](http://localhost:5000)
- RabbitMQ: [http://localhost:15672](http://localhost:15672) (`guest/guest`)

---

## Recommendations / Recomendaciones

- Run `dotnet ef migrations add Init` and `dotnet ef database update` if you change models / Ejecuta esos comandos si cambias los modelos.
- Check Docker logs using `docker-compose logs -f service_name` / Verifica los logs con ese comando.
- Use `Grpcurl` or Postman to test `Discount.Grpc` / Usa Grpcurl o Postman para probar `Discount.Grpc`.

---

## Next Steps / Pendientes y mejoras futuras

- Authentication with JWT or IdentityServer / Autenticación con JWT o IdentityServer
- Admin dashboard / Panel de administración
- Frontend integration / Integración con frontend
- Retry policies and circuit breakers / Reintentos y tolerancia a fallos
- Event Sourcing / Almacenamiento de eventos

---

## Author / Autor
Mario Díaz

