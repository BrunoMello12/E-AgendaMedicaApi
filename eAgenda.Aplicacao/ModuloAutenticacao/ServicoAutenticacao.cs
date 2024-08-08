using eAgenda.Dominio.ModuloAutenticacao;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Aplicacao.ModuloAutenticacao
{
    public class ServicoAutenticacao : ServicoBase<Usuario, ValidadorUsuario>
    {
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> sign;

        public ServicoAutenticacao(UserManager<Usuario> userManager, SignInManager<Usuario> sign)
        {
            this.userManager = userManager;
            this.sign = sign;
        }

        public async Task<Result<Usuario>> RegistrarAsync(Usuario usuario, string senha)
        {
            Result resultado = Validar(usuario);

            if (resultado.IsFailed)
            {
                return Result.Fail(resultado.Errors);
            }

            var usuarioEncontrado = await userManager.FindByEmailAsync(usuario.Email);

            if (usuarioEncontrado != null)
            {
                return Result.Fail($"e-mail {usuarioEncontrado.Email} já cadastrado");
            }

            IdentityResult usuarioResult = await userManager.CreateAsync(usuario, senha);

            if (usuarioResult.Succeeded == false)
            {
                return Result.Fail(usuarioResult.Errors.Select(erro => new Error(erro.Description)));
            }

            return Result.Ok(usuario);
        }

        public async Task<Result<Usuario>> LoginAsync(string login, string senha)
        {
            var loginResult = await sign.PasswordSignInAsync(login, senha, false, lockoutOnFailure: true);

            var erros = new List<IError>();

            if (loginResult.IsLockedOut)
                erros.Add(new Error("O acesso para este usuário foi bloqueado"));

            if (loginResult.IsNotAllowed)
                erros.Add(new Error("O login ou a senha estão incorretas"));

            if (loginResult.Succeeded)
            {
                var usuario = await userManager.FindByNameAsync(login);
                return Result.Ok(usuario);
            }
            else
            {
                if (erros.Count == 0)
                    erros.Add(new Error("Login ou senha incorretos"));

                return Result.Fail(erros);
            }
        }

        public async Task<Result<Usuario>> LogoutAsync()
        {
            await sign.SignOutAsync();

            return Result.Ok(); ;
        }
    }
}
