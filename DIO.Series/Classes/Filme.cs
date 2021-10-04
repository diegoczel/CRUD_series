using DIO.Series.Enumerador;
using System;

namespace DIO.Series.Classes
{
    public class Filme : EntidadeBase
    {
        public Filme(int id, Genero genero, string titulo, string descricao, int ano)
        {
            this.Id = id;
            this.Genero = genero;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Ano = ano;
            this.Excluido = false;
        }

        public override string ToString()
        {
            //return base.ToString();

            string retorno = "";
            retorno += $"Gênero: {this.Genero}{Environment.NewLine}";
            retorno += $"Titulo: {this.Titulo}{Environment.NewLine}";
            retorno += $"Descrição: {this.Descricao}{Environment.NewLine}";
            retorno += $"Ano de Inicio: {this.Ano}{Environment.NewLine}";
            retorno += $"Excluido: {this.Excluido}";
            return retorno;
        }

        public string GetTitulo() { return this.Titulo; }

        public int GetId() { return this.Id; }

        public bool GetExcluido() { return this.Excluido; }
        public void Excluir() { this.Excluido = true; }
    }
}