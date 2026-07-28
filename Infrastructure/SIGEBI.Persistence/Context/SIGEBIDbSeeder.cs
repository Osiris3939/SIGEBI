using System;
using System.Linq;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Entities.Library;
using SIGEBI.Domain.Entities.Loan;
using SIGEBI.Domain.Entities.Notification;

namespace SIGEBI.Persistence.Context
{
    // Clase para la siembra inicial de datos de prueba en la base de datos (Seeder)
    public static class SIGEBIDbSeeder
    {
        public static void Initialize(SIGEBIContext context)
        {
            context.Database.EnsureCreated();

            // 1. Usuarios
            if (!context.Usuarios.Any())
            {
                context.Usuarios.AddRange(
                    new Usuario
                    {
                        Id = 1,
                        Nombre = "David",
                        Apellido = "Peguero Santana",
                        Correo = "david00817@gmail.com",
                        Password = "password123",
                        RolUsuarioId = 2,
                        TipoUsuarioId = 1,
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "Sistema",
                        Estado = true
                    },
                    new Usuario
                    {
                        Id = 2,
                        Nombre = "David",
                        Apellido = "Martínez Pérez",
                        Correo = "david1234@gmail.com",
                        Password = "password123",
                        RolUsuarioId = 2,
                        TipoUsuarioId = 1,
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "Sistema",
                        Estado = true
                    },
                    new Usuario
                    {
                        Id = 3,
                        Nombre = "Juan",
                        Apellido = "Peguero Torres",
                        Correo = "juan1234@gmail.com",
                        Password = "password123",
                        RolUsuarioId = 1,
                        TipoUsuarioId = 1,
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "Sistema",
                        Estado = true
                    }
                );
                context.SaveChanges();
            }

            // 2. Recursos Bibliograficos
            if (!context.RecursosBibliograficos.Any())
            {
                context.RecursosBibliograficos.AddRange(
                    new RecursoBibliografico
                    {
                        Id = 1,
                        Titulo = "El Principito (Edición Especial)",
                        Autor = "Antoine de Saint-Exupéry",
                        Editorial = "Literatura",
                        AnioPublicacion = 2020,
                        CategoriaId = 1,
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "Sistema",
                        Estado = true
                    },
                    new RecursoBibliografico
                    {
                        Id = 2,
                        Titulo = "Cien años de soledad",
                        Autor = "Gabriel García Márquez",
                        Editorial = "Novela literaria",
                        AnioPublicacion = 2019,
                        CategoriaId = 1,
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "Sistema",
                        Estado = true
                    }
                );
                context.SaveChanges();
            }

            // 3. Prestamos
            if (!context.Prestamos.Any())
            {
                context.Prestamos.AddRange(
                    new Prestamo
                    {
                        Id = 1,
                        UsuarioId = 3,
                        EjemplarId = 1,
                        FechaPrestamo = DateTime.Now.AddDays(-7),
                        FechaDevolucionPactada = DateTime.Now.AddDays(3),
                        EstadoPrestamo = "Activo",
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "Sistema",
                        Estado = true
                    },
                    new Prestamo
                    {
                        Id = 2,
                        UsuarioId = 3,
                        EjemplarId = 2,
                        FechaPrestamo = DateTime.Now.AddDays(-4),
                        FechaDevolucionPactada = DateTime.Now.AddDays(6),
                        EstadoPrestamo = "Activo",
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "Sistema",
                        Estado = true
                    }
                );
                context.SaveChanges();
            }

            // 4. Penalizaciones
            if (!context.Penalizaciones.Any())
            {
                context.Penalizaciones.AddRange(
                    new Penalizacion
                    {
                        Id = 1,
                        UsuarioId = 3,
                        PrestamoId = 1,
                        MontoMulta = 50.00m,
                        Motivo = "Retraso leve en devolución",
                        Pagada = false,
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "Sistema",
                        Estado = true
                    },
                    new Penalizacion
                    {
                        Id = 2,
                        UsuarioId = 2,
                        PrestamoId = 2,
                        MontoMulta = 120.00m,
                        Motivo = "Retraso de 3 días en la devolución del ejemplar",
                        Pagada = false,
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "Sistema",
                        Estado = true
                    }
                );
                context.SaveChanges();
            }

            // 5. Notificaciones
            if (!context.Notificaciones.Any())
            {
                context.Notificaciones.AddRange(
                    new Notificacion
                    {
                        Id = 1,
                        UsuarioId = 3,
                        Mensaje = "Devolucion Pendiente para mañana",
                        FechaEnvio = DateTime.Now.AddDays(-1),
                        Leida = false,
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "Sistema",
                        Estado = true
                    },
                    new Notificacion
                    {
                        Id = 2,
                        UsuarioId = 2,
                        Mensaje = "Devolución Pendiente para el Martes",
                        FechaEnvio = DateTime.Now,
                        Leida = false,
                        FechaRegistro = DateTime.Now,
                        UsuarioRegistro = "Sistema",
                        Estado = true
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
