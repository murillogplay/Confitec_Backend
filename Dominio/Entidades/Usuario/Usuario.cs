using Dominio.Argumentos.Usuario;
using Dominio.Recurso;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Teste.Domain.Entities.Base;

namespace Dominio.Entidades.Usuario
{
    public class Usuario : EntityBase
    { 
        public string Nome { get; private set; }
        public string SobreNome { get; private set; }
        public string Email { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public int Escolaridade { get; private set; }
        public Usuario() { }
        public Usuario(UsuarioRequest request)
        {
            Nome = request.Nome;
            SobreNome = request.SobreNome;
            Email = request.Email;
            DataNascimento = request.DataNascimento;
            Escolaridade = request.Escolaridade;

            new AddNotifications<Usuario>(this)
            .IfNullOrEmpty(x => x.Nome, Mensagens.CAMPO_X0_INVALIDO.ToFormat("Nome"))
            .IfNullOrEmpty(x => x.SobreNome, Mensagens.CAMPO_X0_INVALIDO.ToFormat("SobreNome"))
            .IfNotEmail(x => x.Email, Mensagens.CAMPO_X0_INVALIDO.ToFormat("Email"))
            .IfGreaterOrEqualsThan(x => x.DataNascimento, DateTime.Now, Mensagens.CAMPO_X0_INVALIDO.ToFormat("DataNascimento"))
            .IfEqualsZero(x => x.Escolaridade, Mensagens.CAMPO_X0_INVALIDO.ToFormat("Escolaridade"));
        }
        public void Atualizar(UsuarioRequest request)
        {
            { 
                Nome = request.Nome;
                SobreNome = request.SobreNome;
                Email = request.Email;
                DataNascimento = request.DataNascimento;
                Escolaridade = request.Escolaridade;

                new AddNotifications<Usuario>(this)
                .IfEqualsZero(x => x.Id, Mensagens.CAMPO_X0_INVALIDO.ToFormat("Id"))
                .IfNullOrEmpty(x => x.Nome, Mensagens.CAMPO_X0_INVALIDO.ToFormat("Nome"))
                .IfNullOrEmpty(x => x.SobreNome, Mensagens.CAMPO_X0_INVALIDO.ToFormat("SobreNome"))
                .IfNotEmail(x => x.Email, Mensagens.CAMPO_X0_INVALIDO.ToFormat("Email"))
                .IfGreaterOrEqualsThan(x => x.DataNascimento, DateTime.Now, Mensagens.CAMPO_X0_INVALIDO.ToFormat("DataNascimento"))
                .IfEqualsZero(x => x.Escolaridade, Mensagens.CAMPO_X0_INVALIDO.ToFormat("Escolaridade"));
            }
        }
    }
}
