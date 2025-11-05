using System;
using System.Collections.Generic;

namespace SistemaArte
{
    public class Tela
    {
        //
        // Propriedades
        //
        private int largura;
        private int altura;
        private int colunaInicial;
        private int linhaInicial;
        private bool telaCheia;

        //
        // Construtores
        //

        // Construtor padrão (tela cheia)
        public Tela()
        {
            this.largura = 90;
            this.altura = 30;
            this.colunaInicial = 0;
            this.linhaInicial = 0;
            this.telaCheia = true;
        }

        // Construtor usado para telas menores
        public Tela(int coluna, int linha, int largura, int altura)
        {
            this.largura = largura;
            this.altura = altura;
            this.colunaInicial = coluna;
            this.linhaInicial = linha;
            this.telaCheia = false;
        }

        //
        // Métodos
        //

        public void PrepararTela(string titulo = "")
        {
            this.MontarMoldura(this.colunaInicial, this.linhaInicial, this.colunaInicial + this.largura, this.linhaInicial + this.altura);

            if (this.telaCheia)
            {
                this.MontarMoldura(this.colunaInicial, this.linhaInicial, this.colunaInicial + this.largura, this.linhaInicial + 2);
                this.MontarMoldura(this.colunaInicial, this.linhaInicial + this.altura - 2, this.colunaInicial + this.largura, this.linhaInicial + this.altura);
            }

            this.Centralizar(this.colunaInicial, this.colunaInicial + this.largura, this.linhaInicial + 1, titulo);
        }

        public string MostrarMenu(List<string> ops, int ci, int li)
        {
            int cf, lf, linha;
            cf = ci + ops[0].Length + 4;
            lf = li + ops.Count + 3;

            this.MontarMoldura(ci, li, cf, lf);

            linha = li + 1;
            for (int i = 0; i < ops.Count; i++)
            {
                Console.SetCursorPosition(ci + 1, linha);
                Console.Write(ops[i]);
                linha++;
            }

            Console.SetCursorPosition(ci + 1, linha);
            Console.Write("Opção : ");
            string op = Console.ReadLine();
            return op;
        }

        public void Centralizar(int ci, int cf, int lin, string msg)
        {
            int col = (cf - ci - msg.Length) / 2 + ci;
            Console.SetCursorPosition(col, lin);
            Console.Write(msg);
        }

        public void ApagarArea(int ci, int li, int cf, int lf)
        {
            for (int coluna = ci; coluna <= cf; coluna++)
            {
                for (int linha = li; linha <= lf; linha++)
                {
                    Console.SetCursorPosition(coluna, linha);
                    Console.Write(" ");
                }
            }
        }

        public void MontarMoldura(int ci, int li, int cf, int lf)
        {
            int col, lin;

            this.ApagarArea(ci, li, cf, lf);

            // Linhas horizontais
            for (col = ci; col < cf; col++)
            {
                Console.SetCursorPosition(col, li);
                Console.Write("═");

                Console.SetCursorPosition(col, lf);
                Console.Write("═");
            }

            // Linhas verticais
            for (lin = li; lin < lf; lin++)
            {
                Console.SetCursorPosition(ci, lin);
                Console.Write("║");

                Console.SetCursorPosition(cf, lin);
                Console.Write("║");
            }

            // Cantos
            Console.SetCursorPosition(ci, li);
            Console.Write("╔");

            Console.SetCursorPosition(ci, lf);
            Console.Write("╚");

            Console.SetCursorPosition(cf, li);
            Console.Write("╗");

            Console.SetCursorPosition(cf, lf);
            Console.Write("╝");
        }

        public void MontarJanela(string titulo, List<string> dados, int coluna, int linha, int largura)
        {
            this.MontarMoldura(coluna, linha, coluna + largura, linha + dados.Count + 2);
            this.Centralizar(coluna, coluna + largura, linha + 1, titulo);
            linha += 2;

            for (int i = 0; i < dados.Count; i++)
            {
                Console.SetCursorPosition(coluna + 1, linha);
                Console.Write(dados[i]);
                linha++;
            }
        }

        public void MostrarMensagem(string msg)
        {
            this.ApagarArea(this.colunaInicial + 1, this.linhaInicial + this.altura - 1,
                            this.colunaInicial + this.largura - 1, this.linhaInicial + this.altura - 1);

            int coluna = (this.largura - msg.Length) / 2;
            Console.SetCursorPosition(coluna, this.linhaInicial + this.altura - 1);
            Console.Write(msg);
        }

        public void MostrarMensagem(int coluna, int linha, string msg)
        {
            Console.SetCursorPosition(coluna, linha);
            Console.Write(msg);
        }

        public string Perguntar(string pergunta)
        {
            string resp = "";
            this.MostrarMensagem(pergunta);
            resp = Console.ReadLine();
            return resp;
        }
    }
}
