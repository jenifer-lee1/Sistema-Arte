namespace SistemaArte
{
    public class UsuarioCRUD
    {
        // Propriedades
        private List<Usuario> usuarios;
        private Usuario usuario;
        private int posicao;
        private List<string> dados = new List<string>();
        private int coluna, linha, largura;
        private int larguraDados, colunaDados, linhaDados;
        private Tela tela;

        // Construtor
        public UsuarioCRUD(Tela tela)
        {
            this.usuarios = new List<Usuario>();
            this.usuario = new Usuario();
            this.posicao = -1;

            // Inicializar vetor com perguntas
            this.dados.Add("Nome:");
            this.dados.Add("Email:");
            this.dados.Add("Telefone:");
            this.dados.Add("Tipo de Usuário:");
            this.dados.Add("ID do Usuário:");

            // Indica onde está o objeto Tela
            this.tela = tela;

            // Define a posição e largura da janela
            this.coluna = 15;
            this.linha = 5;
            this.largura = 50;

            // Calcula a área dos dados
            this.larguraDados = this.largura - dados[0].Length - 2;
            this.colunaDados = this.coluna + dados[0].Length + 1;
            this.linhaDados = this.linha + 2;

            // Teste com usuários pré-cadastrados
            this.usuarios.Add(new Usuario("Ana Silva", "ana@gmail.com", "47995498241", "Comprador", 45151));
            this.usuarios.Add(new Usuario("Bruno Souza", "bruno@gmail.com", "47995498242", "Vendedor", 145152));
        }

        public void ExecutarCRUD()
        {
            string resp;

            // Montar a janela do CRUD
            this.tela.MontarJanela("Cadastro de Usuários", this.dados, this.coluna, this.linha, this.largura);

            // Algoritmo do CRUD
            this.EntrarDados(1);
            bool achou = this.ProcurarCodigo();
            if (!achou)
            {
                resp = this.tela.Perguntar("Usuário não encontrado. Deseja cadastrar (S/N): ");
                if (resp.ToLower() == "s")
                {
                    this.EntrarDados(2);
                    resp = this.tela.Perguntar("Confirmar cadastro (S/N): ");
                    if (resp.ToLower() == "s")
                    {
                        this.usuarios.Add(
                            new Usuario(
                                this.usuario.nome,
                                this.usuario.email,
                                this.usuario.telefone,
                                this.usuario.tipoUsuario,
                                this.usuario.id
                            )
                        );
                        this.tela.MostrarMensagem("Usuário cadastrado com sucesso! Pressione uma tecla para continuar...");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                this.MostrarDados();
                resp = this.tela.Perguntar("Deseja alterar, excluir ou voltar (A/E/V): ");
                if (resp.ToLower() == "a")
                {
                    this.tela.MontarJanela("Alteração de Usuário", this.dados, this.coluna, this.linha + this.dados.Count + 2, this.largura);
                    this.tela.MostrarMensagem("Informe os novos dados");
                    this.EntrarDados(2, true);
                    resp = this.tela.Perguntar("Confirma alteração (S/N): ");
                    if (resp.ToLower() == "s")
                    {
                        this.usuarios[this.posicao] = this.usuario;
                        this.tela.MostrarMensagem("Usuário alterado com sucesso! Pressione uma tecla para continuar...");
                        Console.ReadKey();
                    }
                }
                else if (resp.ToLower() == "e")
                {
                    resp = this.tela.Perguntar("Confirma exclusão (S/N): ");
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
                this.usuario.nome = Console.ReadLine();
            }
            else
            {
                // Se for alteração, desloca a linha para a "segunda tela"
                int deslocamentoLinha = alteracao ? this.dados.Count + 2 : 0;

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 1);
                this.usuario.nome = Console.ReadLine();

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 2);
                this.usuario.email = Console.ReadLine();

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 3);
                this.usuario.telefone = Console.ReadLine();

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 4);
                Console.Write("Escolha o tipo de usuário (1-Comprador / 2-Vendedor / 3-Curador): ");
                string escolha = Console.ReadLine();
                switch (escolha)
                {
                    case "1": this.usuario.tipoUsuario = "Comprador"; break;
                    case "2": this.usuario.tipoUsuario = "Vendedor"; break;
                    case "3": this.usuario.tipoUsuario = "Curador"; break;
                    default: this.usuario.tipoUsuario = "Indefinido"; break;
                }

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 5);
                this.usuario.id = int.Parse(Console.ReadLine());
            }
        }

        public bool ProcurarCodigo()
        {
            bool encontrei = false;
            for (int i = 0; i < this.usuarios.Count; i++)
            {
                if (this.usuario.nome == this.usuarios[i].nome)
                {
                    encontrei = true;
                    this.posicao = i;
                    break;
                }
            }
            return encontrei;
        }

        public string ObterNomePorEmail(string email)
        {
            this.usuario.email = email;
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

        // === Mantido 100% igual ao seu modelo ===
        // ...existing code...
        public void MostrarDados()
        {
            if (this.posicao < 0 || this.posicao >= this.usuarios.Count)
            {
                this.tela.MostrarMensagem("Nenhum usuário selecionado para exibir.");
                return;
            }

            var u = this.usuarios[this.posicao];

            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 1, u.nome);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 2, u.email);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 3, u.telefone);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 4, u.tipoUsuario);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 5, u.id.ToString());
        }

    }
}