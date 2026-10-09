# GameVault

## Integrantes

- Vanessa Jaeger
- Andres David Quispe Valencia
- Jampier Orlando Tuya Barja
- Gian Pier Alessandro Tovar Inuma

## Descripción del proyecto

**GameVault** es una aplicación web desarrollada para la gestión y visualización de productos tecnológicos relacionados con el mundo gaming, como periféricos y accesorios.

El proyecto busca ofrecer una interfaz intuitiva y moderna que permita a los usuarios consultar los productos disponibles y ponerse en contacto con la administración de la tienda.

## Funcionalidades principales

- Visualización de productos en la página principal.
- Gestión de productos mediante operaciones CRUD (crear, consultar, editar y eliminar).
- Inicio de sesión para el administrador.
- Gestión de mensajes enviados mediante el formulario de contacto.
- Validación de formularios.
- Gestión de la información mediante una base de datos SQL Server.

## Tecnologías utilizadas

- **Lenguaje:** C#
- **Framework:** ASP.NET Core MVC
- **Acceso a datos:** Entity Framework Core
- **Base de datos:** Microsoft SQL Server
- **Frontend:** HTML, CSS, JavaScript y Bootstrap
- **Entorno de desarrollo:** Visual Studio
- **Control de versiones:** Git y GitHub
- **Publicación local:** IIS (Internet Information Services)

## Requisitos previos

Para ejecutar el proyecto, necesitas:

- Visual Studio con las herramientas de desarrollo web ASP.NET.
- El SDK de .NET compatible con la versión del proyecto.
- Microsoft SQL Server.
- SQL Server Management Studio (SSMS), recomendado para administrar la base de datos.
- Git, para clonar el repositorio.

## Cómo ejecutar el proyecto

### 1. Clonar el repositorio

Abre una terminal y ejecuta:


git clone https://github.com/rootless404x/GameVault.git


Entra en la carpeta del proyecto:


cd GameVault


### 2. Abrir el proyecto

Abre la solución de GameVault (`.sln`) en Visual Studio. Si el repositorio no contiene una solución, abre el archivo del proyecto (`.csproj`).

Espera a que Visual Studio restaure los paquetes NuGet necesarios.

### 3. Configurar la conexión a la base de datos

Configura la cadena de conexión a SQL Server en `appsettings.json` o mediante la configuración local correspondiente.

Ejemplo de una cadena de conexión para SQL Server local:


{
  "ConnectionStrings": {
    "ConexionTienda": "Server=localhost;Database=GameVaultDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}


**Importante:** ajusta el servidor y el método de autenticación según tu instalación de SQL Server. No publiques contraseñas ni credenciales reales en GitHub.

### 4. Crear y actualizar la base de datos

Abre la consola del Administrador de paquetes NuGet de Visual Studio y ejecuta:


Update-Database


Este comando aplica las migraciones de Entity Framework Core pendientes para crear o actualizar la estructura de la base de datos. Si los procedimientos almacenados están incluidos en las migraciones, también se crearán al aplicar las migraciones correspondientes.

### 5. Ejecutar la aplicación

En Visual Studio, selecciona el perfil de ejecución del proyecto y pulsa **F5** o **Ctrl + F5**.

La aplicación se abrirá en el navegador con la dirección local configurada por Visual Studio.

## Publicación

El proyecto también se ha publicado en IIS para su ejecución en un entorno local. Para utilizar esta modalidad en otro equipo, es necesario configurar IIS, instalar el Hosting Bundle de .NET correspondiente y configurar la conexión a SQL Server.

## Repositorio

Código fuente de GameVault:

https://github.com/rootless404x/GameVault
