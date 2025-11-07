namespace SistemaArte
{
    public class Venda
    {
        private static int contador = 1; // gera IDs automáticos

        public int id;
        public string idObra;
        public string compradorCodigo;
        public double valorFinal;
        public DateTime dataVenda;

        public Venda()
        {
            this.id = contador++;
            this.idObra = "";
            this.compradorCodigo = "";
            this.valorFinal = 0.0;
            this.dataVenda = DateTime.MinValue;
        }

        public Venda(string idObra, string compradorCodigo, double valorFinal, DateTime dataVenda)
        {

            this.idObra = idObra;
            this.compradorCodigo = compradorCodigo;
            this.valorFinal = valorFinal;
            this.dataVenda = dataVenda;
        }
    }
}
