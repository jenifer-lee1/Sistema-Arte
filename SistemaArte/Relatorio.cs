using System;

namespace SistemaArte
{
    // DTO para o relatório "Total de Lances por Obra"
    public class LancesPorObra
    {
        public string IdObra { get; set; }
        public string NomeObra { get; set; }
        public int TotalLances { get; set; }

        public LancesPorObra(string idObra, string nomeObra, int totalLances)
        {
            this.IdObra = idObra;
            this.NomeObra = nomeObra;
            this.TotalLances = totalLances;
        }
    }

    // DTO para o relatório "Índice de Obras Vendidas" (Taxa de Sucesso)
    public class TaxaSucesso
    {
        public int TotalObras { get; set; }
        public int ObrasVendidas { get; set; }
        public double TaxaPercentual { get; set; }

        public TaxaSucesso(int totalObras, int obrasVendidas, double taxaPercentual)
        {
            this.TotalObras = totalObras;
            this.ObrasVendidas = obrasVendidas;
            this.TaxaPercentual = taxaPercentual;
        }
    }
}
