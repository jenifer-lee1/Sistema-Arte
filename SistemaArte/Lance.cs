public class Lance
{
    public int id;
    public string cod;
    public int valor;
    public DateTime dataLance;
    //public DateTime dataDevolucao;
    //public DateTime? dataDevolvido;

    public Lance(int id, string cod, int valor, DateTime dataLance)
    {
        this.id = id;
        this.cod = cod;
        this.valor = valor;
        this.dataLance = dataLance;
        //this.dataDevolucao = dataDevolucao;
        //this.dataDevolvido = dataDevolvido;
    }

    public Lance()
    {
        this.id = 0;
        this.cod = "";
        this.valor = 0;
        this.dataLance = DateTime.MinValue;
        //this.dataDevolucao = DateTime.MinValue;
        //this.dataDevolvido = null;
    }
}
