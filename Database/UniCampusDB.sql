IF DB_ID('UniCampusDB') IS NULL
    CREATE DATABASE UniCampusDB;
GO

USE UniCampusDB;
GO

IF OBJECT_ID('dbo.Resena') IS NOT NULL DROP TABLE dbo.Resena;
IF OBJECT_ID('dbo.Inscripcion') IS NOT NULL DROP TABLE dbo.Inscripcion;
IF OBJECT_ID('dbo.Actividad') IS NOT NULL DROP TABLE dbo.Actividad;
IF OBJECT_ID('dbo.TipoActividad') IS NOT NULL DROP TABLE dbo.TipoActividad;
IF OBJECT_ID('dbo.Usuario') IS NOT NULL DROP TABLE dbo.Usuario;
GO

CREATE TABLE dbo.TipoActividad (
    TipoId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE dbo.Usuario (
    UsuarioId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(150) NOT NULL UNIQUE,
    Programa NVARCHAR(100) NULL,
    Clave CHAR(64) NOT NULL
);

CREATE TABLE dbo.Actividad (
    ActividadId INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(120) NOT NULL,
    Descripcion NVARCHAR(1000) NOT NULL,
    TipoId INT NOT NULL FOREIGN KEY REFERENCES dbo.TipoActividad(TipoId),
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    HoraInicio TIME(0) NOT NULL,
    HoraFin TIME(0) NOT NULL,
    Modalidad NVARCHAR(20) NOT NULL,
    Lugar NVARCHAR(150) NOT NULL,
    Cupos INT NOT NULL CHECK (Cupos > 0),
    Estado NVARCHAR(20) NOT NULL DEFAULT 'Activa',
    CHECK (FechaFin >= FechaInicio),
    CHECK (Estado IN ('Activa', 'Finalizada', 'Cancelada'))
);

CREATE TABLE dbo.Inscripcion (
    InscripcionId INT IDENTITY(1,1) PRIMARY KEY,
    ActividadId INT NOT NULL FOREIGN KEY REFERENCES dbo.Actividad(ActividadId),
    UsuarioId INT NOT NULL FOREIGN KEY REFERENCES dbo.Usuario(UsuarioId),
    FechaInscripcion DATETIME NOT NULL DEFAULT GETDATE(),
    UNIQUE (ActividadId, UsuarioId)
);

CREATE TABLE dbo.Resena (
    ResenaId INT IDENTITY(1,1) PRIMARY KEY,
    ActividadId INT NOT NULL FOREIGN KEY REFERENCES dbo.Actividad(ActividadId),
    UsuarioId INT NOT NULL FOREIGN KEY REFERENCES dbo.Usuario(UsuarioId),
    Calificacion INT NOT NULL CHECK (Calificacion BETWEEN 1 AND 5),
    Comentario NVARCHAR(500) NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    UNIQUE (ActividadId, UsuarioId)
);
GO

INSERT INTO dbo.TipoActividad (Nombre) VALUES
(N'Taller'), (N'Conferencia'), (N'Seminario'), (N'Curso'), (N'Encuentro');

INSERT INTO dbo.Usuario (Nombre, Correo, Programa, Clave) VALUES
(N'Samuel Vanegas', N'samuel@unicampus.edu', N'Ingeniería de Sistemas', CONVERT(CHAR(64), HASHBYTES('SHA2_256', '123456'), 2)),
(N'Ana Torres', N'ana@unicampus.edu', N'Ingeniería de Sistemas', CONVERT(CHAR(64), HASHBYTES('SHA2_256', '123456'), 2)),
(N'Carlos Pérez', N'carlos@unicampus.edu', N'Diseño Gráfico', CONVERT(CHAR(64), HASHBYTES('SHA2_256', '123456'), 2)),
(N'Laura Gómez', N'laura@unicampus.edu', N'Administración', CONVERT(CHAR(64), HASHBYTES('SHA2_256', '123456'), 2));

INSERT INTO dbo.Actividad (Titulo, Descripcion, TipoId, FechaInicio, FechaFin, HoraInicio, HoraFin, Modalidad, Lugar, Cupos, Estado) VALUES
(N'Taller de Programación Web', N'Espacio práctico para reforzar conceptos de programación web con ASP.NET.', 1, '2026-11-27', '2026-11-28', '08:00', '12:00', N'Presencial', N'Laboratorio 3', 30, N'Activa'),
(N'Conferencia de IA', N'Charla sobre inteligencia artificial aplicada a la educación.', 2, '2026-12-02', '2026-12-02', '14:00', '16:00', N'Virtual', N'Microsoft Teams', 100, N'Activa'),
(N'Seminario de Bases de Datos', N'Modelado entidad-relación y consultas en SQL Server.', 3, '2026-12-05', '2026-12-05', '09:00', '12:00', N'Presencial', N'Sala 204', 50, N'Activa'),
(N'Curso de Diseño UX/UI', N'Principios de experiencia de usuario y prototipado.', 4, '2026-12-09', '2026-12-12', '18:00', '20:00', N'Virtual', N'Microsoft Teams', 40, N'Activa'),
(N'Encuentro de Investigación', N'Presentación de avances de los semilleros.', 5, '2026-10-15', '2026-10-15', '08:00', '17:00', N'Presencial', N'Auditorio', 80, N'Finalizada'),
(N'Taller de Seguridad Digital', N'Buenas prácticas para proteger información y cuentas.', 1, '2026-11-30', '2026-11-30', '14:00', '17:00', N'Presencial', N'Laboratorio 2', 2, N'Activa');

INSERT INTO dbo.Inscripcion (ActividadId, UsuarioId) VALUES
(1, 2), (1, 3), (1, 4), (2, 1), (5, 1), (5, 2), (6, 2), (6, 3);

INSERT INTO dbo.Resena (ActividadId, UsuarioId, Calificacion, Comentario) VALUES
(5, 1, 5, N'Muy buena organización.'),
(5, 2, 4, N'Interesante, aunque fue largo.');
GO

SELECT * FROM dbo.Actividad;
GO
