📡 Backend API – ExpenseControl

API REST desarrollada en .NET 9, responsable de gestionar presupuestos, gastos, fondos monetarios, depósitos y más dentro del sistema ExpenseControl.

📁 Estructura del Proyecto
Backend.Api/
│
├── Controllers/        # Endpoints principales del sistema
├── Data/               # DbContext, inicialización y configuración de base de datos
├── DTOs/               # Modelos de transferencia de datos
├── Migrations/         # Migraciones de Entity Framework Core
├── obj/                # Archivos de compilación
├── bin/                # Builds Debug/Release
└── appsettings.json    # Configuración del sistema

🚀 Tecnologías Utilizadas

.NET 9 Web API

Entity Framework Core 9

SQL Server

AutoMapper

Swagger / OpenAPI

Microsoft Identity / JWT Ready (referencias incluidas)

LINQ y programación asincrónica (async / await)

📌 Módulos Implementados

La API implementa controladores completos para manejar todos los recursos del sistema:

### 1. Presupuestos (Budgets)

CRUD completo, relaciona fondos monetarios y gastos.

BudgetsController.cs

2. Tipos de Gasto (Expense Types)

Clasificación y organización de categorías de gastos.

ExpenseTypesController.cs

3. Encabezados de Gasto (Expense Headers)

Agrupa gastos antes de asignarlos a un presupuesto.

ExpenseHeadersController.cs

4. Gastos (Expenses)

CRUD y asignación a encabezados o presupuestos.

ExpensesController.cs

5. Fondos Monetarios (Monetary Funds)

Módulo completo de fondos, con código único.

MonetaryFundsController.cs

6. Depósitos (Deposits)

Gestión de ingresos hacia los fondos monetarios.

DepositsController.cs

🧩 Arquitectura

El backend sigue una estructura clara:

DTOs

Todos los módulos poseen:

CreateDto → modelos de entrada

Dto → modelos de retorno para API

Data Layer

AppDbContext.cs contiene:

Definición de entidades

Relaciones

Configuraciones

Seeds iniciales vía DbInitializer.cs

Migrations

Estructura real incluida con migraciones como:

InitialCreate

AddMonetaryFund

FixMonetaryFundCode

AddExpenses

AddDepositsModule

y más…

🛠️ Configuración del Proyecto
1️⃣ Restaurar paquetes
dotnet restore

2️⃣ Aplicar migraciones a la base de datos
dotnet ef database update

3️⃣ Ejecutar el servidor de desarrollo
dotnet run


La API estará disponible en:

https://localhost:5222

4️⃣ Abrir Swagger
https://localhost:5222/swagger

🔧 Configuración de Base de Datos

En appsettings.json se encuentra la cadena de conexión:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ExpenseControl;Trusted_Connection=True;TrustServerCertificate=True"
}


Puedes cambiarla según tu entorno.

📤 Publicación

Para generar un release:

dotnet publish -c Release


Los archivos se generarán en:

/bin/Release/net9.0/

👨‍💻 Estilo y Buenas Prácticas Aplicadas

Patrón DTO + AutoMapper

Controladores limpios con Single Responsibility

Validaciones automáticas basadas en modelos

Separación de capas (Data / Controllers / DTOs)

Uso intensivo de async/await

Uso de Swagger para documentación

📦 Endpoints Principales

Ejemplos:

Budget
GET    /api/budgets
GET    /api/budgets/{id}
POST   /api/budgets
PUT    /api/budgets/{id}
DELETE /api/budgets/{id}

ExpenseTypes
GET    /api/expensetypes
POST   /api/expensetypes
...


Y así sucesivamente para cada módulo.

🏁 Estado Actual del Backend

✔ API funcional y completa
✔ Base de datos estable con migraciones
✔ Integración lista para uso por Angular Frontend
✔ Código limpio, estructurado y escalable
