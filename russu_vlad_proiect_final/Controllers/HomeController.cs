using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using russu_vlad_proiect_final.Data;
using russu_vlad_proiect_final.Models;
using russu_vlad_proiect_final.Models.RecordStoreViewModels;

namespace russu_vlad_proiect_final.Controllers
{
    public class HomeController : Controller
    {
        private readonly RecordStoreContext _context;

        public HomeController(RecordStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> Statistics()
        {
            IQueryable<OrderGroup> data =
                from order in _context.Orders
                group order by order.OrderDate into dateGroup
                select new OrderGroup()
                {
                    OrderDate = dateGroup.Key,
                    AlbumCount = dateGroup.Count()
                };

            return View(await data.AsNoTracking().ToListAsync());
        }
    }
}
