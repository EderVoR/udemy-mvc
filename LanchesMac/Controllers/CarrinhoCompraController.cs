using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
	public class CarrinhoCompraController : Controller
	{
		private readonly ILancheRepository _lancheRepository;
		private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, 
										CarrinhoCompra carrinhoCompra)
        {
			_carrinhoCompra = carrinhoCompra;
			_lancheRepository = lancheRepository;
        }

        public IActionResult Index()
		{
			var itens = _carrinhoCompra.GetCarrinhoCompraItens();
			_carrinhoCompra.CarrinhoCompraItens = itens;

			var CarrinhoCompraVM = new CarrinhoCompraViewModel
			{
				CarrinhoCompra = _carrinhoCompra,
				CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal(),
			};

			return View(CarrinhoCompraVM);
		}

		[Authorize]
		public IActionResult AdicionarItemNoCarrinhoCompra(int lancheId)
		{
			var lancheSelecionado = _lancheRepository.Lanches
									.FirstOrDefault(l => l.LancheId == lancheId);

			if(lancheSelecionado != null)
			{
				_carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
			}

			return RedirectToAction("Index");
		}

		[Authorize]
		public IActionResult RemoverItemDoCarrinhoCompra(int lancheId)
		{
			var lancheSelecionado = _lancheRepository.Lanches
									.FirstOrDefault(l => l.LancheId == lancheId);

			if(lancheSelecionado != null)
			{
				_carrinhoCompra.RemoveDoCarinho(lancheSelecionado);
			}

			return RedirectToAction("Index");
		}
	}
}
