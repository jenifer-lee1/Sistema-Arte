using System;
using System.Collections.Generic;

namespace SistemaArte
{
    public class ObraCRUD
    {
        private List<Obra> obras;
        private Obra obra;
        private int posicao;
        private List<string> dados = new List<string>();
        private int coluna, linha, largura;
        private int larguraDados, colunaDados, linhaDados;
        private Tela tela;


        public ObraCRUD(Tela tela)
        {
            this.obras = new List<Obra>();
            this.obra = new Obra();
            this.posicao = -1;

            this.dados.Add("Id                                           : ");
            this.dados.Add("Nome                                         : ");
            this.dados.Add("Autor                                        : ");
            this.dados.Add("Ano de criação                               : ");
            this.dados.Add("Número do Certificado de Autenticidade       : ");
            this.dados.Add("Estado da Obra (Original/Restaurada/Réplica) : ");

            this.tela = tela;

            this.coluna = 8;
            this.linha = 14;
            this.largura = 80;

            this.larguraDados = this.largura - dados[0].Length - 2;
            this.colunaDados = this.coluna + dados[0].Length + 1;
            this.linhaDados = this.linha + 2;

            // dados iniciais
            this.obras.Add(new Obra("12505", "Monalisa", "Leonardo da Vinci", "1503", "1001", "Original"));
            this.obras.Add(new Obra("12506", "O Grito", "Edvard Munch", "1893", "1002", "Original"));
        }

        public void ExecutarCRUD()
        {
            string resp;

            // montar a janela do CRUD
            this.tela.MontarJanela("Cadastro de Obras", this.dados, this.coluna, this.linha, this.largura);

            // algoritmo CRUD
            this.EntrarDados(1);
            bool achou = this.ProcurarCodigo();
            if (!achou)
            {
                resp = this.tela.Perguntar("Id da Obra não encontrada. Deseja cadastrar (S/N) : ");
                if (resp.ToLower() == "s")
                {
                    this.EntrarDados(2);
                    resp = this.tela.Perguntar("Confirma cadastro (S/N) : ");
                    if (resp.ToLower() == "s")
                    {
                        this.obras.Add(
                            new Obra(this.obra.id, this.obra.nome, this.obra.autor, this.obra.ano, this.obra.numero, this.obra.estado)
                        );
                    }
                }
            }
            else // if (achou)
            {
                this.MostrarDados();
                resp = this.tela.Perguntar("Deseja alterar, excluir ou voltar (A/E/V) : ");
                if (resp.ToLower() == "a")
                {
                    this.tela.MontarJanela("Alteração de Obras", this.dados, this.coluna, this.linha + this.dados.Count + 2, this.largura);
                    this.tela.MostrarMensagem("Informe os novos dados");
                    this.EntrarDados(2, true);
                    resp = this.tela.Perguntar("Confirma alteração (S/N) : ");
                    if (resp.ToLower() == "s")
                    {
                        this.obras[this.posicao] = this.obra;
                        this.tela.MostrarMensagem("Obra alterada com sucesso! Pressione uma tecla para continuar...");
                        Console.ReadKey();
                    }
                }
                else if (resp.ToLower() == "e")
                {
                    resp = this.tela.Perguntar("Confirma exclusão (S/N) : ");
                    if (resp.ToLower() == "s")
                    {
                        this.obras.RemoveAt(this.posicao);
                        this.tela.MostrarMensagem("Obra excluída com sucesso! Pressione uma tecla para continuar...");
                        Console.ReadKey();
                    }
                }
            }
        }


        public void EntrarDados(int qual, bool alteracao = false)
        {
            if (qual == 1)
            {
                Console.SetCursorPosition(this.colunaDados, this.linhaDados);
                this.obra.id = Console.ReadLine();
            }
            else
            {
                // se for alteração, desloca a linha para a "segunda tela"
                int deslocamentoLinha = alteracao ? this.dados.Count + 2 : 0;

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 1);
                this.obra.nome = Console.ReadLine();
                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 2);
                this.obra.autor = Console.ReadLine();
                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 3);
                this.obra.ano = Console.ReadLine();
                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 4);
                this.obra.numero = Console.ReadLine();
                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 5);
                this.obra.estado = Console.ReadLine();
            }
        }


        public bool ProcurarCodigo()
        {
            bool encontrei = false;
            for (int i = 0; i < this.obras.Count; i++)
            {
                if (this.obra.id == this.obras[i].id)
                {
                    encontrei = true;
                    this.posicao = i;
                    break;
                }
            }
            return encontrei;
        }



        public string ObterNomePorId(string id)
        {
            this.obra.id = id;
            bool achou = this.ProcurarCodigo();
            if (achou)
            {
                return this.obras[this.posicao].nome;
            }
            else
            {
                return "";
            }
        }
        public List<Obra> ListarObras()
        {
            return this.obras;
        }

        public void MostrarDados()
        {
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 1, this.obras[this.posicao].nome);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 2, this.obras[this.posicao].autor);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 3, this.obras[this.posicao].ano);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 4, this.obras[this.posicao].numero);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 5, this.obras[this.posicao].estado);
        }

    }
}