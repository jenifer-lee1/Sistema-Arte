using System;
using System.Collections.Generic;

namespace SistemaArte
{
    public class UsuarioCRUD
    {
        //
        // Propriedades
        //
        private List<Usuario> usuarios;
        private Usuario usuario;
        private int posicao;
        private List<string> dados = new List<string>();
        private int coluna, linha, largura;
        private int larguraDados, colunaDados, linhaDados;
        private Tela tela;


        //
        // Métodos
        //
        public UsuarioCRUD(Tela tela)
        {
            // propriedades para o CRUD
            this.usuarios = new List<Usuario>(); // inicializa a coleção de usuários
            this.usuario = new Usuario();        // inicializa o objeto para manipulação dos dados de UM usuário
            this.posicao = -1;               // inicializa o "ponteiro" da coleção de usuários

            // inicializa o vetor com as perguntas de usuário
            this.dados.Add("ID                                           : ");
            this.dados.Add("Nome                                         : ");
            this.dados.Add("E-mail                                       : ");
            this.dados.Add("Telefone                                     : ");
            this.dados.Add("Tipo de Usuário (Vendedor/Comprador/Curador) : ");

            // indica para UsuarioCRUD onde está o objeto tela
            this.tela = tela;

            // define a posição e largura da janela
            this.coluna = 15;
            this.linha = 6;
            this.largura = 80;

            // calcula a área dos dados
            this.larguraDados = this.largura - dados[0].Length - 5;
            this.colunaDados = this.coluna + dados[0].Length + 3;
            this.linhaDados = this.linha + 2;

            // inclusão de dados iniciais para teste
            this.usuarios.Add(new Usuario("2025001", "Ana Souza", "ana@gmail.com", "123", "Vendedor"));
            this.usuarios.Add(new Usuario("2025002", "Bruno Lima", "bruno@uol.com", "456", "Comprador"));
        }

        public void ExecutarCRUD()
        {
            string resp;

            // montar a janela do CRUD
            this.tela.MontarJanela("Cadastro de Usuários", this.dados, this.coluna, this.linha, this.largura);

            // algoritmo CRUD
            this.EntrarDados(1);
            bool achou = this.ProcurarCodigo();
            if (!achou)
            {
                resp = this.tela.Perguntar("ID não encontrado. Deseja cadastrar (S/N) : ");
                if (resp.ToLower() == "s")
                {
                    this.EntrarDados(2);
                    resp = this.tela.Perguntar("Confirma cadastro (S/N) : ");
                    if (resp.ToLower() == "s")
                    {
                        this.usuarios.Add(
                            new Usuario(this.usuario.id, this.usuario.nome, this.usuario.email, this.usuario.telefone, this.usuario.TipoUsuario)
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
                    this.tela.MontarJanela("Alteração de Usuário", this.dados, this.coluna, this.linha + this.dados.Count + 2, this.largura);
                    this.tela.MostrarMensagem("Informe os novos dados");
                    this.EntrarDados(2, true);
                    resp = this.tela.Perguntar("Confirma alteração (S/N) : ");
                    if (resp.ToLower() == "s")
                    {
                        this.usuarios[this.posicao] = this.usuario;
                        this.tela.MostrarMensagem("Usuário alterado com sucesso! Pressione uma tecla para continuar...");
                        Console.ReadKey();
                    }
                }
                else if (resp.ToLower() == "e")
                {
                    resp = this.tela.Perguntar("Confirma exclusão (S/N) : ");
                    if (resp.ToLower() == "s")
                    {
                        this.usuarios.RemoveAt(this.posicao);
                        this.tela.MostrarMensagem("Usuário excluído com sucesso! Pressione uma tecla para continuar...");
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
                this.usuario.id = Console.ReadLine();
            }
            else
            {
                // se for alteração, desloca a linha para a "segunda tela"
                int deslocamentoLinha = alteracao ? this.dados.Count + 2 : 0;

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 1);
                this.usuario.id = Console.ReadLine();
                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 2);
                this.usuario.nome = Console.ReadLine();
                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 3);
                this.usuario.email = Console.ReadLine();
                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 4);
                this.usuario.telefone = Console.ReadLine();

                // 🔧 correção da posição do cursor no campo "Tipo de Usuário"
                Console.SetCursorPosition(this.coluna + this.dados[4].Length + 1, this.linhaDados + deslocamentoLinha + 5);
                this.usuario.TipoUsuario = Console.ReadLine();
            }
        }

        public bool ProcurarCodigo()
        {
            bool encontrei = false;
            for (int i = 0; i < this.usuarios.Count; i++)
            {
                if (this.usuario.id == this.usuarios[i].id)
                {
                    encontrei = true;
                    this.posicao = i;
                    break;
                }
            }
            return encontrei;
        }

        public string ObterNomePorMatricula(string matricula)
        {
            this.usuario.id = matricula;
            bool achou = this.ProcurarCodigo();
            if (achou)
            {
                return this.usuarios[this.posicao].nome;
            }
            else
            {
                return "";
            }
        }

        public void MostrarDados()
        {
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 1, this.usuarios[this.posicao].nome);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 2, this.usuarios[this.posicao].email);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 3, this.usuarios[this.posicao].telefone);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 4, this.usuarios[this.posicao].TipoUsuario);
        }

    }
}
