﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SistemaBiblioteca.App.Mvc
{
    /// <summary>
    /// Representa um feedback exibido pelo sistema
    /// </summary>
    public class Feedback
    {
        /// <summary>
        /// Tipo do feedback
        /// </summary>
        [JsonProperty("tipo")]
        public TipoFeedback tipo { get; }

        /// <summary>
        /// Descrição do tipo do feedback
        /// </summary>
        [JsonProperty("tipoDescricao")]
        public string tipoDescricao => this.tipo.ObterDescricao();

        /// <summary>
        /// Mensagem do feedback
        /// </summary>
        [JsonProperty("mensagem")]
        public string mensagem { get; }

        /// <summary>
        /// Mensagem adicional do feedback
        /// </summary>
        [JsonProperty("mensagemAdicional")]
        public string mensagemAdicional { get; set; }

        /// <summary>
        /// Tipo da ação que deverá ser executada após o feedback ser apresentado
        /// </summary>
        [JsonProperty("tipoAcao")]
        public TipoAcaoAoOcultarFeedback tipoAcao { get; set; }

        public Feedback(
            TipoFeedback Tipo,
            string Mensagem,
            IEnumerable<string> MensagensAdicionais = null,
            TipoAcaoAoOcultarFeedback TipoAcao = TipoAcaoAoOcultarFeedback.Ocultar)
        {
            //Tipo = tipo;
            //Mensagem = mensagem;
            //MensagemAdicional = mensagensAdicionais != null && mensagensAdicionais.Any()
            //    ? string.Join(string.Empty, mensagensAdicionais.Where(x => !string.IsNullOrEmpty(x)).Select(x => "<li style=\"margin: 1em 0;\">" + x + "</li>"))
            //    : string.Empty;
            //TipoAcao = tipoAcao;
            tipo = Tipo;
            mensagem = Mensagem;
            mensagemAdicional = MensagensAdicionais != null && MensagensAdicionais.Any()
                ? string.Join(string.Empty, MensagensAdicionais.Where(x => !string.IsNullOrEmpty(x)).Select(x => "<li style=\"margin: 1em 0;\">" + x + "</li>"))
                : string.Empty;
            tipoAcao = TipoAcao;
        }
    }

    /// <summary>
    /// Tipo de feedback
    /// </summary>
    public enum TipoFeedback
    {
        [Description("INFO")]
        Info = 1,
        [Description("ATENCAO")]
        Atencao = 2,
        [Description("ERRO")]
        Erro = 3,
        [Description("SUCESSO")]
        Sucesso = 4
    }

    /// <summary>
    /// Tipo de ação que deverá ser executada ao ocultar o feedback
    /// </summary>
    public enum TipoAcaoAoOcultarFeedback
    {
        Ocultar = 0,
        /// <summary>
        /// Redireciona para a página anteriormente exibida antes da exibição da mensagem de erro
        /// </summary>
        VoltarPaginaAnterior = 1,
        /// <summary>
        /// Fecha a janela no caso de uma janela popup
        /// </summary>
        FecharJanela = 2,
        /// <summary>
        /// Redireciona para a página inicial
        /// </summary>
        RedirecionarTelaInicial = 3,
        /// <summary>
        /// Executa um reload na página (location.reload)
        /// </summary>
        ReloadPagina = 4,
        /// <summary>
        /// Oculta os moldais abertos
        /// </summary>
        OcultarMoldais = 5,
        /// <summary>
        /// Redireciona para tela de login
        /// </summary>
        RedirecionarTelaLogin = 6
    }

    public static partial class ExtensionMethods
    {
        public static string ObterDescricao(this Enum member)
        {
            if (member == null)
                return null;

            var fieldInfo = member.GetType().GetField(member.ToString());

            if (fieldInfo == null)
                return null;

            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).ToList();

            return attributes.Count > 0 ? ((DescriptionAttribute)attributes[0]).Description : member.ToString();
        }
    }
}
