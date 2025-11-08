using System;

namespace SistemaArte
{
    public class Avaliar
    {
        // referente à obra (pode ser nula se a avaliação foi criada apenas com id)
        public Obra? obra;

        // backup do id da obra (usado quando a instância não tem a referência 'obra')
        private string idObraBackup;

        public string idCurador;
        public double precoReserva;
        public string observacoes;
        public DateTime dataAvaliacao;

        public string idObra
        {
            get => obra?.idObra ?? idObraBackup ?? "";
            set
            {

                idObraBackup = value ?? "";
            }
        }
        public Avaliar(Obra obra, string idCurador, double precoReserva, string observacoes, DateTime dataAvaliacao)
        {
            this.obra = obra ?? throw new ArgumentNullException(nameof(obra));
            this.idObraBackup = obra.idObra ?? "";
            this.idCurador = idCurador ?? "";
            this.precoReserva = precoReserva;
            this.observacoes = observacoes ?? "";
            this.dataAvaliacao = dataAvaliacao;
        }

        public Avaliar(string idObra, string idCurador, double precoReserva, string observacoes, DateTime dataAvaliacao)
        {
            this.obra = null;
            this.idObraBackup = idObra ?? "";
            this.idCurador = idCurador ?? "";
            this.precoReserva = precoReserva;
            this.observacoes = observacoes ?? "";
            this.dataAvaliacao = dataAvaliacao;
        }

        // Construtor 
        public Avaliar()
        {
            this.obra = null;
            this.idObraBackup = "";
            this.idCurador = "";
            this.precoReserva = 0.0;
            this.observacoes = "";
            this.dataAvaliacao = DateTime.MinValue;
        }
    }
}
