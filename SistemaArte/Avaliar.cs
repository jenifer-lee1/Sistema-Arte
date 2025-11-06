using System;

namespace SistemaArte
{
    public class Avaliar
    {
        // referência à obra (pode ser nula se a avaliação foi criada apenas com id)
        public Obra? obra;

        // backup do id da obra (usado quando a instância não tem a referência 'obra')
        private string idObraBackup;

        public string idCurador;
        public double precoReserva;
        public string observacoes;
        public DateTime dataAvaliacao;

        // Propriedade compatível para o código que ainda acessa a.idObra
        public string idObra
        {
            get => obra?.idObra ?? idObraBackup ?? "";
            set
            {
                // atualiza o backup (não cria uma Obra nova)
                idObraBackup = value ?? "";
            }
        }

        // Construtor principal por referência (recomendado)
        public Avaliar(Obra obra, string idCurador, double precoReserva, string observacoes, DateTime dataAvaliacao)
        {
            this.obra = obra ?? throw new ArgumentNullException(nameof(obra));
            this.idObraBackup = obra.idObra ?? "";
            this.idCurador = idCurador ?? "";
            this.precoReserva = precoReserva;
            this.observacoes = observacoes ?? "";
            this.dataAvaliacao = dataAvaliacao;
        }

        // Construtor compatível (recebe apenas id da obra) — mantém compatibilidade com código antigo
        public Avaliar(string idObra, string idCurador, double precoReserva, string observacoes, DateTime dataAvaliacao)
        {
            this.obra = null;
            this.idObraBackup = idObra ?? "";
            this.idCurador = idCurador ?? "";
            this.precoReserva = precoReserva;
            this.observacoes = observacoes ?? "";
            this.dataAvaliacao = dataAvaliacao;
        }

        // Construtor padrão
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
