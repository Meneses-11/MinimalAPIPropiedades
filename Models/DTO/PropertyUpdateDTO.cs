namespace PropiedadesMinimalAPI.Models.DTO;

public class PropertyUpdateDTO
{
    public int IdPropiedad { get; set; }
    public string NombrePropiedad { get; set; }
    public string Descripcion { get; set; }
    public string Ubicacion { get; set; }
    public bool Activa { get; set; }
}
