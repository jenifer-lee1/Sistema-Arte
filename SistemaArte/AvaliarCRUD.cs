using SistemaArte;
using System;
using System.Collections.Generic;

public class AvaliarCRUD
{
    public List<Avaliar> avaliacoes;
    public Avaliar avaliacaoAtual;
    public int posicao;
    private List<string> dados;
    private int coluna, linha, largura;
    private int larguraDados, colunaDados, linhaDados;
    private Tela tela;
    private ObraCRUD obraCRUD;

    public AvaliarCRUD(Tela tela, ObraCRUD obraCRUD)
    {
        this.avaliacoes = new List<Avaliar>();
        this.avaliacaoAtual = new Avaliar();
        this.posicao = -1;
        this.dados = new List<string>();

        this.dados.Add("Id                                              : ");
        this.dados.Add("Nome da Obra                                    : ");
        this.dados.Add("Autor                                          : ");
        this.dados.Add("Ano de criação                                   : ");
        this.dados.Add("Número do Certificado de Autenticidade           : ");
        this.dados.Add("Estado da Obra (Original/Restaurada/Réplica)     : ");

        this.dados.Add("ID do Curador                                   : ");
        this.dados.Add("Preço de Reserva                                 : ");
        this.dados.Add("Observações                                      : ");

        this.tela = tela;
        this.obraCRUD = obraCRUD;

        this.coluna = 8;
        this.linha = 8;
        this.largura = 90;

        this.larguraDados = this.largura - this.dados[0].Length - 2;
        this.colunaDados = this.coluna + this.dados[0].Length + 1;
        this.linhaDados = this.linha + 2;
    }

    public void ExecutarCRUD()
    {
        string opcao, resp;
        List<string> opcoesAv = new List<string>()
        {
            " AVALIAÇÃO DE OBRAS ",
            "1 - Avaliar Obra",
            "2 - Visualizar Preço",
            "3 - Listar Avaliações",
            "0 - Sair"
        };

        while (true)
        {
            opcao = tela.MostrarMenu(opcoesAv, coluna, linha);
            if (opcao == "0") break;

            else if (opcao == "1")
            {
                this.coluna += 10;
                this.linha += 2;
                this.colunaDados += 10;
                this.linhaDados += 2;

                string titulo = "Registrar Avaliação";
                this.tela.MontarJanela(titulo, this.dados, this.coluna, this.linha, this.largura);

                Console.SetCursorPosition(this.colunaDados, this.linhaDados);
                string idObra = Console.ReadLine();

                Obra obraEncontrada = this.obraCRUD.BuscarPorId(idObra);

                if (obraEncontrada == null)
                {
                    this.tela.MostrarMensagem("Obra não encontrada. Pressione uma tecla para continuar...");
                    Console.ReadKey();
                }
                else
                {
                    MostrarObra(obraEncontrada);
                    this.avaliacaoAtual.obra = obraEncontrada;

                    bool achou = this.ProcurarCodigo();

                    if (achou)
                    {
                        this.MostrarDados();
                        this.tela.MostrarMensagem("Obra já avaliada. Pressione uma tecla para continuar...");
                        Console.ReadKey();
                    }
                    else
                    {
                        bool dadosValidos = this.EntrarDados();
                        if (!dadosValidos) break;

                        resp = this.tela.Perguntar("Confirma Avaliação (S/N) : ");
                        if (resp.ToLower() == "s")
                        {
                            this.avaliacoes.Add(new Avaliar(
                                this.avaliacaoAtual.obra,
                                this.avaliacaoAtual.idCurador,
                                this.avaliacaoAtual.precoReserva,
                                this.avaliacaoAtual.observacoes,
                                this.avaliacaoAtual.dataAvaliacao
                            ));
                            this.tela.MostrarMensagem("Avaliação registrada com sucesso! Pressione uma tecla para continuar...");
                            Console.ReadKey();
                        }
                    }
                }

                this.tela.ApagarArea(this.coluna, this.linha, this.coluna + this.largura, this.linha + this.dados.Count + 2);
                this.coluna -= 10;
                this.linha -= 2;
                this.colunaDados -= 10;
                this.linhaDados -= 2;
            }
            else if (opcao == "2")
            {
                this.coluna += 10;
                this.linha += 2;
                this.colunaDados += 10;
                this.linhaDados += 2;

                List<string> camposPreco = new List<string>()
    {
        "ID da Obra   : ",
        "Nome da Obra : ",
        "Autor        : ",
        "Preço        : "
    };

                string titulo = "Visualizar Preço";
                this.tela.MontarJanela(titulo, camposPreco, this.coluna, this.linha, this.largura);

                // Calcula a posição do cursor logo após os ":"
                int colunaInput = this.coluna + camposPreco[0].Length + 1;
                int linhaInput = this.linha + 2;

                Console.SetCursorPosition(colunaInput, linhaInput);
                string idObra = Console.ReadLine();

                Obra obraEncontrada = this.obraCRUD.BuscarPorId(idObra);

                if (obraEncontrada == null)
                {
                    this.tela.MostrarMensagem("Obra não encontrada. Pressione uma tecla para continuar...");
                    Console.ReadKey();
                }
                else
                {
                    // Mostra os dados alinhados logo após os ":"
                    this.tela.MostrarMensagem(colunaInput, linhaInput + 0, obraEncontrada.idObra);
                    this.tela.MostrarMensagem(colunaInput, linhaInput + 1, obraEncontrada.nome);
                    this.tela.MostrarMensagem(colunaInput, linhaInput + 2, obraEncontrada.autor);

                    Avaliar avaliacao = this.avaliacoes.Find(a =>
                        (a.obra != null && a.obra.idObra == idObra) || a.idObra == idObra);

                    string preco = (avaliacao != null)
                        ? avaliacao.precoReserva.ToString("F2")
                        : "(não avaliada)";

                    this.tela.MostrarMensagem(colunaInput, linhaInput + 3, preco);

                    this.tela.MostrarMensagem("Obra encontrada com sucesso! Pressione uma tecla para continuar...");
                    Console.ReadKey();
                }

                this.tela.ApagarArea(this.coluna, this.linha, this.coluna + this.largura, this.linha + camposPreco.Count + 3);

                this.coluna -= 10;
                this.linha -= 2;
                this.colunaDados -= 10;
                this.linhaDados -= 2;
            }


            else if (opcao == "3")
            {
                ListarAvaliacoes();
            }

            else
            {
                tela.MostrarMensagem("Opção inválida. Pressione uma tecla para continuar...");
                Console.ReadKey();
            }
        }
    }


    private bool EntrarDados()
    {
        Console.SetCursorPosition(this.colunaDados, this.linhaDados + 6);
        this.avaliacaoAtual.idCurador = Console.ReadLine();

        Console.SetCursorPosition(this.colunaDados, this.linhaDados + 7);
        if (!double.TryParse(Console.ReadLine(), out double preco))
        {
            this.tela.MostrarMensagem("Valor inválido! Pressione uma tecla para continuar...");
            Console.ReadKey();
            return false;
        }
        this.avaliacaoAtual.precoReserva = preco;

        Console.SetCursorPosition(this.colunaDados, this.linhaDados + 8);
        this.avaliacaoAtual.observacoes = Console.ReadLine();

        this.avaliacaoAtual.dataAvaliacao = DateTime.Now;
        return true;
    }

    public void MostrarObra(Obra obra)
    {
        this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 0, obra.idObra);
        this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 1, obra.nome);
        this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 2, obra.autor);
        this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 3, obra.ano);
        this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 4, obra.numero);
        this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 5, obra.estado);
    }

    public bool ProcurarCodigo()
    {
        for (int i = 0; i < this.avaliacoes.Count; i++)
        {
            if (this.avaliacoes[i].obra.idObra == this.avaliacaoAtual.obra.idObra)
            {
                this.posicao = i;
                return true;
            }
        }
        return false;
    }

    public void ListarAvaliacoes()
    {

        {
            this.tela.PrepararTela("Listagem de Avaliações");
            this.tela.MostrarMensagem(1, 3, " ID        |  Nome da Obra     | Autor      | Curador       |  Preço     | Observações | Data ");
            this.tela.MostrarMensagem(1, 4, "-----------+------------------+------------------+---------------+----------------+----------------");

            int linha = 5;
            foreach (var a in this.avaliacoes)
            {
                Obra? obra = obraCRUD.BuscarPorId(a.idObra);

                string nomeObra = obra?.nome ?? "(não encontrada)";
                string autor = obra?.autor ?? "(desconhecido)";

                Console.SetCursorPosition(1, linha);
                Console.Write(a.idObra.PadRight(5));

                Console.SetCursorPosition(7, linha);
                Console.Write(nomeObra.PadRight(24));

                Console.SetCursorPosition(33, linha);
                Console.Write(autor.PadRight(18));

                Console.SetCursorPosition(53, linha);
                Console.Write(a.idCurador.PadRight(10));

                Console.SetCursorPosition(68, linha);
                Console.Write(a.precoReserva.ToString("F2").PadLeft(12));

                Console.SetCursorPosition(82, linha);
                Console.Write(a.observacoes.PadRight(27));

                Console.SetCursorPosition(106, linha);
                Console.Write(a.dataAvaliacao.ToString("dd/MM/yyyy"));

                linha++;
            }
        }

        this.tela.MostrarMensagem("");
        this.tela.MostrarMensagem("Pressione uma tecla para continuar...");
        Console.ReadKey();
    }

    public double ObterPrecoReservaPorObra(string idObra)
    {
        foreach (var av in this.avaliacoes)
        {
            if (av.obra != null && av.obra.idObra == idObra)
            {
                return av.precoReserva;
            }
        }
        return 0;
    }


    public void MostrarDados()
    {
        Obra obra = this.avaliacoes[this.posicao].obra;
        if (obra != null)
            MostrarObra(obra);

        this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 6, this.avaliacoes[this.posicao].idCurador);
        this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 7, this.avaliacoes[this.posicao].precoReserva.ToString());
        this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 8, this.avaliacoes[this.posicao].observacoes);
    }
}