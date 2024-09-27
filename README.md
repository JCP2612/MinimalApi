# Minimal Api

Minimal Api es una aplicación RESTful para una tienda en línea. Proporciona funcionalidades para registro y autenticación de usuarios, manejo de productos y categorías, y gestión de productos favoritos. Este proyecto fue desarrollado como parte de una prueba técnica.

## Características

- **Autenticación**: Registro y login de usuarios.
- **CRUD de Productos**: Crear, leer, actualizar y eliminar productos.
- **CRUD de Categorías**: Crear, leer, actualizar y eliminar categorías.
- **Filtro de Productos por Categoría**: Filtrar productos por categorías específicas.
- **Gestión de Productos deseados**: Los usuarios pueden marcar productos como deseados.

## Tecnologías Utilizadas

- **Backend**: ASP.NET Core, Entity Framework Core
- **Base de Datos**: PostgreSQL
- **Autenticación**: Validación de contraseña y nombre de usuario
- **ORM**: Entity Framework Core para el acceso a datos
- **Patrón de Diseño**: Arquitectura limpia y casos de uso (Use Cases)

## Requisitos Previos

- .NET SDK 8.0+
- PostgreSQL
- IDE: Visual Studio, Visual Studio Code o cualquier otro editor compatible con .NET

## Instalación

Sigue estos pasos para clonar e instalar las dependencias del proyecto:

1. **Clona el repositorio:**

   ```bash
   git clone https://github.com/JCP2612/MinimalApi.git
   ```
2. **Navega al directorio del proyecto:**

   ```bash
   cd MinimalApi
   ```
3. **Restaura las dependencias del proyecto:**

   ```bash
   dotnet restore
   ```

### Configuración de la Base de Datos
1. **Configura la conexión a la base de datos:**

   Abre el archivo appsettings.json y configura la cadena de conexión a tu base de datos. Por ejemplo:
    
   ```
   "ConnectionStrings": {
     "DefaultConnection": "Server=your_server;Database=StoreF1;User Id=your_user;Password=your_password;"
   }
   ```
2. **Ejecuta las migraciones (si se utilizan Entity Framework Core):**
   ```
   dotnet ef database update
   ```
   Esto creará la base de datos y aplicará todas las migraciones necesarias.

### Ejecución del Proyecto
Para ejecutar el proyecto en tu entorno local:

1. **Ejecuta el servidor de desarrollo:**
   ```
   dotnet run
   ```
   Esto iniciará la aplicación en modo de desarrollo. Por defecto, estará disponible en http://localhost:5077 0 https://localhost:5137.
2. **Acceder a la documentación de la API:**
   Si se utiliza Swagger para documentar la API, puedes acceder a la interfaz de Swagger en:
   ```
   https://localhost:5137/swagger
   ```

### Estructura del proyecto
Una breve descripción de la estructura del proyecto:
```
ProductsNet/
├── Migrations/         # Migraciones generadas para la contrucción de la BD
├── source/                # Contenido principal del proyecto
|  ├── Application/     # Capa de aplicación
|  |  ├── DTO/         # Estructura de datos para casos de uso
|  ├── Domain/          # Capa de dominio
|  ├── Infraestructure/ # Capa de infraestructura
|  |  ├── Data/         # Conexión a la base de datos
├── appsettings.json    # Configuraciones de la aplicación
├── Program.cs          # Punto de entrada de la aplicación
└── ProductsNet.csproj     # Archivo de proyecto de .NET
```
### Contribución
Si deseas contribuir al proyecto, por favor crea una rama nueva a partir de main, realiza tus cambios y abre un Pull Request.
