using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropiedadesMinimalAPI.Data;
using PropiedadesMinimalAPI.Mapper;
using PropiedadesMinimalAPI.Models;
using PropiedadesMinimalAPI.Models.DTO;
using PropiedadesMinimalAPI.Validaciones;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PropertiesDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionLocalSQL"));
});

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

app.MapGet("/api/Properties", (ILogger<Program> logger, IMapper mapper) =>
{
    logger.LogInformation("Dependency injections");

    return Results.Ok(new APIResponse
    {
        Success = true,
        Data = PropertyData.properties.Select(prpt => mapper.Map<PropertyDTO>(prpt)).ToList(),
        StatusCode = HttpStatusCode.OK
    });

}).WithName("GetProperties").Produces<IEnumerable<APIResponse>>(200);

app.MapGet("/api/Properties/{id:int}", (IMapper mapper, [FromRoute] int id) =>
{
    return Results.Ok(new APIResponse
    {
        Success = true,
        Data = mapper.Map<PropertyDTO>(PropertyData.properties.FirstOrDefault(prt => prt.IdPropiedad == id)),
        StatusCode = HttpStatusCode.OK
    });
}).WithName("GetProperty").Produces<APIResponse>(200);

app.MapPost("/api/Properties", async (IMapper mapper, IValidator<PropertyCreateDTO> validation, [FromBody] PropertyCreateDTO propertyCreateDTO ) =>
{
    APIResponse result = new APIResponse() { Errors = [] };

    var resultValidations = await validation.ValidateAsync(propertyCreateDTO);

    if (!resultValidations.IsValid)
    {
        result.Success = false;
        result.Errors.Add(resultValidations.Errors.FirstOrDefault().ToString());
        result.StatusCode = HttpStatusCode.BadRequest;
        return Results.BadRequest(result);
    }

    if(PropertyData.properties.FirstOrDefault(prt => prt.NombrePropiedad.ToLower() == propertyCreateDTO.NombrePropiedad.ToLower()) != null)
    {
        result.Success = false;
        result.Errors.Add("Ya existe una propiedad con ese nombre");
        result.StatusCode = HttpStatusCode.BadRequest;
        return Results.BadRequest(result);
    }

    Property property = mapper.Map<Property>(propertyCreateDTO);
    property.IdPropiedad = PropertyData.properties.OrderByDescending(prt => prt.IdPropiedad).First().IdPropiedad + 1;
    property.FechaCreacion = DateTime.Now;

    PropertyData.properties.Add(property);


    PropertyDTO propertyDTO = mapper.Map<PropertyDTO>(property);

    //return Results.CreatedAtRoute("GetProperty", new {id = propertyDTO.IdPropiedad}, propertyDTO);

    result.Success = true;
    result.Data = propertyDTO;
    result.StatusCode = HttpStatusCode.Created;
    return Results.Ok(result);

}).WithName("PostProperty").Accepts<PropertyCreateDTO>("application/json").Produces<APIResponse>(201).Produces(400);

app.MapPut("/api/Properties", async (IMapper mapper, IValidator<PropertyUpdateDTO> validation, [FromBody] PropertyUpdateDTO propertyUpdateDTO) =>
{
    APIResponse result = new APIResponse() { Errors = [] };

    var resultValidations = await validation.ValidateAsync(propertyUpdateDTO);

    if (!resultValidations.IsValid)
    {
        result.Success = false;
        result.Errors.Add(resultValidations.Errors.FirstOrDefault().ToString());
        result.StatusCode = HttpStatusCode.BadRequest;
        return Results.BadRequest(result);
    }

    Property propertyBD = PropertyData.properties.FirstOrDefault(prpt => prpt.IdPropiedad == propertyUpdateDTO.IdPropiedad);

    if(propertyBD == null)
    {
        result.Success = false;
        result.Errors.Add("No se encontro ninguna propiedad con ese ID");
        result.StatusCode = HttpStatusCode.NotFound;
        return Results.NotFound(result);
    }

    propertyBD.NombrePropiedad = propertyUpdateDTO.NombrePropiedad;
    propertyBD.Descripcion = propertyUpdateDTO.Descripcion;
    propertyBD.Ubicacion = propertyUpdateDTO.Ubicacion;
    propertyBD.Activa = propertyUpdateDTO.Activa;

    PropertyDTO propertyDTO = mapper.Map<PropertyDTO>(propertyBD);

    result.Success = true;
    result.Data = propertyDTO;
    result.StatusCode = HttpStatusCode.Created;
    return Results.Ok(result);

}).WithName("PutProperty").Accepts<PropertyUpdateDTO>("application/json").Produces<APIResponse>(200).Produces(400).Produces(404);

app.MapDelete("/api/Propierties/{id:int}", ([FromRoute] int id) =>
{
    APIResponse result = new APIResponse { Errors = [] };

    Property propertyDB = PropertyData.properties.FirstOrDefault(prpt => prpt.IdPropiedad == id);

    if (propertyDB == null)
    {
        result.Success = false;
        result.Errors.Add("No se encontro ninguna propiedad con ese ID");
        result.StatusCode = HttpStatusCode.NotFound;
        return Results.NotFound(result);
    }

    PropertyData.properties.Remove(propertyDB);

    result.Success = true;
    result.StatusCode = HttpStatusCode.NoContent;
    return Results.Ok(result);

}).WithName("DeleteProperty").Produces<APIResponse>(204).Produces(400).Produces(404);

app.UseHttpsRedirection();
app.Run();
