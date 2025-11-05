using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

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
            this.usuarios = new List<Usuario>(); // inicializa a coleção
            this.usuario = new Usuario();        // inicializa o objeto
            this.posicao = -1;                   // inicializa o "ponteiro"

            // ordem dos campos: Nome primeiro, depois ID
            this.dados.Add("Nome                                       : ");
            this.dados.Add("ID                                         : ");
            this.dados.Add("E-mail                                     : ");
            this.dados.Add("Telefone                                   : ");
            this.dados.Add("Tipo Usuário (Curador/Vendedor/Comprador)  : ");

            this.tela = tela;

            this.coluna = 15;
            this.linha = 6;
            this.largura = 80;

            this.larguraDados = this.largura - dados[0].Length - 2;
            this.colunaDados = this.coluna + dados[0].Length + 1;
            this.linhaDados = this.linha + 2;

            // inclusão de dados iniciais
            this.usuarios.Add(new Usuario("Ana", 2025001, "ana@gmail.com", "123", "Vendedor"));
            this.usuarios.Add(new Usuario("Gael", 2025002, "bruno@uol.com", "456", "Comprador"));
        }


        public void ExecutarCRUD()
        {
            string resp;

            this.tela.MontarJanela("Cadastro de Usuários", this.dados, this.coluna, this.linha, this.largura);

            // entrada inicial — busca pelo nome
            this.EntrarDados(1);
            bool achou = this.ProcurarCodigo();
            if (!achou)
            {
                resp = this.tela.Perguntar("Usuário não encontrado. Deseja cadastrar (S/N) : ");
                if (resp.ToLower() == "s")
                {
                    this.EntrarDados(2);
                    resp = this.tela.Perguntar("Confirma cadastro (S/N) : ");
                    if (resp.ToLower() == "s")
                    {
                        this.usuarios.Add(
                            new Usuario(this.usuario.nome, this.usuario.id, this.usuario.email, this.usuario.telefone, this.usuario.TipoUsuario)
                        );
                    }
                }
            }
            else
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
                this.usuario.nome = Console.ReadLine();
            }
            else
            {
                // desloca se for alteração
                int deslocamentoLinha = alteracao ? this.dados.Count + 2 : 0;

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 1);
                int.TryParse(Console.ReadLine(), out this.usuario.id);

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 2);
                this.usuario.email = Console.ReadLine();

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 3);
                this.usuario.telefone = Console.ReadLine();

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 4);
                this.usuario.TipoUsuario = Console.ReadLine();
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


        public void MostrarDados()
        {
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 1, this.usuarios[this.posicao].id.ToString());
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 2, this.usuarios[this.posicao].email);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 3, this.usuarios[this.posicao].telefone);
            this.tela.MostrarMensagem(this.colunaDados, this.linhaDados + 4, this.usuarios[this.posicao].TipoUsuario);
        }
    }
}
