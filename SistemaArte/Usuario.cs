public class Usuario
{
    // propriedades
    public string nome;
    public int id;
    public string email;
    public string telefone;
    public string TipoUsuario;

    // construtor
    public Usuario()
    {
        this.nome = "";
        this.id = 0;
        this.email = "";
        this.telefone = "";
        this.TipoUsuario = "";
    }


    public Usuario(string nome, int id, string email, string telefone, string TipoUsuario)
    {
        this.nome = nome;
        this.id = id;
        this.email = email;
        this.telefone = telefone;
        this.TipoUsuario = TipoUsuario;
    }
}