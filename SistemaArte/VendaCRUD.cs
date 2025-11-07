using System;
using System.Collections.Generic;

namespace SistemaArte
{
    public class VendaCRUD
    {
        public List<Venda> vendas;
        public Venda venda;
        private int posicao;
        private List<string> dados;
        private int coluna, linha, largura;
        private int larguraDados, colunaDados, linhaDados;

        private Tela tela;
        private ObraCRUD obraCRUD;
        private AvaliarCRUD avaliarCRUD;
        private UsuarioCRUD usuarioCRUD;
        private LanceCRUD lanceCRUD;

        public VendaCRUD(Tela tela, ObraCRUD obraCRUD, AvaliarCRUD avaliarCRUD, UsuarioCRUD usuarioCRUD, LanceCRUD lanceCRUD)
        {
            this.vendas = new List<Venda>();
            this.venda = new Venda();
            this.posicao = -1;

            this.dados = new List<string>();
            this.dados.Add("ID da Venda : ");
            this.dados.Add("ID da Obra  : ");

            this.tela = tela;
            this.obraCRUD = obraCRUD;
            this.avaliarCRUD = avaliarCRUD;
            this.usuarioCRUD = usuarioCRUD;
            this.lanceCRUD = lanceCRUD;

            this.coluna = 8;
            this.linha = 6;
            this.largura = 60;

            this.larguraDados = this.largura - dados[0].Length - 2;
            this.colunaDados = this.coluna + dados[0].Length + 1;
            this.linhaDados = this.linha + 2;
        }

        public void ExecutarCRUD()
        {
            string opcao;
            List<string> opcoes = new List<string>
            {
                " FECHAMENTO DE VENDAS ",
                "1 - Fechar Venda ",
                "2 - Listar Vendas ",
                "0 - Sair "
            };

            while (true)
            {
                opcao = tela.MostrarMenu(opcoes, coluna, linha);
                if (opcao == "0") break;

                if (opcao == "1")
                {
                    this.tela.MontarJanela("Fechamento de Venda", this.dados, this.coluna, this.linha, this.largura);
                    bool ok = this.EntrarDados();
                    if (!ok)
                    {
                        this.tela.MostrarMensagem("Operação cancelada. Pressione uma tecla para continuar...");
                        Console.ReadKey();
                        continue;
                    }

                    // Busca preço de reserva
                    double precoReserva = -1;
                    foreach (var a in this.avaliarCRUD.avaliacoes)
                    {
                        if (a.idObra == this.venda.idObra)
                        {
                            precoReserva = a.precoReserva;
                            break;
                        }
                    }

                    if (precoReserva < 0)
                    {
                        this.tela.MostrarMensagem("Obra sem avaliação/preço de reserva. Impossível fechar venda.");
                        Console.ReadKey();
                        continue;
                    }

                    // Busca maior lance
                    Lance maiorLance = this.lanceCRUD.ObterMaiorLance(this.venda.idObra);

                    if (maiorLance == null)
                    {
                        this.tela.MostrarMensagem("Nenhum lance encontrado para essa obra. Pressione uma tecla...");
                        Console.ReadKey();
                        continue;
                    }

                    this.venda.compradorCodigo = maiorLance.cod;
                    this.venda.valorFinal = Convert.ToDouble(maiorLance.valor);

                    if (this.venda.valorFinal >= precoReserva)
                    {
                        // Marca obra como vendida
                        foreach (var o in this.obraCRUD.ListarObras())
                        {
                            if (o.idObra == this.venda.idObra)
                            {
                                o.estado = "Vendida";
                                break;
                            }
                        }

                        // Registra venda
                        this.venda.dataVenda = DateTime.Now;
                        this.vendas.Add(new Venda(this.venda.idObra, this.venda.compradorCodigo, this.venda.valorFinal, this.venda.dataVenda));

                        this.tela.MostrarMensagem("Venda fechada com sucesso! Obra marcada como 'Vendida'.");
                        Console.ReadKey();
                    }
                    else
                    {
                        this.tela.MostrarMensagem("Lance vencedor menor que o preço de reserva. Venda não realizada.");
                        Console.ReadKey();
                    }

                    this.tela.ApagarArea(this.coluna, this.linha, this.coluna + this.largura, this.linha + this.dados.Count + 4);
                }
                else if (opcao == "2")
                {
                    ListarVendas();
                }
                else
                {
                    tela.MostrarMensagem("Opção inválida. Pressione uma tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        public bool EntrarDados()
        {
            try
            {
                Console.SetCursorPosition(this.colunaDados, this.linhaDados);
                int.TryParse(Console.ReadLine(), out this.venda.id);

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + 1);
                this.venda.idObra = Console.ReadLine() ?? "";

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ListarVendas()
        {
            this.tela.PrepararTela("Listagem de Vendas");
            this.tela.MostrarMensagem(1, 3, "ID | Obra | Comprador | Valor | Data");
            this.tela.MostrarMensagem(1, 4, "---+------+-----------+-------+----------------");

            int linha = 5;
            foreach (var v in this.vendas)
            {
                Console.SetCursorPosition(1, linha);
                Console.Write(v.id);
                Console.SetCursorPosition(6, linha);
                Console.Write(v.idObra);
                Console.SetCursorPosition(14, linha);
                Console.Write(v.compradorCodigo);
                Console.SetCursorPosition(27, linha);
                Console.Write(v.valorFinal);
                Console.SetCursorPosition(36, linha);
                Console.Write(v.dataVenda.ToString("dd/MM/yyyy"));
                linha++;
            }

            this.tela.MostrarMensagem("\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }
    }
}
