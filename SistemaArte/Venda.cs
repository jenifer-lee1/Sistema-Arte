using System;

namespace SistemaArte
{
    public class Venda
    {
        public int id;
        public string idObra;
        public string compradorCodigo;
        public double valorFinal;
        public DateTime dataVenda;

        public Venda()
        {
            this.id = 0;
            this.idObra = "";
            this.compradorCodigo = "";
            this.valorFinal = 0.0;
            this.dataVenda = DateTime.MinValue;
        }

        public Venda(int id, string idObra, string compradorCodigo, double valorFinal, DateTime dataVenda)
        {
            this.id = id;
            this.idObra = idObra;
            this.compradorCodigo = compradorCodigo;
            this.valorFinal = valorFinal;
            this.dataVenda = dataVenda;
        }
    }
}
