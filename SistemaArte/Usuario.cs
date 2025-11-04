namespace SistemaArte
{
    public class Usuario
    {
        //propriedades
        public string nome;
        public string email;
        public string telefone;
        public string tipoUsuario;
        public int id;


        //Construtor
        public Usuario()
        {
            this.nome = "";
            this.email = "";
            this.telefone = "";
            this.tipoUsuario = "";
            this.id = 0;
        }

        public Usuario(string nome, string email, string telefone, string tipoUsuario, int id)
        {
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.tipoUsuario = tipoUsuario;
            this.id = id;

        }
    }
}