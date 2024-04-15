using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models
{
	public class CarrinhoCompra
	{
        private readonly AppDbContext _appDbContext;

        public CarrinhoCompra(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider serviceProvider)
        {
            //Define sessão
            ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            //Obtem um serviço do tipo do contexto
            var context = serviceProvider.GetService<AppDbContext>();
            //Obtem ou gera um Id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();
            //atribui o Id do carrinho a sessao
            session.SetString("CarrinhoId", carrinhoId);

            //retorna o carrinho com o contexto e o Id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _appDbContext.CarrinhoCompraItens.SingleOrDefault(
                 s => s.Lanche.LancheId == lanche.LancheId &&
                 s.CarrinhoCompraId == CarrinhoCompraId);

            if( carrinhoCompraItem == null )
            {
                var item = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };

                _appDbContext.CarrinhoCompraItens.Add(item);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }

            _appDbContext.SaveChanges();
        }

        public int RemoveDoCarinho(Lanche lanche)
        {
            var carrinhoCompraItem = _appDbContext.CarrinhoCompraItens.SingleOrDefault(
                 s => s.Lanche.LancheId == lanche.LancheId &&
				 s.CarrinhoCompraId == CarrinhoCompraId);

            var qtLocal = 0;

            if(carrinhoCompraItem != null)
            {
                if(carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    qtLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _appDbContext.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            _appDbContext.SaveChanges();
            return qtLocal;
		}

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItens ??
                    (CarrinhoCompraItens =
                        _appDbContext.CarrinhoCompraItens
                        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                        .Include(s => s.Lanche)
                        .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _appDbContext.CarrinhoCompraItens
                                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId);

            _appDbContext.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _appDbContext.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _appDbContext.CarrinhoCompraItens
                        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                        .Select(c => c.Lanche.Preco * c.Quantidade)
                        .Sum();

            return total;
        }
    }
}
