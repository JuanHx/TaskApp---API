# TaskApp - API en .NET con Entity Framework Core

##  Descripción

TaskApp es una API REST desarrollada en **.NET 8** con **Entity Framework Core** y una base de datos en memoria (**InMemoryDatabase**). 
Permite la gestión de tareas, incluyendo la creación, actualización y eliminación de registros.

## Tecnologías Utilizadas

- **.NET 8**
- **C#**
- **Entity Framework Core**
- **InMemory Database**
- **Swagger para documentación**
- **Azure App Service** (para despliegue)

---

###  Estructura del Proyecto

```
TaskApp/
│-- Controllers/
│   ├── TareasController.cs
│-- Data/
│   ├── AppDBContext.cs
│-- Models/
│   ├── Tareas.cs
│-- Program.cs
│-- README.md
```

---

##  Instalación y Ejecución Local

### Clonar el Repositorio**

git clone https://github.com/tu-usuario/TaskApp---API.git

cd taskApp


### **2️⃣ Restaurar Dependencias**

dotnet restore


### Ejecutar la API**

dotnet run

La API estará disponible en: [http://localhost:5000]

--

##  Endpoints de la API

| Método | Endpoint								| Descripción               |
| ------ | ------------------------------------ | ------------------------  |
| GET    | `/api/tasks`							| Obtener todas las tareas  |
| GET    | `/api/tasks/{id}`					| Obtener una tarea por ID  |
| GET    | `/api/tasks/filtrar?estado={estado}` | Obtener tareas por estado |
| POST   | `/api/tasks`							| Crear una nueva tarea     |
| PUT    | `/api/tasks/{id}`					| Actualizar una tarea      |
| DELETE | `/api/tasks/{id}`					| Eliminar una tarea        |

### **Ejemplo de Petición POST**

```json
{
  "titulo": "Nueva Tarea",
  "descripcion": "Descripción de la tarea",
  "estado": 0
}
```

### **Ejemplo de los Diferentes Estados**
```json
{
  "estado": 0, // Pendiente
  "estado": 1, // En Proceso
  "estado": 2  // Completada
}
