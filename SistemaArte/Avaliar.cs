using System.Globalization;

public class Avaliar
{
    public string idObra;
    public string autenticidade;
    public string numeroCertificado;
    public string precoR;
    public string confidencial;
    public string curador;

    public Avaliar()
    {
        this.idObra = "";
        this.autenticidade = "";
        this.precoR = "";
        this.numeroCertificado = "";
        this.confidencial = "";
        this.curador = "";
    }

    public Avaliar(string idObra, string autenticidade, string numeroCertificado, string precoR, string confidencial, string curador)
    {
        this.idObra = idObra;
        this.autenticidade = autenticidade;
        this.numeroCertificado = numeroCertificado;
        this.precoR = precoR;
        this.confidencial = confidencial;
        this.curador = curador;
    }

}