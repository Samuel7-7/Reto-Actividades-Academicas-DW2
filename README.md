# UniCampus - Registro de Actividades Académicas

Proyecto de Desarrollo de Aplicaciones Web II.
Autor: Samuel Alejandro Vanegas Perea

## Tecnologías
C#, ASP.NET Web Forms, .NET Framework 4.8.1, SQL Server y ADO.NET.

## Arquitectura
- UniCampus.Web: páginas (Vista), Presenters e interfaces (MVP)
- UniCampus.BLL: servicios con las validaciones
- UniCampus.DAL: repositorios que consultan SQL Server
- UniCampus.Entities: clases de datos

## Cómo ejecutar
1. Ejecutar `Database/UniCampusDB.sql` en SQL Server Management Studio.
2. Abrir `UniCampus.sln` en Visual Studio.
3. Cambiar el `Data Source` en `UniCampus.Web/Web.config` si su servidor no es `.\SQLEXPRESS`.
4. Ejecutar con F5.

Usuario de prueba: samuel@unicampus.edu / 123456

## Funciones
- Iniciar sesión
- Registrar, listar, buscar, editar y eliminar actividades
- Ver detalle e inscribirse con control de cupos
- Publicar reseñas con calificación de 1 a 5
