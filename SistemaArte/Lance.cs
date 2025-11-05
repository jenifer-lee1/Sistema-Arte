public class Lance
{
    public int id;
    public string cod;
    public int valor;
    public DateTime dataLance;
    //public DateTime DataDevolucao;
    //public DateTime? DataDevolvido;




    public Lance(int id, string cod, int valor, DateTime dataLance)
    {
        this.id = id;
        this.cod = cod;
        this.valor = valor;
        this.DataLance = dataLance;
        //this.DataDevolucao = dataDevolucao;
        //this.DataDevolvido = dataDevolvido;
    }



    public Lance()
    {
        this.id = 0;
        this.cod = "";
        this.valor = "";
        this.Datalance = DateTime.MinValue;
        //this.DataDevolucao = DateTime.MinValue;
        //this.DataDevolvido = DateTime.MinValue;
    }
}