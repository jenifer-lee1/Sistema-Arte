public class Obra
{
    // propriedades
    public int id;
    public string nome;
    public string autor;
    public int ano;
    public int numero;
    public string estado;

    // construtor
    public Obra()
    {
        this.id = 0;
        this.nome = "";
        this.ano = 0;
        this.numero = 0;
        this.estado = "";

    }


    public Obra(int id, string nome, string autor, int ano, int numero, string estado)
    {
        this.id = id;
        this.nome = nome;
        this.autor = autor;
        this.ano = ano;
        this.numero = numero;
        this.estado = estado;

    }
}