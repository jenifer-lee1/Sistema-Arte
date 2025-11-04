using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaArte
{
    public class UsuarioCRUD
    {
        private List<Usuario> usuarios;
        private Usuario usuario;
        private int posicao;
        private List<string> dados = new List<string>();
        private int coluna, linha, largura;
        private int larguraDados, colunaDados, linhaDados;
        private Tela tela;

        public UsuarioCRUD(Tela tela)
        {
            this.usuarios = new List<Usuario>();
            this.usuario = new Usuario();
            this.posicao = -1;

            // Campos que serão exibidos
            this.dados.Add("Nome:");
            this.dados.Add("ID do Usuário:");
            this.dados.Add("Email:");
            this.dados.Add("Telefone:");
            this.dados.Add("Tipo de Usuário [(Comprador)/(Vendedor)/(Curador)]:");

            this.tela = tela;

            this.coluna = 5;
            this.linha = 12;
            this.largura = 70;

            // Calcular posições com base no maior rótulo
            int maiorLabel = this.dados.Max(d => d.Length);
            this.colunaDados = this.coluna + maiorLabel + 2;
            this.larguraDados = this.largura - maiorLabel - 6;
            this.linhaDados = this.linha + 2;

            // Usuários para teste
            this.usuarios.Add(new Usuario("Ana Silva", "12352", "ana@gmail.com", "47995498241", "Comprador"));
            this.usuarios.Add(new Usuario("Bruno Souza", "12353", "bruno@gmail.com", "47995498242", "Vendedor"));
        }

        public void ExecutarCRUD()
        {
            string resp;

            // Montar janela abaixo do menu
            this.tela.MontarJanela("Cadastro de Usuários", this.dados, this.coluna, this.linha, this.largura);

            // Entrada e verificação
            this.EntrarDados(1);
            bool achou = this.ProcurarCodigo();

            if (!achou)
            {
                resp = this.tela.Perguntar("Usuário não encontrado. Deseja cadastrar? (S/N): ");
                if (resp.ToLower() == "s")
                {
                    this.EntrarDados(2);
                    resp = this.tela.Perguntar("Confirmar cadastro? (S/N): ");
                    if (resp.ToLower() == "s")
                    {
                        this.usuarios.Add(
                            new Usuario(
                                this.usuario.nome,
                                this.usuario.id,
                                this.usuario.email,
                                this.usuario.telefone,
                                this.usuario.tipoUsuario
                            )
                        );
                        this.tela.MostrarMensagem("Usuário cadastrado com sucesso! Pressione uma tecla para continuar...");
                        Console.ReadKey();
                    }
                }
            } // 👈 ESSE estava faltando (fecha o if (!achou))

            else
            {
                this.MostrarDados();
                resp = this.tela.Perguntar("Deseja alterar, excluir ou voltar (A/E/V): ");
                if (resp.ToLower() == "a")
                {
                    this.tela.MontarJanela("Alteração de Usuário", this.dados, this.coluna, this.linha + this.dados.Count + 3, this.largura);
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
                // Pega o comprimento do rótulo "Nome:" e posiciona logo depois dele
                Console.SetCursorPosition(this.coluna + this.dados[0].Length + 1, this.linha + 2);
                this.usuario.nome = Console.ReadLine();
            }
            else
            {
                int deslocamentoLinha = alteracao ? this.dados.Count + 2 : 0;

                // Cada campo usa o tamanho do respectivo rótulo
                Console.SetCursorPosition(this.coluna + this.dados[0].Length + 1, this.linha + 2 + deslocamentoLinha);
                this.usuario.nome = Console.ReadLine();

                Console.SetCursorPosition(this.coluna + this.dados[1].Length + 1, this.linha + 3 + deslocamentoLinha);
                this.usuario.id = Console.ReadLine();

                Console.SetCursorPosition(this.coluna + this.dados[2].Length + 1, this.linha + 4 + deslocamentoLinha);
                this.usuario.email = Console.ReadLine();

                Console.SetCursorPosition(this.coluna + this.dados[3].Length + 1, this.linha + 5 + deslocamentoLinha);
                this.usuario.telefone = Console.ReadLine();

                Console.SetCursorPosition(this.coluna + this.dados[4].Length + 1, this.linha + 6 + deslocamentoLinha);
                this.usuario.tipoUsuario = Console.ReadLine();
            }
        }



        public bool ProcurarCodigo()
        {
            for (int i = 0; i < this.usuarios.Count; i++)
            {
                if (this.usuario.nome.Equals(this.usuarios[i].nome, StringComparison.OrdinalIgnoreCase))
                {
                    this.posicao = i;
                    return true;
                }
            }
            return false;
        }

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
