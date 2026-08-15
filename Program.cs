using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PropiedadesMinimalAPI.Data;
using PropiedadesMinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//EndPoints

app.MapGet("/api/Properties", (ILogger<Program> logger) =>
{
    logger.LogInformation("Dependency injections");
    return Results.Ok(PropertyData.properties);
}).WithName("GetProperties").Produces<IEnumerable<PropertyDTO>>(200);
app.MapGet("/api/Properties/{id:int}", ([FromRoute] int id) =>
{
    return Results.Ok(PropertyData.properties.FirstOrDefault(prt => prt.IdPropiedad == id));
}).WithName("GetProperty").Produces<Property>(200);
app.MapPost("/api/Properties", ([FromBody] PropertyCreateDTO propertyCreateDTO ) =>
{
    if(string.IsNullOrEmpty(propertyCreateDTO.NombrePropiedad))
    {
        return Results.BadRequest("IdPropiedad incorrecto o nombre vacio");
    }

    if(PropertyData.properties.FirstOrDefault(prt => prt.NombrePropiedad.ToLower() == propertyCreateDTO.NombrePropiedad.ToLower()) != null)
    {
        return Results.BadRequest("Ya existe una propiedad con ese nombre");
    }

    //propertyCreateDTO.IdPropiedad = PropertyData.properties.OrderByDescending(prt => prt.IdPropiedad).First().IdPropiedad + 1;

    Property property = new Property()
    {
        IdPropiedad = PropertyData.properties.OrderByDescending(prt => prt.IdPropiedad).First().IdPropiedad + 1,
        NombrePropiedad = propertyCreateDTO.NombrePropiedad,
        Descripcion = propertyCreateDTO.Descripcion,
        Ubicacion = propertyCreateDTO.Ubicacion,
        Activa = propertyCreateDTO.Activa,
        FechaCreacion = DateTime.Now
    };

    PropertyData.properties.Add(property);


    PropertyDTO propertyDTO = new PropertyDTO()
    {
        IdPropiedad = property.IdPropiedad,
        NombrePropiedad = property.NombrePropiedad,
        Descripcion = property.Descripcion,
        Ubicacion = property.Ubicacion,
        Activa = property.Activa
    };

    return Results.CreatedAtRoute("GetProperty", new {id = propertyDTO.IdPropiedad}, propertyDTO);
}).WithName("PostProperty").Accepts<PropertyDTO>("application/json").Produces<PropertyDTO>(2001).Produces(400);
    

app.UseHttpsRedirection();
app.Run();
