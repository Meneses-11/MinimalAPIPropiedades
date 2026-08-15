using FluentValidation;
using PropiedadesMinimalAPI.Models.DTO;

namespace PropiedadesMinimalAPI.Validaciones;

public class ValidationPropertyUpdate : AbstractValidator<PropertyUpdateDTO>
{
    public ValidationPropertyUpdate()
    {
        RuleFor(mdl => mdl.IdPropiedad).NotEmpty().GreaterThan(0);
        RuleFor(mdl => mdl.NombrePropiedad).NotEmpty();
        RuleFor(mdl => mdl.Descripcion).NotEmpty();
        RuleFor(mdl => mdl.Ubicacion).NotEmpty();
    }
}
