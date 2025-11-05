public class Comprador : Usuario
{
    public string numeroCartao;
    public string nomeBanco;
    public string tipoCartao;


    public Comprador() : base()
    {
        this.numeroCartao = "";
        this.nomeBanco = "";
        this.tipoCartao = "";
        this.TipoUsuario = "Comprador";
    }

    public Comprador(string nome, int id, string email, string telefone, string numeroCartao, string nomeBanco)
    {
        this.nome = nome;
        this.id = id;
        this.email = email;
        this.telefone = telefone;
        this.numeroCartao = numeroCartao;
        this.nomeBanco = nomeBanco;
        this.TipoUsuario = "Comprador";
    }


}