using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaArte
{
    public class RelatorioCRUD
    {
        private Tela tela;
        private ObraCRUD obraCRUD;
        private LanceCRUD lanceCRUD;
        private VendaCRUD vendaCRUD;
        private int coluna = 4;
        private int linha = 4;

        public RelatorioCRUD(Tela tela, ObraCRUD obraCRUD, LanceCRUD lanceCRUD, VendaCRUD vendaCRUD)
        {
            this.tela = tela;
            this.obraCRUD = obraCRUD;
            this.lanceCRUD = lanceCRUD;
            this.vendaCRUD = vendaCRUD;
        }

        public void ExecutarCRUD()
        {
            string opcao;
            List<string> opcoes = new List<string>
            {
                " RELATÓRIOS ",
                "1 - Total de Lances por Obra",
                "2 - Índice de Obras Vendidas (Taxa de Sucesso)",
                "0 - Sair"
            };

            while (true)
            {
                opcao = tela.MostrarMenu(opcoes, coluna, linha);
                if (opcao == "0") break;

                if (opcao == "1")
                {
                    MostrarTotalLancesPorObra();
                }
                else if (opcao == "2")
                {
                    MostrarIndiceObrasVendidas();
                }
                else
                {
                    tela.MostrarMensagem("Opção inválida. Pressione uma tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        // RN-009 - Total de Lances por Obra
        private void MostrarTotalLancesPorObra()
        {
            this.tela.PrepararTela("Relatório: Total de Lances por Obra");
            this.tela.MostrarMensagem(1, 3, "ID Obra | Nome da Obra                 | # Lances");
            this.tela.MostrarMensagem(1, 4, "--------+-------------------------------+--------");

            var obras = obraCRUD.ListarObras();
            int linhaAtual = 5;

            // Construir lista de DTOs para facilitar testes e reutilização
            List<LancesPorObra> entries = new List<LancesPorObra>();
            foreach (var o in obras)
            {
                int totalLances = this.lanceCRUD.lances.Count(l => l.idObra == o.id);
                entries.Add(new LancesPorObra(o.id, o.nome, totalLances));
            }

            // Apresentar os DTOs
            foreach (var e in entries)
            {
                Console.SetCursorPosition(1, linhaAtual);
                Console.Write(e.IdObra);
                Console.SetCursorPosition(10, linhaAtual);
                Console.Write(e.NomeObra.PadRight(31));
                Console.SetCursorPosition(42, linhaAtual);
                Console.Write(e.TotalLances);
                linhaAtual++;
            }

            this.tela.MostrarMensagem("");
            this.tela.MostrarMensagem("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }

        // RN-009 - Índice de Obras Vendidas (Taxa de Sucesso)
        private void MostrarIndiceObrasVendidas()
        {
            this.tela.PrepararTela("Relatório: Índice de Obras Vendidas");

            var obras = obraCRUD.ListarObras();
            int totalObras = obras.Count;
            int vendidas = obras.Count(o => o.estado != null && o.estado.ToLower() == "vendida");

            double taxa = 0.0;
            if (totalObras > 0)
                taxa = (vendidas / (double)totalObras) * 100.0;

            // Usar DTO para facilitar testes e separação de responsabilidades
            TaxaSucesso dto = new TaxaSucesso(totalObras, vendidas, taxa);

            this.tela.MostrarMensagem(2, 4, $"Total de Obras: {dto.TotalObras}");
            this.tela.MostrarMensagem(2, 5, $"Obras Vendidas: {dto.ObrasVendidas}");
            this.tela.MostrarMensagem(2, 6, $"Taxa de Sucesso do Leilão: {dto.TaxaPercentual.ToString("F2")} %");

            this.tela.MostrarMensagem("");
            this.tela.MostrarMensagem("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }
    }
}
