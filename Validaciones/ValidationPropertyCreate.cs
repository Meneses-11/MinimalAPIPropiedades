using FluentValidation;
using PropiedadesMinimalAPI.Models.DTO;

namespace PropiedadesMinimalAPI.Validaciones;

public class ValidationPropertyCreate : AbstractValidator<PropertyCreateDTO>
{
    public ValidationPropertyCreate()
    {
        RuleFor(model => model.NombrePropiedad).NotEmpty();
        RuleFor(model => model.Descripcion).NotEmpty();
        RuleFor(model => model.Ubicacion).NotEmpty();
    }
}
