namespace SistemaArte
{
    public class Usuario
    {
        //propriedades
        public string nome;
        public string id;
        public string email;
        public string telefone;
        public string tipoUsuario;



        //Construtor
        public Usuario()
        {

            this.nome = "";
            this.id = "";
            this.email = "";
            this.telefone = "";
            this.tipoUsuario = "";

        }

        public Usuario(string nome, string id, string email, string telefone, string tipoUsuario)
        {
            this.nome = nome;
            this.id = id;
            this.email = email;
            this.telefone = telefone;
            this.tipoUsuario = tipoUsuario;

        }
    }
}