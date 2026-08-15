using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PropiedadesMinimalAPI.Data;
using PropiedadesMinimalAPI.Mapper;
using PropiedadesMinimalAPI.Models;
using PropiedadesMinimalAPI.Validaciones;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(map => map.AddMaps(typeof(PropertiesMapper)));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

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
app.MapPost("/api/Properties", (IMapper mapper, IValidator<PropertyCreateDTO> validation, [FromBody] PropertyCreateDTO propertyCreateDTO ) =>
{
    var resultValidations = validation.ValidateAsync(propertyCreateDTO).GetAwaiter().GetResult();

    if (!resultValidations.IsValid)
    {
        return Results.BadRequest(resultValidations.Errors.FirstOrDefault().ToString());
    }

    if(PropertyData.properties.FirstOrDefault(prt => prt.NombrePropiedad.ToLower() == propertyCreateDTO.NombrePropiedad.ToLower()) != null)
    {
        return Results.BadRequest("Ya existe una propiedad con ese nombre");
    }

    Property property = mapper.Map<Property>(propertyCreateDTO);
    property.IdPropiedad = PropertyData.properties.OrderByDescending(prt => prt.IdPropiedad).First().IdPropiedad + 1;
    property.FechaCreacion = DateTime.Now;

    PropertyData.properties.Add(property);


    PropertyDTO propertyDTO = mapper.Map<PropertyDTO>(property);

    return Results.CreatedAtRoute("GetProperty", new {id = propertyDTO.IdPropiedad}, propertyDTO);
}).WithName("PostProperty").Accepts<PropertyDTO>("application/json").Produces<PropertyDTO>(2001).Produces(400);
    

app.UseHttpsRedirection();
app.Run();
