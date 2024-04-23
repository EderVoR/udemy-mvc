using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
	public class Pedido
	{
        public int PedidoId { get; set; }
        [Required(ErrorMessage = "Informe o nome")]
        [StringLength(50)]
        public string Nome { get; set; }
		[Required(ErrorMessage = "Informe o sobrenome")]
		[StringLength(50)]
		public string Sobrenome { get; set; }
		[Required(ErrorMessage = "Informe o endereço")]
		[StringLength(50)]
		public string Endereco1 { get; set; }
		[Required(ErrorMessage = "Informe o complemento")]
		[StringLength(50)]
		public string Endereco2 { get; set; }
		[Required(ErrorMessage = "Informe seu CEP")]
		[StringLength(10, MinimumLength = 8)]
		[Display(Name = "CEP")]
		public string Cep { get; set; }
		[StringLength(10)]
        public string Estado { get; set; }
		[StringLength(50)]
        public string Cidade { get; set; }
		[Required(ErrorMessage = "Informe o telefone")]
		[StringLength(25)]
		[DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }
		[Required(ErrorMessage = "Informe o email")]
		[StringLength(50)]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[ScaffoldColumn(false)]
		[Column(TypeName = "decimal(18,2)")]
		[Display(Name = "Total do pedido")]
        public decimal PedidoTotal { get; set; }
		[ScaffoldColumn(false)]
		[Display(Name = "Itens do Pedido")]
		public int TotalItensPedido { get; set; }
		[DisplayName("Data do Pedido")]
		[DataType(DataType.Text)]
		[DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm", ApplyFormatInEditMode = true)]
        public DateTime PedidoEnviado { get; set; }
		[DisplayName("Data de Entrega")]
		[DataType(DataType.Text)]
		[DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm", ApplyFormatInEditMode = true)]
		public DateTime? PedidoEntregueEm { get; set; }

        public List<PedidoDetalhe> PedidoItens { get; set; }
    }
}
