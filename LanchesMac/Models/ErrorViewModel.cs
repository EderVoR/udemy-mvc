namespace LanchesMac.Models
{
    public class ErrorViewModel
    {
#pragma warning disable CS8632 // A anota��o para tipos de refer�ncia anul�veis deve ser usada apenas em c�digo em um contexto de anota��es '#nullable'.
        public string? RequestId { get; set; }
#pragma warning restore CS8632 // A anota��o para tipos de refer�ncia anul�veis deve ser usada apenas em c�digo em um contexto de anota��es '#nullable'.

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}