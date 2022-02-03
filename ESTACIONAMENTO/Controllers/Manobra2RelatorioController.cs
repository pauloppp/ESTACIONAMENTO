using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ESTACIONAMENTO.Dados;
using Rotativa;
using Rotativa.AspNetCore;

namespace ESTACIONAMENTO.Controllers
{
    public class Manobra2RelatorioController : Controller
    {
        private readonly EstacionamentoContext _context;

        public Manobra2RelatorioController(EstacionamentoContext context)
        {
            _context = context;
        }

        // GET: Manobra2/ReciboP/5
        public async Task<ViewAsPdf> ReciboP(int? id)
        {
            var manobra2 = await _context.Manobras2.FindAsync(id);
            var reciboPDF = new ViewAsPdf()
            {
                ViewName = "ReciboP",
                IsGrayScale = true,
                Model = manobra2
            };
            return reciboPDF;
        }

    }
}
