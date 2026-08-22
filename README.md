# MinimalAPIPropiedades

API REST desarrollada con **ASP.NET Core Minimal APIs** para la gestión de propiedades.

El proyecto fue desarrollado como parte de mi formación práctica en desarrollo backend con **C#/.NET**, con el objetivo de reforzar el desarrollo de APIs utilizando el enfoque de Minimal APIs.

## Tecnologías

- C#
- ASP.NET Core Minimal APIs
- Entity Framework Core
- SQL Server
- Code First
- Migrations
- DTOs
- AutoMapper
- FluentValidation
- Dependency Injection
- Swagger / OpenAPI

## Funcionalidades

La API permite realizar operaciones CRUD sobre propiedades:

- Consultar propiedades.
- Consultar una propiedad específica.
- Crear propiedades.
- Actualizar propiedades.
- Eliminar propiedades.
- Validar datos mediante FluentValidation.

## API

| Método | Endpoint | Descripción |
|---|---|---|
| GET | `/api/Properties` | Obtener propiedades |
| GET | `/api/Properties/{id}` | Obtener una propiedad |
| POST | `/api/Properties` | Crear una propiedad |
| PUT | `/api/Properties/{id}` | Actualizar una propiedad |
| DELETE | `/api/Properties/{id}` | Eliminar una propiedad |

La API puede explorarse y probarse mediante **Swagger/OpenAPI**.

## 

**Adrian Manuel Meneses López**

Ingeniero en Sistemas Computacionales enfocado en desarrollo backend con **C#/.NET, APIs REST y SQL Server**.