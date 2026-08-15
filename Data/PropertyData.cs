using PropiedadesMinimalAPI.Models;

namespace PropiedadesMinimalAPI.Data;

public static class PropertyData
{
    public static List<Property> properties = new List<Property> 
    {
        new Property{IdPropiedad = 1, NombrePropiedad = "Casa las palmas", Descripcion = "Hermosa casa con jardín y alberca", Ubicacion = "Tuxtla Gutierrez", Activa = true, FechaCreacion = DateTime.Now.AddDays(-10)},
        new Property{IdPropiedad = 2, NombrePropiedad = "Departamento centro", Descripcion = "Cómodo departamento cerca del parque central", Ubicacion = "Tuxtla Gutierrez", Activa = true, FechaCreacion = DateTime.Now.AddDays(-8)},
        new Property{IdPropiedad = 3, NombrePropiedad = "Casa residencial", Descripcion = "Amplia casa en zona residencial privada", Ubicacion = "Tuxtla Gutierrez", Activa = false, FechaCreacion = DateTime.Now.AddDays(-15)},
        new Property{IdPropiedad = 4, NombrePropiedad = "Terreno el vergel", Descripcion = "Terreno de 500m2 con vista al bosque", Ubicacion = "Chiapa de Corzo", Activa = true, FechaCreacion = DateTime.Now.AddDays(-5)},
        new Property{IdPropiedad = 5, NombrePropiedad = "Casa san miguel", Descripcion = "Casa de dos pisos con terraza", Ubicacion = "San Cristobal", Activa = true, FechaCreacion = DateTime.Now.AddDays(-20)},
        new Property{IdPropiedad = 6, NombrePropiedad = "Departamento moderno", Descripcion = "Departamento nuevo, totalmente equipado", Ubicacion = "Tuxtla Gutierrez", Activa = false, FechaCreacion = DateTime.Now.AddDays(-3)},
        new Property{IdPropiedad = 7, NombrePropiedad = "Casa campo", Descripcion = "Propiedad rural con jardín y huerta", Ubicacion = "Chiapa de Corzo", Activa = true, FechaCreacion = DateTime.Now.AddDays(-12)},
        new Property{IdPropiedad = 8, NombrePropiedad = "Local comercial", Descripcion = "Local en zona de alto tránsito peatonal", Ubicacion = "Tuxtla Gutierrez", Activa = true, FechaCreacion = DateTime.Now.AddDays(-7)},
        new Property{IdPropiedad = 9, NombrePropiedad = "Casa colonial", Descripcion = "Casa estilo colonial con 3 recámaras", Ubicacion = "San Cristobal", Activa = false, FechaCreacion = DateTime.Now.AddDays(-25)},
        new Property{IdPropiedad = 10, NombrePropiedad = "Terreno comercial", Descripcion = "Terreno con acceso principal para negocio", Ubicacion = "Tuxtla Gutierrez", Activa = true, FechaCreacion = DateTime.Now.AddDays(-1)}
    };

    
}
