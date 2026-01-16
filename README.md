#Prueba Técnica Atica

##Sistema de Gestión de Usuarios
Proyecto para prueba técnica FullStack .Net para Ática. Consiste en un CRUD completo de usuarios con validaciones de negocio y una interfaz moderna.

##Instalación y Configuración
1. Base de Datos
El sistema utiliza SQL Server Express.

Se debe ejecutar el script incluido en la raíz del proyecto GestiónUsuarios: script.sql.

El script crea la base de datos GestionUsuariosDB y la tabla Usuarios.

2. Configuración de Conexión
Abrir el archivo appsettings.Development.json en el proyecto Web (GestiónUsuarios) y actualizar la cadena de conexión::

3. Ejecución
Abrir la solución (.sln) en Visual Studio 2022 o superior.


##Explicación Técnica

###Arquitectura de N-Capas
Se implementó una arquitectura desacoplada para garantizar la mantenibilidad y escalabilidad del sistema:

*GestiónUsuarios.Web: Capa de presentación (ASP.NET Core MVC 8). Contiene los controladores, vistas y ViewModels.

*GestiónUsuarios.Services: Capa de negocio. Contiene la lógica de negocio y validaciones.

*GestiónUsuarios.Core: Contiene las entidades de dominio e interfaces.

*GestiónUsuarios.Data: Capa de infraestructura. Implementa el acceso a datos utilizando Dapper.

Adicional:

*GestiónUsuarios.Test: Contiene test básicos sobre la capa de servicios.

##Tecnologías Utilizadas
*Backend: .NET 8, C#, Dapper.

*Frontend: Razor Pages, Bootstrap 5, Bootstrap Icons, JavaScript (Vanilla).

*Database: SQL Server Express.

*Testing: xUnit, Moq.

##Notas adicionales
El sistema incluye un Rol Simulado "Administrador" (permisos totales) y "Usuario" (solo lectura/creación de tipo Usuario), para cambiar de rol, se debe cambiar la variable _rolSimulado en UsuariosController y volver a ejecutar.

Todos los campos del formulario son requeridos, los campos nombre, apellido y email tienen una validación en el largo (50 para nombre y apellido, 100 para email), el documento se valida que sean entre 7 y 8 números y el mail debe estar en un formato correcto.