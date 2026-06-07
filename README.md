# SIGEBI - Sistema de Gestion de Bibliotecas

Este repositorio contiene la estructura de arquitectura inicial para el proyecto SIGEBI (Sistema de Gestion de Bibliotecas). El diseño sigue un patron de arquitectura por capas, organizando el codigo de forma modular y desacoplada para facilitar el desarrollo futuro.

---

## Nota Aclaratoria sobre el Alcance

**Importante:** Esta entrega corresponde **unicamente a la estructura inicial de la arquitectura y organizacion del proyecto** (solucion, proyectos por capas, carpetas y modelos de datos basicos en el dominio). No contiene una implementacion funcional, logica de negocio completa, bases de datos configuradas ni pantallas funcionales.

---

## Estructura por Capas

El proyecto esta organizado de la siguiente manera:

- **API/**
  - `SIGEBI.Api/`: Punto de entrada de la interfaz de programacion de aplicaciones (Web API).
- **Application/**
  - `SIGEBI.Application/`: Capa que contendra las reglas de aplicacion y casos de uso.
- **Core/**
  - `SIGEBI.Domain/`: Nucleo del sistema. Contiene las entidades base (`AuditEntity`, `OperationResult`), las entidades del negocio (Usuario, Libro, Prestamo, etc.) y las interfaces de repositorio (`IBaseRepository`).
  - `SIGEBI.Model/`: Biblioteca auxiliar para modelos de datos compartidos.
- **Infrastructure/**
  - `SIGEBI.Infrastructure/`: Implementaciones de servicios de infraestructura externa (como notificaciones, logs).
  - `SIGEBI.Persistence/`: Implementacion de acceso a datos y repositorios concretos.
- **IOC/**
  - `SIGEBI.IOC/`: Modulo encargado de la Inyeccion de Dependencias y registro de servicios.
- **Test/**
  - `SIGEBI.Application.Test/`: Pruebas unitarias para la capa de aplicacion.
  - `SIGEBI.Persistence.Test/`: Pruebas de integracion y unitarias para la persistencia.
- **Web/**
  - `SIGEBI.Web/`: Interfaz de usuario basada en Web.

---

## Ramas de Trabajo del Repositorio

Para el desarrollo del proyecto, se han definido y creado las siguientes ramas de caracteristicas (feature branches) partiendo de la rama principal:

- `feature/domain`: Desarrollo del modelo de dominio y entidades Core.
- `feature/application`: Desarrollo de los casos de uso y logica de aplicacion.
- `feature/persistence`: Implementacion de acceso a datos y repositorios.
- `feature/api`: Creacion de controladores y endpoints de la API.
- `feature/web`: Diseño e integracion de la interfaz web.
