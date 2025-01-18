using Microsoft.EntityFrameworkCore;
using TerapiaVirtual.Backend.Data;
using TerapiaVirtual.Backend.DTOs;
using TerapiaVirtual.Backend.Models;
using TerapiaVirtual.Backend.Utils;

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
            var hashedPassword = PasswordHasher.HashPassword(usuarioDto.Senha);

            var novoUsuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = hashedPassword
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

    public async Task<ResultadoDto> Login(string email, string senha)
    {
        var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);

        if (usuario == null || !PasswordHasher.VerifyPassword(senha, usuario.Senha))
        {
            return new ResultadoDto
            {
                Sucesso = false,
                Mensagem = "Email ou senha inválidos."
            };
        }
        return new ResultadoDto
        {
            Sucesso = true,
            Mensagem = "Login realizado com sucesso!"
        };
    }
}



