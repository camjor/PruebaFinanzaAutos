
---

## 🏗️ Decisiones Arquitectónicas

### ✔ **1. Arquitectura por capas con separación estricta**
- **Models:** Representan entidades del dominio Northwind.
- **Dtos:** Objetos para entrada y salida de datos.
- **Repositories:** Acceso a datos con Entity Framework Core.
- **Controllers:** Capa HTTP.
- **Mapping:** Automapper para conversión automática entre entidades y DTOs.
- **Middleware:** Manejo centralizado de errores y validaciones.

### ✔ **2. Repositorios concretos por entidad**
Cada entidad tiene su propio repositorio (ProductRepository, CategoryRepository, OrderRepository, etc.), cumpliendo con el **Principio de Responsabilidad Única** y facilitando las pruebas unitarias.

### ✔ **3. AutoMapper para desacoplar el dominio del transporte HTTP**
Se utiliza un `AutoMapperProfile` con mapeos avanzados para evitar lógica en controladores y simplificar transformaciones.

### ✔ **4. Seed de datos Northwind simplificado**
`LoadDatabase.InsertarData()` crea automáticamente datos iniciales (usuario, categorías, productos, órdenes, etc.), permitiendo probar la API sin configuraciones adicionales.

### ✔ **5. JWT como mecanismo de seguridad**
Todos los endpoints están protegidos. El JWT se integra con IdentityCore para la gestión de usuarios.

### ✔ **6. Base de datos**
Configurada con **SQL Server** como base de datos primaria.

---

## ☁️ Escalabilidad Horizontal en Entornos Cloud

Diseñada para funcionar en plataformas como AWS, Azure o GCP.

### ✔ **1. Stateless API**
Controladores sin estado, permitiendo múltiples instancias sin conflictos.

### ✔ **2. JWT sin almacenamiento en servidor**
Autenticación stateless, ideal para balanceadores de carga.

### ✔ **3. Base de datos externa**
Compatible con servicios como Azure SQL o AWS RDS.

### ✔ **4. Contenerización recomendada**
Preparada para Docker, Kubernetes (AKS/EKS/GKE), ECS, App Services, etc.

### ✔ **5. Health checks**
Pueden añadirse fácilmente para habilitar autoescalado.

### ✔ **6. Cache distribuido (recomendación futura)**
Posibilidad de integrar Redis Cache para optimizar consultas frecuentes.

---

## 🧪 Pruebas Unitarias e Integración

### ✔ **Pruebas unitarias con xUnit + Moq**
- `CategoryRepositoryTests`
- `ProductControllerTests`
- `OrderRepositoryTests`

Estas pruebas mockean dependencias y validan repositorios y controladores sin infraestructura real.

### ✔ **Prueba de Integración con Testcontainers**
Incluye un SQL Server real en Docker, migraciones y pruebas de inserción en una base de datos aislada (ej: `InsertCategory_RealDatabase`).

---

## 🚀 Instrucciones para Clonar, Construir y Ejecutar

### 1️⃣ Clonar el repositorio
```bash
git clone https://github.com/camjor/PruebaFinanzaAutos.git
cd Asisya
```

### 2️⃣ Restaurar dependencias
```bash
dotnet restore
```
### 3️⃣ Ejecutar migraciones
```bash
dotnet ef database update
```
### 4️⃣ Ejecutar la API
```bash
dotnet run
```
La API estará disponible en:

HTTP: http://localhost:5000

Swagger UI: Disponible en /swagger.

### 📂 Endpoints principales
Todos requieren JWT (excepto el login).

### 🔐 1. Login
POST /api/usuario/login
Devuelve token JWT e información del usuario.

### 📦 2. Products
GET /api/product – Lista todos los productos (incluye CategoryName y SupplierName).

GET /api/product/{id} – Obtiene un producto específico.

POST /api/product – Crea un producto.

PUT /api/product/{id} – Actualiza un producto.

DELETE /api/product/{id} – Elimina un producto.

POST /api/product/bulk?count=100000 – Carga masiva: genera miles de productos falsos (ideal para pruebas de rendimiento).

### 🧨 Carga masiva
```bash
POST /api/product/bulk
```
Este endpoint genera miles o cientos de miles de productos automáticamente.
La carga se hace por lotes (batch) para no saturar SQL Server.

### 🗂️ 3. Categories
CRUD completo.

### 👤 4. Customers
CRUD completo.

### 🚚 5. Shippers
CRUD completo.

### 👨‍💼 6. Employees
Incluye jerarquía, ManagerName y subordinados.

### 📄 7. Orders
Incluye Customer, Employee, Shipper y Details. Además, endpoint para agregar detalles.

### 💾 SQL Server Seed
En el primer inicio se cargan automáticamente:

Categorías

Productos

Empleados (con jerarquía)

Clientes

Shippers

Órdenes y detalles

Usuario Identity

### 🧱 Tecnologías Utilizadas
.NET 9

Entity Framework Core

Docker

SQL Server o MySQL

AutoMapper

IdentityCore + JWT

xUnit

Moq

Testcontainers (para pruebas de integración)



