using Microsoft.AspNetCore.Mvc;
using TerapiaVirtual.Backend.DTOs;
using TerapiaVirtual.Backend.Services;
using TerapiaVirtual.Backend.Utils;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly CadastrarUsuarioService _cadastrarUsuarioService;

    public UsuarioController(CadastrarUsuarioService cadastrarUsuarioService)
    {
        _cadastrarUsuarioService = cadastrarUsuarioService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UsuarioDto usuarioDto)
    {
        if (string.IsNullOrWhiteSpace(usuarioDto.Senha))
        {
            return BadRequest("A senha não pode estar vazia.");
        }
        
        var senhaHashed = PasswordHasher.HashPassword(usuarioDto.Senha);
        usuarioDto.Senha = senhaHashed;

        var resultado = await _cadastrarUsuarioService.CadastrarUsuario(usuarioDto);

        if (resultado.Sucesso)
        {
            return Ok(resultado.Mensagem);
        }
        else
        {
            return BadRequest(resultado.Mensagem);
        }
    }
}