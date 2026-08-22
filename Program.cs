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

app.MapGet("/api/Properties", async (PropertiesDBContext _db, IMapper mapper) =>
{
    return Results.Ok(new APIResponse
    {
        Success = true,
        Data = _db.Property.Select(prpt => mapper.Map<PropertyDTO>(prpt)).ToList(),
        StatusCode = HttpStatusCode.OK
    });

}).WithName("GetProperties").Produces<IEnumerable<APIResponse>>(200);

app.MapGet("/api/Properties/{id:int}", async (PropertiesDBContext _db, IMapper mapper, [FromRoute] int id) =>
{
    APIResponse result = new() { Errors = [] };

    if (id <= 0)
    {
        result.Success = false;
        result.Errors.Add("Invalid Id");
        result.StatusCode = HttpStatusCode.BadRequest;
        return Results.BadRequest(result);
    }

    Property? property = await _db.Property.FirstOrDefaultAsync(prt => prt.IdPropiedad == id);

    if(property == null)
    {
        result.Success = false;
        result.Errors.Add("No property was found");
        result.StatusCode = HttpStatusCode.NotFound;
        return Results.NotFound(result);
    }

    result.Success = true;
    result.Data = mapper.Map<PropertyDTO>(property);
    return Results.Ok(result);
}).WithName("GetProperty").Produces<APIResponse>(200).Produces<APIResponse>(400).Produces<APIResponse>(404);

app.MapPost("/api/Properties", async (PropertiesDBContext _db, IMapper mapper, IValidator<PropertyCreateDTO> validation, [FromBody] PropertyCreateDTO propertyCreateDTO ) =>
{
    APIResponse result = new() { Errors = [] };

    var resultValidations = await validation.ValidateAsync(propertyCreateDTO);

    if (!resultValidations.IsValid)
    {
        result.Success = false;
        result.Errors.AddRange(resultValidations.Errors.Select(errs => errs.ErrorMessage));
        result.StatusCode = HttpStatusCode.BadRequest;
        return Results.BadRequest(result);
    }

    if((await _db.Property.FirstOrDefaultAsync(prt => prt.NombrePropiedad.ToLower() == propertyCreateDTO.NombrePropiedad.ToLower())) != null)
    {
        result.Success = false;
        result.Errors.Add("A property with that name already exist");
        result.StatusCode = HttpStatusCode.BadRequest;
        return Results.BadRequest(result);
    }

    Property property = mapper.Map<Property>(propertyCreateDTO);
    property.FechaCreacion = DateTime.UtcNow;

    await _db.Property.AddAsync(property);
    await _db.SaveChangesAsync();

    PropertyDTO propertyDTO = mapper.Map<PropertyDTO>(property);

    result.Success = true;
    result.Data = propertyDTO;
    result.StatusCode = HttpStatusCode.Created;
    return Results.CreatedAtRoute("GetProperty", new { id=  property.IdPropiedad}, result);

}).WithName("PostProperty").Accepts<PropertyCreateDTO>("application/json").Produces<APIResponse>(201).Produces<APIResponse>(400);

app.MapPut("/api/Properties", async (PropertiesDBContext _db, IMapper mapper, IValidator<PropertyUpdateDTO> validation, [FromBody] PropertyUpdateDTO propertyUpdateDTO) =>
{
    APIResponse result = new() { Errors = [] };

    var resultValidations = await validation.ValidateAsync(propertyUpdateDTO);

    if (!resultValidations.IsValid)
    {
        result.Success = false;
        result.Errors.AddRange(resultValidations.Errors.Select(errs => errs.ErrorMessage));
        result.StatusCode = HttpStatusCode.BadRequest;
        return Results.BadRequest(result);
    }

    Property? propertyBD = await _db.Property.FirstOrDefaultAsync(prpt => prpt.IdPropiedad == propertyUpdateDTO.IdPropiedad);

    if(propertyBD == null)
    {
        result.Success = false;
        result.Errors.Add("No property was found");
        result.StatusCode = HttpStatusCode.NotFound;
        return Results.NotFound(result);
    }

    propertyBD.NombrePropiedad = propertyUpdateDTO.NombrePropiedad;
    propertyBD.Descripcion = propertyUpdateDTO.Descripcion;
    propertyBD.Ubicacion = propertyUpdateDTO.Ubicacion;
    propertyBD.Activa = propertyUpdateDTO.Activa;

    await _db.SaveChangesAsync();

    PropertyDTO propertyDTO = mapper.Map<PropertyDTO>(propertyBD);

    result.Success = true;
    result.Data = propertyDTO;
    result.StatusCode = HttpStatusCode.OK;
    return Results.Ok(result);

}).WithName("PutProperty").Accepts<PropertyUpdateDTO>("application/json").Produces<APIResponse>(200).Produces<APIResponse>(400).Produces<APIResponse>(404);

app.MapDelete("/api/Properties/{id:int}", async (PropertiesDBContext _db, [FromRoute] int id) =>
{
    APIResponse result = new() { Errors = [] };

    if(id <= 0)
    {
        result.Success = false;
        result.Errors.Add("Invalid Id");
        result.StatusCode = HttpStatusCode.BadRequest;
        return Results.BadRequest(result);
    }

    Property? propertyDB = await _db.Property.FirstOrDefaultAsync(prpt => prpt.IdPropiedad == id);

    if (propertyDB == null)
    {
        result.Success = false;
        result.Errors.Add("No property was found");
        result.StatusCode = HttpStatusCode.NotFound;
        return Results.NotFound(result);
    }

    _db.Remove(propertyDB);
    await _db.SaveChangesAsync();

    return Results.NoContent();

}).WithName("DeleteProperty").Produces(204).Produces<APIResponse>(400).Produces<APIResponse>(404);

app.UseHttpsRedirection();
app.Run();
