using System;
using System.Collections.Generic;

namespace SistemaArte
{
    public class LanceCRUD
    {
        public List<Lance> lances;   // lista de lances
        public Lance lance;          // objeto lance atual
        public int posicao;          // posição no vetor
        private List<string> dados;
        private int coluna, linha, largura;
        private int larguraDados, colunaDados, linhaDados;
        private Tela tela;
        private ObraCRUD obraCRUD;
        private UsuarioCRUD usuarioCRUD;

        // Construtor recebendo as dependências
        public LanceCRUD(Tela tela, ObraCRUD obraCRUD, UsuarioCRUD usuarioCRUD)
        {
            this.lances = new List<Lance>();
            this.lance = new Lance();
            this.posicao = -1;

            this.dados = new List<string>();
            this.dados.Add("ID              : ");
            this.dados.Add("Código Comprador: ");
            this.dados.Add("Valor           : ");
            this.dados.Add("Data do Lance   : ");

            // Atribui as referências recebidas (sem criar novos objetos)
            this.tela = tela;
            this.obraCRUD = obraCRUD;
            this.usuarioCRUD = usuarioCRUD;

            this.coluna = 10;
            this.linha = 5;
            this.largura = 55;

            this.larguraDados = this.largura - dados[0].Length - 2;
            this.colunaDados = this.coluna + dados[0].Length + 1;
            this.linhaDados = this.linha + 2;

            // Exemplos de lances iniciais (pode remover se quiser começar vazio)
            this.lances.Add(new Lance(1, "U001", 1500, DateTime.Now.AddDays(-2)));
            this.lances.Add(new Lance(2, "U002", 1800, DateTime.Now.AddDays(-1)));
        }

        // Método principal
        public void ExecutarCRUD()
        {
            string opcao, resp;
            List<string> opcoesLance = new List<string>
            {
                " LANCES ",
                "1 - Registrar Lance ",
                "2 - Listar Lances   ",
                "0 - Sair            "
            };

            while (true)
            {
                opcao = tela.MostrarMenu(opcoesLance, coluna, linha);
                if (opcao == "0") break;

                if (opcao == "1")
                {
                    this.tela.MontarJanela("Registrar Lance", this.dados, this.coluna, this.linha, this.largura);
                    this.EntrarDados();

                    resp = this.tela.Perguntar("Confirma registro do lance (S/N): ");
                    if (resp.ToLower() == "s")
                    {
                        this.lances.Add(new Lance(this.lance.id, this.lance.cod, this.lance.valor, this.lance.dataLance));
                        this.tela.MostrarMensagem("Lance registrado com sucesso! Pressione uma tecla...");
                        Console.ReadKey();
                    }

                    this.tela.ApagarArea(this.coluna, this.linha, this.coluna + this.largura, this.linha + this.dados.Count + 2);
                }
                else if (opcao == "2")
                {
                    this.ListarLances();
                }
                else
                {
                    tela.MostrarMensagem("Opção inválida. Pressione uma tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        // Entrar dados de lance
        public void EntrarDados()
        {
            Console.SetCursorPosition(this.colunaDados, this.linhaDados);
            this.lance.id = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 1);
            this.lance.cod = Console.ReadLine();

            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 2);
            this.lance.valor = int.Parse(Console.ReadLine());

            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 3);
            this.lance.dataLance = DateTime.Parse(Console.ReadLine());
        }

        // Listar lances
        public void ListarLances()
        {
            this.tela.PrepararTela("Listagem de Lances");
            this.tela.MostrarMensagem(1, 3, "ID   | Código | Valor  | Data do Lance ");
            this.tela.MostrarMensagem(1, 4, "-----+--------+--------+---------------");

            int linhaAtual = 5;
            foreach (Lance l in this.lances)
            {
                Console.SetCursorPosition(1, linhaAtual);
                Console.Write(l.id);
                Console.SetCursorPosition(8, linhaAtual);
                Console.Write(l.cod);
                Console.SetCursorPosition(18, linhaAtual);
                Console.Write(l.valor);
                Console.SetCursorPosition(28, linhaAtual);
                Console.Write(l.dataLance.ToString("dd/MM/yyyy"));
                linhaAtual++;
            }

            this.tela.MostrarMensagem("");
            this.tela.MostrarMensagem("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }
    }
}
