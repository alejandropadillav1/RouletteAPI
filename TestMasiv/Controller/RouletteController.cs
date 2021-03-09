using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestMasiv.Interface;
namespace TestMasiv.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class RouletteController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IRouletteServices _rouletteServices;
        public RouletteController(IRouletteServices rouletteServices) { _rouletteServices = rouletteServices; }
        [HttpPost]
        public async Task<IActionResult> NewRoulette()
        {
            var rouletteId = await _rouletteServices.CreateRoulette();
            return Ok(rouletteId);
        }
    }
}
