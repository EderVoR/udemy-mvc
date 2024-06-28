using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Areas.Admin.Services
{
	public class RelatorioVendasService
	{
		private readonly AppDbContext _appDbContext;

        public RelatorioVendasService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Pedido>> FinByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resultado = from obj in _appDbContext.Pedidos select obj;

            if(minDate.HasValue)
                resultado = resultado.Where(x => x.PedidoEnviado >= minDate.Value);
            if(maxDate.HasValue)
                resultado = resultado.Where(x => x.PedidoEnviado <= maxDate.Value);

            return await resultado
                .Include(l => l.PedidoItens)
                .ThenInclude(l => l.Lanche)
                .OrderByDescending(x => x.PedidoEnviado)
                .ToListAsync();
        }
    }
}
