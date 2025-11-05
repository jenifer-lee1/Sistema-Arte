using SistemaArte;
using System;
using System.Collections.Generic;

public class AvaliarCRUD
{
    public List<Avaliar> avaliacao;
    public Avaliar avaliar;
    public int posicao;
    private List<string> dados;
    private int coluna, linha, largura;
    private int larguraDados, colunaDados, linhaDados;
    private Tela tela;
    private ObraCRUD obraCRUD;
    private UsuarioCRUD usuarioCRUD;

    // Construtor para inicializar dependências e estruturas
    public AvaliarCRUD(Tela tela, ObraCRUD obraCRUD, UsuarioCRUD usuarioCRUD)
    {
        this.avaliacao = new List<Avaliar>();
        this.avaliar = new Avaliar();
        this.posicao = -1;

        this.dados = new List<string>();
        this.dados.Add("ID");
        this.dados.Add("ID Obra");
        this.dados.Add("Curador");
        this.dados.Add("Preço Reserva");
        this.dados.Add("Observações");

        this.tela = tela;
        this.obraCRUD = obraCRUD;
        this.usuarioCRUD = usuarioCRUD;

        this.coluna = 8;
        this.linha = 8;
        this.largura = 80;

        this.larguraDados = this.largura - (this.dados.Count > 0 ? this.dados[0].Length : 10) - 2;
        this.colunaDados = this.coluna + (this.dados.Count > 0 ? this.dados[0].Length : 10) + 1;
        this.linhaDados = this.linha + 2;
    }

    public void ExecutarCRUD()
    {
        string opcao, resp;
        List<string> opcoesAv = new List<string>();

        // As strings foram alongadas com espaços para aumentar a largura do menu.
        opcoesAv.Add("  OBRAS DE ARTE  ");
        opcoesAv.Add("1 - Avaliar Obra         ");
        opcoesAv.Add("2 - Registrar Preço      ");
        opcoesAv.Add("3 - Listar Avaliações    ");
        opcoesAv.Add("0 - Sair                 ");



        while (true)
        {
            opcao = tela.MostrarMenu(opcoesAv, coluna, linha);
            if (opcao == "0") break;

            else if (opcao == "1" || opcao == "2")
            {
                this.coluna += 20;
                this.linha += 4;
                this.colunaDados += 20;
                this.linhaDados += 4;

                string titulo = (opcao == "1") ? "Registrar Avaliação" : "Registrar Preço";
                this.tela.MontarJanela(titulo, this.dados, this.coluna, this.linha, this.largura);

                this.EntrarDados(1);
                bool achou = this.ProcurarCodigo();

                if (opcao == "1" && achou)
                {
                    this.MostrarDados();
                    this.tela.MostrarMensagem("Obra já avaliada. Pressione uma tecla para continuar...");
                    Console.ReadKey();
                }
                else if (opcao == "1" && !achou)
                {
                    bool dadosValidos = this.EntrarDados(2);
                    if (!dadosValidos) break;
                    resp = this.tela.Perguntar("Confirma Avaliação (S/N) : ");
                    if (resp.ToLower() == "s")
                    {
                        this.avaliacao.Add(new Avaliar(this.avaliar.id, this.avaliar.idObra, this.avaliar.idCurador, this.avaliar.precoReserva, this.avaliar.observacoes, this.avaliar.dataAvaliacao));
                        this.tela.MostrarMensagem("Avaliação registrada com sucesso! Pressione uma tecla para continuar...");
                        Console.ReadKey();
                    }
                }
                else if (opcao == "2" && achou)
                {
                    this.avaliacao[this.posicao].precoReserva = this.avaliar.precoReserva;
                    this.MostrarDados();
                    this.tela.MostrarMensagem("Preço registrado com sucesso! Pressione uma tecla para continuar...");
                    Console.ReadKey();
                }

                this.tela.ApagarArea(this.coluna, this.linha, this.coluna + this.largura, this.linha + this.dados.Count + 2);

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

    public bool EntrarDados(int qual, bool alteracao = false)
    {
        if (qual == 1)
        {
            Console.SetCursorPosition(this.colunaDados, this.linhaDados);
            this.avaliar.id = int.Parse(Console.ReadLine());
        }
        else
        {
            // Solicita o ID da obra
            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 1);
            string idObra = Console.ReadLine();

            // Procura a obra completa pelo ID na ObraCRUD
            Obra obraEncontrada = null;
            foreach (var o in obraCRUD.ListarObras())
            {
                if (o.id == idObra)
                {
                    obraEncontrada = o;
                    break;
                }
            }

            if (obraEncontrada == null)
            {
                this.tela.MostrarMensagem("Obra não encontrada. Pressione uma tecla para continuar...");
                Console.ReadKey();
                return false;
            }

            // Preenche o ID da obra na avaliação
            this.avaliar.idObra = obraEncontrada.id;

            // Exibe automaticamente os dados da obra na tela
            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 1);
            Console.Write(obraEncontrada.nome);

            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 2);
            Console.Write(obraEncontrada.autor);

            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 3);
            Console.Write(obraEncontrada.ano);

            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 4);
            Console.Write(obraEncontrada.numero);

            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 5);
            Console.Write(obraEncontrada.estado);

            // Solicita apenas os dados da avaliação
            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 6);
            this.avaliar.idCurador = Console.ReadLine();

            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 7);
            this.avaliar.precoReserva = double.Parse(Console.ReadLine());

            Console.SetCursorPosition(this.colunaDados, this.linhaDados + 8);
            this.avaliar.observacoes = Console.ReadLine();

            this.avaliar.dataAvaliacao = DateTime.Now;
        }

        return true;
    }



    public void ListarAvaliacoes()
    {
        this.tela.PrepararTela("Listagem de Avaliações");
        this.tela.MostrarMensagem(1, 3, "ID | Obra      | Curador   | Preço de Reserva | Observações | Data Avaliação");
        this.tela.MostrarMensagem(1, 4, "---+-----------+-----------+-----------------+-------------+----------------");

        int linha = 5;
        foreach (var a in this.avaliacao)
        {
            Console.SetCursorPosition(1, linha);
            Console.Write(a.id);
            Console.SetCursorPosition(5, linha);
            Console.Write(a.idObra);
            Console.SetCursorPosition(16, linha);
            Console.Write(a.idCurador);
            Console.SetCursorPosition(27, linha);
            Console.Write(a.precoReserva);
            Console.SetCursorPosition(44, linha);
            Console.Write(a.observacoes);
            Console.SetCursorPosition(60, linha);
            Console.Write(a.dataAvaliacao.ToString("dd/MM/yyyy"));
            linha++;
        }

        this.tela.MostrarMensagem("");
        this.tela.MostrarMensagem("Pressione uma tecla para continuar...");
        Console.ReadKey();
    }

    public bool ProcurarCodigo()
    {
        bool encontrei = false;
        for (int i = 0; i < this.avaliacao.Count; i++)
        {
            if (this.avaliacao[i].id == this.avaliar.id)
            {
                encontrei = true;
                this.posicao = i;
                break;
            }
        }
        return encontrei;
    }

    public void MostrarDados()
    {
        // Busca novamente a obra pelo ID na ObraCRUD
        Obra obra = null;
        foreach (var o in obraCRUD.ListarObras())
        {
            if (o.id == this.avaliacao[this.posicao].idObra)
            {
                obra = o;
                break;
            }
        }

        if (obra != null)
        {
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 1, obra.nome);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 2, obra.autor);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 3, obra.ano);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 4, obra.numero);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 5, obra.estado);
        }

        this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 6, this.avaliacao[this.posicao].idCurador);
        this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 7, this.avaliacao[this.posicao].precoReserva.ToString());
        this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 8, this.avaliacao[this.posicao].observacoes);
        this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 9, this.avaliacao[this.posicao].dataAvaliacao.ToString("dd/MM/yyyy"));
    }

}
