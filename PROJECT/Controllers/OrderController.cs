using Microsoft.AspNetCore.Mvc;
using PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJECT.Controllers
{
    public class OrderController : Controller
    {
        public async Task<IActionResult> View_All_Order ()
        {
            List<OrderModel> theList = await _dbAccountContext.GetAllOrderAsync();
            return View();
        }
    }
}
