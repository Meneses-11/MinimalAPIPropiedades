using System.ComponentModel.DataAnnotations;

namespace PropiedadesMinimalAPI.Models;

public class Property
{
    [Key]
    public int IdPropiedad { get; set; }
    [Required]
    public string NombrePropiedad { get; set; }
    [Required]
    public string Descripcion { get; set; }
    [Required]
    public string Ubicacion { get; set; }
    [Required]
    public bool Activa { get; set; }
    [Required]
    public DateTime? FechaCreacion { get; set; }
}
