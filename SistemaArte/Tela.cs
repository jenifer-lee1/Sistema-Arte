using System;
using System.Collections.Generic;
using System.Threading;

public class Tela
{
    private string titulo;
    private List<string> opcoes;

    public Tela(string titulo, List<string> opcoes)
    {
        this.titulo = titulo;
        this.opcoes = opcoes;
    }

    public void Mostrar()
    {
        Console.Title = titulo.ToUpper();
        Console.CursorVisible = false;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Clear();

        EfeitoConfeteRapido();         // efeito confete
        MostrarArteCentralizada();     // exibe a arte
        Thread.Sleep(4000);            // mantém a arte por 4 segundos
        Console.Clear();

        MostrarMenu();                 // mostra o menu com moldura
    }

    private void EfeitoConfeteRapido()
    {
        Random random = new Random();
        int largura = Console.WindowWidth;
        int altura = Console.WindowHeight;
        char[] simbolos = { '*', '+', '.', 'o', '@', '#' };

        DateTime inicio = DateTime.Now;
        TimeSpan duracao = TimeSpan.FromMilliseconds(2000);
        while (DateTime.Now - inicio < duracao)
        {
            int x = random.Next(0, largura);
            int y = random.Next(0, altura);
            ConsoleColor cor = (ConsoleColor)random.Next(1, 16);
            char simbolo = simbolos[random.Next(simbolos.Length)];

            Console.ForegroundColor = cor;
            Console.SetCursorPosition(x, y);
            Console.Write(simbolo);

            Thread.Sleep(4);
        }

        Console.Clear();
    }

    private void MostrarArteCentralizada()
    {
        string arte = @"
::────────────────────────────────────────────────────────────────────────────::
::                            BEM VINDOS AO                                   ::
::              SISTEMA DE CURADORIA DE ARTE E LEILÕES ONLINE                 ::
::                                                                            ::
::                                                                            ::
::              []                                       []                   ::
::              []                                       []                   ::
::             .[]:.                                 ,: :[]:.                 ::
::           .: []: :-.                           ,-: : :[]: :.               ::
::         .: : []: : :`._                   _.,': : : : []: : :.             ::
::       .: : : []: : : : :-._           _.-: : : : : : :[]: : : :.           ::
::___..: : : :  []: : : : : : :-._____.-: : : : : : : : :[]: : : : :-.________::
::!_!!_!_!_!_!_!_![]!_!_!_!_!_!_!_!_!_!_!_!_!_!_!_!_!_!_!_![]!_!_!_!_!_!_!_!!_! ::
::!!!!!!!!!!!!! []!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!![]!!!!!!!!!!!!!!!!!!!::
::^^^^^^^^^^^^^^[]^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^[]^^^^^^^^^^^^^^^^^^^::
::              []                                       []                   ::
::              []                                       []                   ::
::              []                                       []                   ::
::              []                                       []                   ::
:: ~ ~- ^ ~ ~- /  \~^-~^~_~^-~_^~-^~_^~~-^~_~^~-~_~-^~_^/  \~^-~_~^- ~ ~- ^ ~ ::
::~ _~~-~ _~~- ~^-^~-^~~- ^~_^-^~~_ -~^_ -~_-~~^- _~~_~-^_ ~^-^~~-_^-~ ~^~ _~~::
:: ~ ^-  ~ ^- ~ ^- _~~_-  ~~ _ ~  ^~  - ~~^ _ -  ^~-  ~ _  ~~^  - ~_   - ~^_~ ::
::  ~-  ^_ ~-  ^_  ~^ -  ^~ _ - ~^~ _   _~^~-  _ ~~^ - _ ~ - _ ~~^ -  ~-  ^_  ::
::  ~^ -_~^ -_ ~^^ -_ ~ _ - _ ~^~-  _~ -_   ~- _ ~^ _ -  ~ ^-  ~^ -_  ~^ -_   ::
:: ~^~ - ~^~ - ~^~ - ~^~ - ~^~ ~^~ - _ ^ - ~~~ _ - _ ~-^ ~ __- ~_ - ~  ~^_- ~^::
::  ~ ~- ^ ~ ~- ^ ~ ~- ^ ~ ~- ^ ~ ~- ^ ~ ~- ^ ~ ~- ^ ~ ~- ^ ~ ~- ^ ~ ~- ^ ~ ~-::
::────────────────────────────────────────────────────────────────────────────::
";

        int larguraConsole = Console.WindowWidth;
        int alturaConsole = Console.WindowHeight;
        string[] linhas = arte.Split('\n');
        int inicioVertical = Math.Max((alturaConsole - linhas.Length) / 2, 0);

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        for (int i = 0; i < linhas.Length; i++)
        {
            string linha = linhas[i];
            int posHorizontal = Math.Max((larguraConsole - linha.Length) / 2, 0);
            Console.SetCursorPosition(posHorizontal, inicioVertical + i);
            Console.WriteLine(linha);
        }
    }

    private void MostrarMenu()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();

        string[] linhas =
        {
        "┌─────────────────────────────────────────────┐",
        "│            Sistema de Leilão                │",
        "├─────────────────────────────────────────────┤",
        "│                                             │",
        "│  Digite qual deseja:                        │",
        "│ ┌─────────────────────────────────────────┐ │"
    };

        // exibe as linhas iniciais centralizadas
        foreach (string linha in linhas)
        {
            int posX = (Console.WindowWidth - linha.Length) / 2;
            Console.SetCursorPosition(posX, Console.CursorTop);
            Console.WriteLine(linha);
        }

        // exibe opções numeradas centralizadas
        for (int i = 0; i < opcoes.Count; i++)
        {
            string linhaOpcao = $"│ │ {i + 1} - {opcoes[i],-35} │ │";
            int posX = (Console.WindowWidth - linhaOpcao.Length) / 2;
            Console.SetCursorPosition(posX, Console.CursorTop);
            Console.WriteLine(linhaOpcao);
        }

        // linha de digitar opção
        string linhaDigite = "│ │ Digite:                                 | │";
        int posXD = (Console.WindowWidth - linhaDigite.Length) / 2;
        Console.SetCursorPosition(posXD, Console.CursorTop);
        Console.WriteLine(linhaDigite);

        // linhas abaixo
        string[] linhasObras =
        {
        "│ └─────────────────────────────────────────┘ │",
        "│                                             │",
        "│  Obras em Destaque (Disponíveis para Lance):│",
        "│ ┌──────────────────────────────────────────┐│",
        "│ │v Obra: \"A Tempestade\" | Artista: X.X.X.││ ",
        "│ │Lance Atual: R$ 5.000,00 | [Dar Lance]    ││",
        "│ │                                          ││",
        "│ │v Obra: \"Quadro Antigo\" | Artista: Y.Y. |│",
        "│ │Lance Atual: R$ 12.500,00 | [Dar Lance]   ││",
        "│ │                                          ││",
        "│ │x Obra: \"Escultura Rara\" | Artista: Z.Z |│",
        "│ │Status: Vendida | Preço Final: R$ 20.000  ││",
        "│ └──────────────────────────────────────────┘│",
    };

        foreach (string linha in linhasObras)
        {
            int posX = (Console.WindowWidth - linha.Length) / 2;
            Console.SetCursorPosition(posX, Console.CursorTop);
            Console.WriteLine(linha);
        }
    }
}
