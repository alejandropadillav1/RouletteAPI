using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMasiv.Models;
namespace TestMasiv.Interface
{
    public interface IRouletteServices
    {
        public Task<string> CreateRoulette(System.Threading.CancellationTokenSource token = null);
        public Task<bool> OpenRoulette(string Id, System.Threading.CancellationTokenSource token = null);
        public Task<bool> BetRoulette(int IdUser, string idRoulette, Bet betRequest, System.Threading.CancellationTokenSource token = null);
        public IAsyncEnumerable<BetUsers> CloseRoulette(string Id);
        public IAsyncEnumerable<Roulette> GetAllRouletteAsync(System.Threading.CancellationTokenSource token = null);
    }
}
