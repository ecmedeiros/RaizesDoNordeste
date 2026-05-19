using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RaizesDoNordeste.App.DTOs.Auth;
using RaizesDoNordeste.Domain.Entidades;
using RaizesDoNordeste.Domain.Enums;
using RaizesDoNordeste.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Perfil = RaizesDoNordeste.Domain.Enums.Perfil;

namespace RaizesDoNordeste.App.Services
{

    public class AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
    {
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var usuario = await usuarioRepository
                .ObterPorEmail(request.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha))
            {
                throw new UnauthorizedAccessException("E-mail ou senha inválidos");
            }
            ;

            var token = GerarToken(usuario);

            return new LoginResponse
            {
                AcessToken = token,
                ExpiresIn = 3600,
                Usuario = new UsuarioResponse
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Perfil = usuario.Perfil!.Nome
                }
            };
        }

        public async Task<UsuarioResponse> Registrar(RegistroRequest request)
        {
            var emailExiste = await usuarioRepository
                .ObterPorEmail(request.Email);

            if (emailExiste != null)
            {
                throw new InvalidOperationException(
                    "E-mail já cadastrado.");
            }

            if (!request.AceitouTermos)
            {
                throw new InvalidOperationException(
                    "É necessário aceitar os termos para se cadastrar");
            }

            var usuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                IdPerfil = (int)Perfil.Cliente,
                DataAceiteTermos = DateTime.Now,
            };

            await usuarioRepository.Adicionar(usuario);

            return new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfil = Perfil.Cliente.GetDescription()
            };
        }

        private string GerarToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
            );

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Perfil!.Nome),
                new Claim(ClaimTypes.Name, usuario.Nome)
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
