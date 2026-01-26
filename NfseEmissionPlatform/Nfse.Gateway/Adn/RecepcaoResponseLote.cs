namespace Nfse.Gateway.Adn
{
    public sealed class RecepcaoResponseLote
    {
        public List<RecepcaoResponseDocumento>? Lote {  get; set; }
        public string? TipoAmbiente { get; set; }
        public string? VersaoAplicativo { get; set; }
        public DateTime? DataHoraProcessamento { get; set; }
    }

    public sealed class RecepcaoResponseDocumento
    {
        public string? ChaveAcesso { get; set; }
        public string? NsuRecepcao { get; set; }
        public string? StatusProcessamento { get; set; }
        public List<RecepcaoMensagem>? Alertas { get; set; }
        public List<RecepcaoMensagem>? Erros {  get; set; }
    }

    public sealed class RecepcaoMensagem
    {
        public string? Codigo { get; set; }
        public string? Descricao { get; set; }
        public string? Complemento { get; set; }
    }
}
