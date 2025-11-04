using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Tela tela = new Tela();
        UsuarioCRUD usuarioCRUD = new UsuarioCRUD(tela);
        ObraCRUD obraCRUD = new ObraCRUD(tela);
        LanceCRUD lanceCRUD = new LanceCRUD(tela, obraCRUD, usuarioCRUD);
        VendaCRUD vendaCRUD = new VendaCRUD(tela, obraCRUD, lanceCRUD);
        RelatorioCRUD relatorioCRUD = new RelatorioCRUD(tela, obraCRUD, usuarioCRUD, lanceCRUD, vendaCRUD);

        string opcao;
        List<string> opcoes = new List<string>();
        opcoes.Add("1 - Acessar área do Usuário");
        opcoes.Add("2 - Cadastro de Obras de Arte");
        opcoes.Add("3 - Avaliação de Obras");
        opcoes.Add("4 - Registro de Lances");
        opcoes.Add("5 - Pagamento");
        opcoes.Add("6 - Relatórios");
        opcoes.Add("0 - Sair do Sistema");

        while (true)
        {
            tela.PrepararTela("Sistema de Curadoria de Arte e Leilões Online");
            opcao = tela.MostrarTelaInicial(opcoes, 2, 2);

            if (opcao == "0") break;
            else if (opcao == "1") usuarioCRUD.ExecutarCRUD();
            else if (opcao == "2") obraCRUD.ExecutarCRUD();
            else if (opcao == "3") obraCRUD.AvaliarObra();
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
}
