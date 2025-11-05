using System;

// CLASSE 1: Avaliar (Preço de Reserva - Curador)
public class Avaliar
{
    public int id;
    public string idObra;
    public string idCurador;
    public double precoReserva;
    public string observacoes;
    public DateTime dataAvaliacao;

    public Avaliar(int id, string idObra, string idCurador, double precoReserva, string observacoes, DateTime dataAvaliacao)
    {
        this.id = id;
        this.idObra = idObra;
        this.idCurador = idCurador;
        this.precoReserva = precoReserva;
        this.observacoes = observacoes;
        this.dataAvaliacao = dataAvaliacao;
    }

    public Avaliar()
    {
        this.id = 0;
        this.idObra = "";
        this.idCurador = "";
        this.precoReserva = 0.0;
        this.observacoes = "";
        this.dataAvaliacao = DateTime.MinValue;
    }
}

// CLASSE 2: AvaliacaoEstrelas (Avaliação Pública) - Adaptada para campos públicos
public class AvaliacaoEstrelas
{
    public int id;
    public string idObra;
    public string idUsuario;
    public int notaEstrelas; // De 1 a 5
    public string comentario;
    public DateTime dataAvaliacao;

    public AvaliacaoEstrelas() { }

    public AvaliacaoEstrelas(int id, string idObra, string idUsuario, int notaEstrelas, string comentario, DateTime dataAvaliacao)
    {
        this.id = id;
        this.idObra = idObra;
        this.idUsuario = idUsuario;
        this.notaEstrelas = notaEstrelas;
        this.comentario = comentario;
        this.dataAvaliacao = dataAvaliacao;
    }
}