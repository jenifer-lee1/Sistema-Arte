public class Obra
{
    // propriedades
    public string idObra;
    public string nome;
    public string autor;
    public string ano;
    public string numero;
    public string estado;

    // construtor
    public Obra()
    {
        this.idObra = "";
        this.nome = "";
        this.autor = "";
        this.ano = "";
        this.numero = "";
        this.estado = "";

    }


    public Obra(string idObra, string nome, string autor, string ano, string numero, string estado)
    {
        this.idObra = idObra;
        this.nome = nome;
        this.autor = autor;
        this.ano = ano;
        this.numero = numero;
        this.estado = estado;

    }
}