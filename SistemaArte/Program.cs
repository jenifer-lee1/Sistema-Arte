using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> opcoes = new List<string>
        {
            "🧑‍🎨 Acessar área do Usuário",
            "🎨 Cadastro de Obras",
            "🖼️ Avaliar Obra",
            "💰 Registrar Lance",
            "📦 Relatório",
            "🚪 Pagamento"
        };

        Tela tela = new Tela("Sistema de Curadoria de Arte e Leilões Online", opcoes);
        tela.Mostrar(); // mostra arte + menu

        // Captura a escolha do usuário
        Console.Write("\nDigite a opção desejada: ");
        string escolha = Console.ReadLine();

        if (int.TryParse(escolha, out int numero) && numero > 0 && numero <= opcoes.Count)
        {
            Console.WriteLine($"\nVocê escolheu: {opcoes[numero - 1]}");
        }
        else
        {
            Console.WriteLine("\nOpção inválida!");
        }

        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}
