namespace TerapiaVirtual.Backend.Models;

public class Sessao
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}