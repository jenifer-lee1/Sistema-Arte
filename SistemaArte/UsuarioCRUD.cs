using System;
using System.Collections.Generic;

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

            this.dados.Add("Nome                                       : ");
            this.dados.Add("ID                                         : ");
            this.dados.Add("E-mail                                     : ");
            this.dados.Add("Telefone                                   : ");
            this.dados.Add("Tipo Usuário (Curador/Vendedor/Comprador)  : ");

            this.tela = tela;

            this.coluna = 8;
            this.linha = 10;
            this.largura = 80;

            this.larguraDados = this.largura - dados[0].Length - 2;
            this.colunaDados = this.coluna + dados[0].Length + 1;
            this.linhaDados = this.linha + 2;

            // dados iniciais
            this.usuarios.Add(new Usuario("Ana", 2025001, "ana@gmail.com", "123", "Vendedor"));
            this.usuarios.Add(new Comprador("Gael", 2025002, "bruno@uol.com", "456", "9999-0000-1111-2222", "Banco XPTO") { tipoCartao = "Crédito" });
        }

        public void ExecutarCRUD()
        {
            string resp;

            this.tela.MontarJanela("Cadastro de Usuários", this.dados, this.coluna, this.linha, this.largura);

            this.EntrarDados(1);
            bool achou = this.ProcurarCodigo();
            if (!achou)
            {
                resp = this.tela.Perguntar("Usuário não encontrado. Deseja cadastrar (S/N) : ");
                if (resp.ToLower() == "s")
                {
                    this.EntrarDados(2);
                    if (this.usuario.TipoUsuario.ToLower() == "comprador")
                    {
                        this.EntrarDadosComprador();
                    }

                    resp = this.tela.Perguntar("Confirma cadastro (S/N) : ");
                    if (resp.ToLower() == "s")
                    {
                        if (this.usuario is Comprador comprador)
                            this.usuarios.Add(comprador);
                        else
                            this.usuarios.Add(this.usuario);
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
                    if (this.usuario.TipoUsuario.ToLower() == "comprador")
                    {
                        this.EntrarDadosComprador();
                    }
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
                int deslocamentoLinha = alteracao ? this.dados.Count + 2 : 0;

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 1);
                int.TryParse(Console.ReadLine(), out this.usuario.id);

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 2);
                this.usuario.email = Console.ReadLine();

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 3);
                this.usuario.telefone = Console.ReadLine();

                Console.SetCursorPosition(this.colunaDados, this.linhaDados + deslocamentoLinha + 4);
                this.usuario.TipoUsuario = Console.ReadLine();

                if (this.usuario.TipoUsuario.ToLower() == "comprador")
                {
                    this.usuario = new Comprador(this.usuario.nome, this.usuario.id, this.usuario.email, this.usuario.telefone, "", "");
                }
            }
        }

        // ✅ Tela extra para COMPRADOR (agora com tipo de cartão)
        public void EntrarDadosComprador()
        {
            Comprador c = (Comprador)this.usuario;
            List<string> campos = new List<string>();
            campos.Add("Número do Cartão                         : ");
            campos.Add("Nome do Banco                            : ");
            campos.Add("Tipo do Cartão (Crédito/Débito)          : ");

            int linhaExtra = this.linha + this.dados.Count + 5;
            this.tela.MontarJanela("Dados do Comprador", campos, this.coluna, linhaExtra, this.largura);

            Console.SetCursorPosition(this.coluna + campos[0].Length + 1, linhaExtra + 2);
            c.numeroCartao = Console.ReadLine();

            Console.SetCursorPosition(this.coluna + campos[1].Length + 1, linhaExtra + 3);
            c.nomeBanco = Console.ReadLine();

            Console.SetCursorPosition(this.coluna + campos[2].Length + 1, linhaExtra + 4);
            c.tipoCartao = Console.ReadLine();

            this.usuario = c;
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

            // ✅ Se for COMPRADOR, mostra moldura extra com os dados bancários
            if (this.usuarios[this.posicao] is Comprador comp)
            {
                List<string> camposComprador = new List<string>();
                camposComprador.Add("Número do Cartão                         : " + comp.numeroCartao);
                camposComprador.Add("Nome do Banco                            : " + comp.nomeBanco);
                camposComprador.Add("Tipo do Cartão (Crédito/Débito)          : " + comp.tipoCartao);

                int linhaExtra = this.linha + this.dados.Count + 5;
                this.tela.MontarJanela("Dados do Comprador", camposComprador, this.coluna, linhaExtra, this.largura);
            }
        }

        public string ObterNomePorID(string id)
        {
            foreach (Usuario u in this.usuarios)
            {
                if (u.id.ToString() == id)
                {
                    return u.nome;
                }
            }
            return "Usuário não encontrado";
        }

        public Usuario ObterUsuarioPorCodigo(string codigo)
        {
            foreach (Usuario u in this.usuarios)
            {
                if (u.id.ToString() == codigo)
                {
                    return u;
                }
            }
            return null;
        }



    }
}
