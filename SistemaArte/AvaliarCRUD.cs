using System;
using System.Collections.Generic;

namespace SistemaArte
{
    public class AvaliarCRUD
    {
        private List<Avaliar> avaliacoes;
        private Avaliar avaliacao;
        private int posicao;
        private List<string> dados;
        private int coluna, linha, largura;
        private int larguraDados, colunaDados, linhaDados;
        private Tela tela;
        private ObraCRUD obraCRUD;

        public AvaliarCRUD(Tela tela, ObraCRUD obraCRUD)
        {
            this.tela = tela;
            this.obraCRUD = obraCRUD;
            this.avaliacoes = new List<Avaliar>();
            this.avaliacao = new Avaliar();
            this.posicao = -1;

            // Campos da avaliação
            this.dados = new List<string>();
            this.dados.Add("Id da Obra                                   : ");
            this.dados.Add("Autenticidade (Original/Réplica/Restaurada)  : ");
            this.dados.Add("Número do Certificado de Autenticidade       : ");
            this.dados.Add("Preço de Reserva                             : ");
            this.dados.Add("Preço Confidencial (S/N)                     : ");
            this.dados.Add("Nome do Curador                              : ");

            this.coluna = 8;
            this.linha = 14;
            this.largura = 80;

            this.larguraDados = this.largura - dados[0].Length - 2;
            this.colunaDados = this.coluna + dados[0].Length + 1;
            this.linhaDados = this.linha + 2;
        }

        public void ExecutarCRUD()
        {
            string resp;

            this.tela.MontarJanela("Avaliação de Obras", this.dados, this.coluna, this.linha, this.largura);

            // Solicita ID da obra
            EntrarDados(1);

            // Procura avaliação existente
            bool achou = ProcurarCodigo();

            // Verifica se a obra existe
            Obra obra = this.obraCRUD.ListarObras().Find(o => o.id == this.avaliacao.idObra);
            if (obra == null)
            {
                this.tela.MostrarMensagem("Obra não encontrada no sistema!");
                Console.ReadKey();
                return;
            }

            // Preenche automaticamente campos que não devem ser alterados
            this.avaliacao.autenticidade = obra.estado;
            this.avaliacao.numeroCertificado = obra.numero;

            if (!achou)
            {
                resp = this.tela.Perguntar("Avaliação não encontrada. Deseja cadastrar (S/N) : ");
                if (resp.ToLower() == "s")
                {
                    EntrarDados(2);
                    resp = this.tela.Perguntar("Confirma avaliação (S/N) : ");
                    if (resp.ToLower() == "s")
                    {
                        this.avaliacoes.Add(
                            new Avaliar(
                                this.avaliacao.idObra,
                                this.avaliacao.autenticidade,
                                this.avaliacao.numeroCertificado,
                                this.avaliacao.precoR,
                                this.avaliacao.confidencial,
                                this.avaliacao.curador
                            )
                        );
                        this.tela.MostrarMensagem("Obra avaliada com sucesso!");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                MostrarDados();
                resp = this.tela.Perguntar("Deseja alterar, excluir ou voltar (A/E/V) : ");
                if (resp.ToLower() == "a")
                {
                    this.tela.MontarJanela("Alteração de Avaliação", this.dados, this.coluna, this.linha + this.dados.Count + 2, this.largura);
                    this.tela.MostrarMensagem("Informe os novos dados");
                    EntrarDados(2, true);
                    resp = this.tela.Perguntar("Confirma alteração (S/N) : ");
                    if (resp.ToLower() == "s")
                    {
                        this.avaliacoes[this.posicao] = this.avaliacao;
                        this.tela.MostrarMensagem("Avaliação alterada com sucesso! Pressione uma tecla para continuar...");
                        Console.ReadKey();
                    }
                }
                else if (resp.ToLower() == "e")
                {
                    resp = this.tela.Perguntar("Confirma exclusão (S/N) : ");
                    if (resp.ToLower() == "s")
                    {
                        this.avaliacoes.RemoveAt(this.posicao);
                        this.tela.MostrarMensagem("Avaliação excluída com sucesso! Pressione uma tecla para continuar...");
                        Console.ReadKey();
                    }
                }
            }
        }

        public void EntrarDados(int qual, bool alteracao = false)
        {
            int deslocamentoLinha = alteracao ? this.dados.Count + 2 : 0;

            if (qual == 1)
            {
                Console.SetCursorPosition(this.colunaDados, this.linhaDados);
                this.avaliacao.idObra = Console.ReadLine();
            }
            else
            {
                // Apenas os campos que o curador preenche manualmente
                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 3);
                this.avaliacao.precoR = Console.ReadLine();

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 4);
                this.avaliacao.confidencial = Console.ReadLine();

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 5);
                this.avaliacao.curador = Console.ReadLine();
            }
        }

        public bool ProcurarCodigo()
        {
            bool encontrei = false;
            for (int i = 0; i < this.avaliacoes.Count; i++)
            {
                if (this.avaliacoes[i].idObra == this.avaliacao.idObra)
                {
                    this.posicao = i;
                    encontrei = true;
                    break;
                }
            }
            return encontrei;
        }

        public void MostrarDados()
        {
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 1, this.avaliacao.autenticidade);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 2, this.avaliacao.numeroCertificado);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 3, this.avaliacao.precoR);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 4, this.avaliacao.confidencial);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 5, this.avaliacao.curador);
        }
    }
}
