public class Obra
{
    // propriedades
    public string nome;
    public string autor;
    public string codigo;
    public int anoCriacao;
    public int valor;
    // métodos
    public Obra(string cod, string tit, string aut, int ano, int val)
    {
        codigo = cod;
        nome = tit;
        autor = aut;
        anoCriacao = ano;
        valor = val;
    }

    public Obra()
    {
        this.codigo = "";
        this.nome = "";
        this.autor = "";
        this.anoCriacao = 0;
        this.valor = 0;
    }
    public void ImprimirDados()
    {
        Console.WriteLine("Dados do Obra:");
        Console.WriteLine($"Código da Obra    : {codigo}");
        Console.WriteLine($"Nome            : {nome}");
        Console.WriteLine($"Autor/Pintor       : {autor}");
        Console.WriteLine($"Ano de Criação : {anoCriacao}");
        Console.WriteLine($"Valor : {valor}");
        Console.WriteLine("");
    }
}
