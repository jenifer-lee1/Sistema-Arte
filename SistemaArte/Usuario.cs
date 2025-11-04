public class Usuario
{
    // propriedades
    public string nome;
    public string id;
    public string email;
    public string telefone;
    public string TipoUsuario;

    // construtor
    public Usuario()
    {
        this.nome = "";
        this.id = "";
        this.email = "";
        this.telefone = "";
        this.TipoUsuario = "";
    }


    public Usuario(string id, string nome, string email, string telefone, string TipoUsuario)
    {
        this.id = id;
        this.nome = nome;
        this.email = email;
        this.telefone = telefone;
        this.TipoUsuario = TipoUsuario;
    }
}