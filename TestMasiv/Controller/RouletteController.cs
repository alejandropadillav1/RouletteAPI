using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMasiv.Interface;
using TestMasiv.Models;
namespace TestMasiv.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class RouletteController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IRouletteServices _rouletteServices;
        public RouletteController(IRouletteServices rouletteServices) { _rouletteServices = rouletteServices; }
        [HttpPost]
        public async Task<IActionResult> NewRouletteAsync()
        {
            var rouletteId = await _rouletteServices.CreateRoulette();
            return Ok(rouletteId);
        }
        [HttpGet]
        public async IAsyncEnumerable<Roulette> GetAllRouletteAsync()
        {
            await foreach(var roulette in _rouletteServices.GetAllRouletteAsync())
            {
                yield return roulette;
            }
        }
        [HttpPut]
        public async Task<IActionResult> OpenRouletteAsync(string Id)
        {
            var status = await _rouletteServices.OpenRoulette(Id);
            return Ok(status);
        }
        [HttpPut]
        public async IAsyncEnumerable<BetUsers> CloseRouletteAsync(string Id)
        {
            await foreach(var roulette in _rouletteServices.CloseRoulette(Id))
            {
                yield return roulette;
            }
        }
    }
}
