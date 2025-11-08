using System;
using System.Collections.Generic;

namespace SistemaArte
{
    public class LanceCRUD
    {
        public List<Lance> lances;
        public Lance lance;
        public int posicao;
        private List<string> dados;
        private int coluna, linha, largura;
        private int larguraDados, colunaDados, linhaDados;
        private Tela tela;
        private ObraCRUD obraCRUD;
        private UsuarioCRUD usuarioCRUD;
        private AvaliarCRUD avaliarCRUD;

        public LanceCRUD(Tela tela, ObraCRUD obraCRUD, UsuarioCRUD usuarioCRUD, AvaliarCRUD avaliarCRUD)
        {
            this.lances = new List<Lance>();
            this.lance = new Lance();
            this.posicao = -1;

            this.dados = new List<string>
            {
                "ID da Obra        : ",
                "Código Comprador  : ",
                "Valor do Lance    : ",
                "Data do Lance     : "
            };

            this.tela = tela;
            this.obraCRUD = obraCRUD;
            this.usuarioCRUD = usuarioCRUD;
            this.avaliarCRUD = avaliarCRUD;

            this.coluna = 10;
            this.linha = 5;
            this.largura = 80;

            this.larguraDados = this.largura - dados[0].Length - 2;
            this.colunaDados = this.coluna + dados[0].Length + 1;
            this.linhaDados = this.linha + 2;
        }

        // MENU PRINCIPAL
        public void ExecutarCRUD()
        {
            string opcao, resp;
            List<string> opcoesLance = new List<string>
            {
                " LANCES ",
                "1 - Obras Disponíveis ",
                "2 - Registrar Lance ",
                "3 - Listar Lances   ",
                "0 - Sair            "
            };

            while (true)
            {
                opcao = tela.MostrarMenu(opcoesLance, coluna, linha);
                if (opcao == "0") break;

                if (opcao == "1")
                {
                    this.ListarObrasAvaliadas();

                }
                else if (opcao == "2")
                {
                    this.tela.MontarJanela("Registrar Lance", this.dados, this.coluna, this.linha, this.largura);
                    this.EntrarDados();

                    // Verifica comprador
                    Usuario usuario = usuarioCRUD.ObterUsuarioPorCodigo(this.lance.cod);
                    if (usuario == null)
                    {
                        tela.MostrarMensagem("Comprador não encontrado. Pressione uma tecla para voltar...");
                        Console.ReadKey();
                        continue;
                    }

                    if (usuario is Comprador comprador)
                    {
                        if (string.IsNullOrWhiteSpace(comprador.numeroCartao))
                        {
                            tela.MostrarMensagem("Comprador sem cartão cadastrado. Atualize os dados para continuar.");
                            Console.ReadKey();
                            continue;
                        }
                    }
                    else
                    {
                        tela.MostrarMensagem("Usuário informado não é um comprador. Lance não permitido.");
                        Console.ReadKey();
                        continue;
                    }

                    // Verifica preço de reserva
                    double precoReserva = avaliarCRUD.ObterPrecoReservaPorObra(this.lance.idObra);
                    double valorLance = Convert.ToDouble(this.lance.valor);

                    if (precoReserva > 0 && valorLance < precoReserva)
                    {
                        tela.MostrarMensagem($"Lance abaixo do preço de reserva (R$ {precoReserva:F2}). Ajuste o valor!");
                        Console.ReadKey();
                        continue;
                    }

                    resp = this.tela.Perguntar("Confirma registro do lance (S/N): ");
                    if (resp.ToLower() == "s")
                    {
                        this.lances.Add(new Lance(this.lances.Count + 1, this.lance.idObra, this.lance.cod, this.lance.valor, this.lance.dataLance));
                        this.tela.MostrarMensagem("Lance registrado com sucesso! Pressione uma tecla...");
                        Console.ReadKey();
                    }

                    this.tela.ApagarArea(this.coluna, this.linha, this.coluna + this.largura, this.linha + this.dados.Count + 2);
                }
                else if (opcao == "3")
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

        // Entrada de dados
        public void EntrarDados()
        {
            Console.SetCursorPosition(this.colunaDados, this.linhaDados);
            this.lance.idObra = Console.ReadLine();

            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 1);
            this.lance.cod = Console.ReadLine();

            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 2);
            this.lance.valor = Console.ReadLine();

            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 3);
            string data = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(data))
                this.lance.dataLance = DateTime.Now;
            else
                this.lance.dataLance = DateTime.Parse(data);
        }

        // ========================
        // LISTAR OBRAS AVALIADAS
        // ========================
        public void ListarObrasAvaliadas()
        {
            // Cabeçalho da janela
            List<string> cabecalho = new List<string>()
        {
         "ID".PadRight(6) + " " +
         "NOME".PadRight(30) +
         "AUTOR".PadRight(20) +
         "PREÇO (R$)"
         };

            // Prepara a moldura igual à do Registrar Lance
            this.tela.MontarMoldura(this.coluna, this.linha, this.coluna + this.largura, this.linha + 15);
            this.tela.Centralizar(this.coluna, this.coluna + this.largura, this.linha + 1, "Obras Disponíveis");

            // Linha de títulos
            int linhaAtual = this.linha + 3;
            Console.SetCursorPosition(this.coluna + 2, linhaAtual);
            Console.Write(cabecalho[0]);
            linhaAtual++;

            // Linha separadora
            Console.SetCursorPosition(this.coluna + 2, linhaAtual);
            Console.Write(new string('-', 70));
            linhaAtual++;

            // Dados das obras avaliadas
            foreach (Avaliar avaliacao in this.avaliarCRUD.avaliacoes)
            {
                if (avaliacao.obra == null) continue;

                Console.SetCursorPosition(this.coluna + 2, linhaAtual);
                Console.Write(avaliacao.obra.idObra.PadRight(6));

                Console.SetCursorPosition(this.coluna + 10, linhaAtual);
                Console.Write(avaliacao.obra.nome.PadRight(30));

                Console.SetCursorPosition(this.coluna + 42, linhaAtual);
                Console.Write(avaliacao.obra.autor.PadRight(20));

                Console.SetCursorPosition(this.coluna + 64, linhaAtual);
                Console.Write($"R$ {avaliacao.precoReserva:F2}");

                linhaAtual++;
            }

            // Mensagem final
            this.tela.MostrarMensagem("");
            this.tela.MostrarMensagem("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }

        // Listagem de Lances
        public void ListarLances()
        {
            this.tela.PrepararTela("Listagem de Lances");
            this.tela.MostrarMensagem(1, 3, "ID  | Obra  | Código | Valor  | Data do Lance ");
            this.tela.MostrarMensagem(1, 4, "----+-------+--------+--------+---------------");

            int linhaAtual = 5;
            foreach (Lance l in this.lances)
            {
                Console.SetCursorPosition(1, linhaAtual);
                Console.Write(l.id);
                Console.SetCursorPosition(6, linhaAtual);
                Console.Write(l.idObra);
                Console.SetCursorPosition(14, linhaAtual);
                Console.Write(l.cod);
                Console.SetCursorPosition(24, linhaAtual);
                Console.Write(l.valor);
                Console.SetCursorPosition(34, linhaAtual);
                Console.Write(l.dataLance.ToString("dd/MM/yyyy"));
                linhaAtual++;
            }

            this.tela.MostrarMensagem("");
            this.tela.MostrarMensagem("Pressione uma tecla para continuar...");
            Console.ReadKey();

        }

        // Obter o maior lance de uma obra
        public Lance ObterMaiorLance(string idObra)
        {
            Lance maior = null;
            double maiorValor = -1;

            foreach (var l in this.lances)
            {
                if (l.idObra == idObra)
                {
                    double valorAtual = Convert.ToDouble(l.valor);
                    if (valorAtual > maiorValor)
                    {
                        maiorValor = valorAtual;
                        maior = l;
                    }
                }
            }
            return maior;
        }
    }
}
