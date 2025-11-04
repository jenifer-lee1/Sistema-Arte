using System;
using System.Collections.Generic;
using System.Threading;

public class Tela
{
    //
    // propriedades
    //
    private int largura;
    private int altura;
    private int colunaInicial;
    private int linhaInicial;
    private bool telaCheia;

    //
    // métodos
    //

    // Construtor usado para "full screen"
    public Tela()
    {
        this.largura = 80;
        this.altura = 25;
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

    // Prepara a tela principal com moldura e título
    public void PrepararTela(string titulo = "")
    {
        this.MontarMoldura(this.colunaInicial, this.linhaInicial,
            this.colunaInicial + this.largura, this.linhaInicial + this.altura);

        if (this.telaCheia)
        {
            this.MontarMoldura(this.colunaInicial, this.linhaInicial,
                this.colunaInicial + this.largura, this.linhaInicial + 2);

            this.MontarMoldura(this.colunaInicial, this.linhaInicial + this.altura - 2,
                this.colunaInicial + this.largura, this.linhaInicial + this.altura);
        }

        this.Centralizar(this.colunaInicial, this.colunaInicial + this.largura,
            this.linhaInicial + 1, titulo);
    }

    // Mostra o menu com moldura e lê a opção do usuário
    public string MostrarMenu(List<string> ops, int ci, int li)
    {
        int cf, lf, linha;
        cf = ci + ops[0].Length + 20;
        lf = li + ops.Count + 3;

        this.MontarMoldura(ci, li, cf, lf);

        linha = li + 1;
        for (int i = 0; i < ops.Count; i++)
        {
            Console.SetCursorPosition(ci + 2, linha);
            Console.Write(ops[i]);
            linha++;
        }

        Console.SetCursorPosition(ci + 2, linha);
        Console.Write("Digite a opção: ");
        string op = Console.ReadLine();
        return op;
    }

    // Centraliza texto horizontalmente dentro de uma moldura
    public void Centralizar(int ci, int cf, int lin, string msg)
    {
        int col = (cf - ci - msg.Length) / 2 + ci;
        Console.SetCursorPosition(col, lin);
        Console.Write(msg);
    }

    // Apaga uma área específica do console
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

    // Monta uma moldura de linhas duplas
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
        Console.SetCursorPosition(ci, li); Console.Write("╔");
        Console.SetCursorPosition(cf, li); Console.Write("╗");
        Console.SetCursorPosition(ci, lf); Console.Write("╚");
        Console.SetCursorPosition(cf, lf); Console.Write("╝");
    }

    // Monta uma janela com título e dados
    public void MontarJanela(string titulo, List<string> dados, int coluna, int linha, int largura)
    {
        this.MontarMoldura(coluna, linha, coluna + largura, linha + dados.Count + 3);
        this.Centralizar(coluna, coluna + largura, linha + 1, titulo);
        linha += 2;
        for (int i = 0; i < dados.Count; i++)
        {
            Console.SetCursorPosition(coluna + 2, linha);
            Console.Write(dados[i]);
            linha++;
        }
    }

    // Mostra mensagem na parte inferior da tela
    public void MostrarMensagem(string msg)
    {
        this.ApagarArea(this.colunaInicial + 1,
            this.linhaInicial + this.altura - 1,
            this.colunaInicial + this.largura - 1,
            this.linhaInicial + this.altura - 1);

        int coluna = (this.largura - msg.Length) / 2;
        Console.SetCursorPosition(coluna, this.linhaInicial + this.altura - 1);
        Console.Write(msg);
    }

    // Mostra mensagem em uma posição específica
    public void MostrarMensagem(int coluna, int linha, string msg)
    {
        Console.SetCursorPosition(coluna, linha);
        Console.Write(msg);
    }

    // Pergunta algo ao usuário e retorna a resposta
    public string Perguntar(string pergunta)
    {
        this.MostrarMensagem(pergunta);
        string resp = Console.ReadLine();
        return resp;
    }

    // 🔹 Tela inicial simplificada (sem obras em destaque)
    public string MostrarTelaInicial(List<string> opcoes, int ci, int li)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();

        // Moldura principal
        this.PrepararTela("SISTEMA DE CURADORIA DE ARTE E LEILÕES ONLINE");

        // Moldura do menu
        this.MontarMoldura(ci, li, ci + 35, li + opcoes.Count + 5);
        Console.SetCursorPosition(ci + 2, li + 1);
        Console.Write("Digite qual deseja:");

        int linha = li + 2;
        for (int i = 0; i < opcoes.Count; i++)
        {
            Console.SetCursorPosition(ci + 3, linha);
            Console.Write(opcoes[i]);
            linha++;
        }

        Console.SetCursorPosition(ci + 3, linha);
        Console.Write("Digite: ");
        string op = Console.ReadLine();

        return op;
    }
}
