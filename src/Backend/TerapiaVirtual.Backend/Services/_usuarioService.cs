using TerapiaVirtual.Backend.Data;
using TerapiaVirtual.Backend.DTOs;
using TerapiaVirtual.Backend.Models;

namespace TerapiaVirtual.Backend.Services;

public class CadastrarUsuarioService
{
    private readonly AppDbContext _dbContext;

    public CadastrarUsuarioService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResultadoDto> CadastrarUsuario(UsuarioDto usuarioDto)
    {
        try
        {
            var novoUsuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                //  Senha = Hashsenha(usuarioDto.Senha)      depois fazer o hash
            };
            await _dbContext.Usuarios.AddAsync(novoUsuario);
            await _dbContext.SaveChangesAsync();

            return new ResultadoDto { Sucesso = true, Mensagem = "Usuario cadastrado com sucesso." };
        }
        catch (Exception ex)
        {
            return new ResultadoDto { Sucesso = false, Mensagem = $"Erro ao cadastrar usuário: {ex.Message}" };
        }
    }
}