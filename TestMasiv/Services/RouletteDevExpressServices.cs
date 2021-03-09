using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestMasiv.Interface;
using TestMasiv.Models;
namespace TestMasiv.Services
{
    public class RouletteDevExpressServices : IRouletteServices
    {
        public Task<bool> BetRoulette(int IdUser, string idRoulette, Bet betRequest, CancellationTokenSource token = null)
        { throw new NotImplementedException(); }
        public IAsyncEnumerable<BetUsers> CloseRoulette(string Id) { throw new NotImplementedException(); }
        public Task<string> CreateRoulette(CancellationTokenSource token = null)
        { throw new NotImplementedException(); }
        public IAsyncEnumerable<Roulette> GetAllRouletteAsync(CancellationTokenSource token = null)
        { throw new NotImplementedException(); }
        public Task<bool> OpenRoulette(string Id, CancellationTokenSource token = null)
        { throw new NotImplementedException(); }
    }
}
