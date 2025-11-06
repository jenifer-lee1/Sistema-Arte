public class Lance
{
    public int id;
    public string cod;
    public string idObra;
    public string valor;
    public DateTime dataLance;
    //public DateTime dataDevolucao;
    //public DateTime? dataDevolvido;

    public Lance(int id, string cod, string valor, DateTime dataLance)
    {
        this.id = id;
        this.idObra = "";
        this.cod = cod;
        this.valor = valor;
        this.dataLance = dataLance;
        //this.dataDevolucao = dataDevolucao;
        //this.dataDevolvido = dataDevolvido;
    }

    // New constructor including obra id
    public Lance(int id, string idObra, string cod, string valor, DateTime dataLance)
    {
        this.id = id;
        this.idObra = idObra;
        this.cod = cod;
        this.valor = valor;
        this.dataLance = dataLance;
    }

    public Lance()
    {
        this.id = 0;
        this.idObra = "";
        this.valor = "";
        this.dataLance = DateTime.MinValue;
        //this.dataDevolucao = DateTime.MinValue;
        //this.dataDevolvido = null;
    }
}
