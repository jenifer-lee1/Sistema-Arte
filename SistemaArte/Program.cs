using System;
using System.Collections.Generic;

namespace SistemaArte
{
    class Program
    {
        static void Main()
        {

            Tela tela = new Tela();
            UsuarioCRUD usuarioCRUD = new UsuarioCRUD(tela);
            ObraCRUD obraCRUD = new ObraCRUD(tela);
            AvaliarCRUD avaliarCRUD = new AvaliarCRUD(tela, obraCRUD, usuarioCRUD);
            LanceCRUD lanceCRUD = new LanceCRUD(tela, obraCRUD, usuarioCRUD);
            VendaCRUD vendaCRUD = new VendaCRUD(tela, obraCRUD, avaliarCRUD, usuarioCRUD);
            RelatorioCRUD relatorioCRUD = new RelatorioCRUD(tela, obraCRUD, lanceCRUD, vendaCRUD);

            List<string> opcoes = new List<string>
            {
                "1 - Acessar área do Usuário",
                "2 - Cadastro de Obras",
                "3 - Avaliação de Obras",
                "4 - Registro de Lances",
                "5 - Pagamento",
                "6 - Relatórios",
                "0 - Sair do Sistema"
            };

            string opcao = "";

            while (true)
            {
                tela.PrepararTela("Sistema de Curadoria de Arte e Leilões Online");
                opcao = tela.MostrarMenu(opcoes, 2, 2);

                if (opcao == "0")
                    break;
                else if (opcao == "1") usuarioCRUD.ExecutarCRUD();
                else if (opcao == "2") obraCRUD.ExecutarCRUD();
                else if (opcao == "3") avaliarCRUD.ExecutarCRUD();
                else if (opcao == "4") lanceCRUD.ExecutarCRUD();
                else if (opcao == "5") vendaCRUD.ExecutarCRUD();
                else if (opcao == "6") relatorioCRUD.ExecutarCRUD();
                else
                {
                    tela.MostrarMensagem("Opção inválida. Pressione uma tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        // 🔹 Método auxiliar para simular uma ação
        static void ExecutarCRUD(string nome)
        {
            Console.Clear();
            Console.WriteLine($"Você escolheu: {nome}");
            Console.WriteLine("Funcionalidade em desenvolvimento...");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }
    }
}
