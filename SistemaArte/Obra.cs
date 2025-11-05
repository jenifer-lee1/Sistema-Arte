public class Obra
{
    // propriedades
    public string id;
    public string nome;
    public string autor;
    public string ano;
    public string numero;
    public string estado;

    // construtor
    public Obra()
    {
        this.id = "";
        this.nome = "";
        this.autor = "";
        this.ano = "";
        this.numero = "";
        this.estado = "";

    }


    public Obra(string id, string nome, string autor, string ano, string numero, string estado)
    {
        this.id = id;
        this.nome = nome;
        this.autor = autor;
        this.ano = ano;
        this.numero = numero;
        this.estado = estado;

    }
}