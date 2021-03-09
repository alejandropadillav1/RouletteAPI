using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestMasiv.Extensions;
using TestMasiv.Interface;
using TestMasiv.Models;
namespace TestMasiv.Services
{
    public class RouletteRedisServices : IRouletteServices
    {
        private readonly IDistributedCache _cache;
        private readonly string RecordKey = "TABLEROULETTE";
        public RouletteRedisServices(IDistributedCache cache) { _cache = cache; }
        public Task<bool> BetRoulette(int IdUser, int idRoulette, Bet betRequest, CancellationTokenSource token = null)
        { throw new NotImplementedException(); }
        public IAsyncEnumerable<BetUsers> CloseRoulette(int Id) { throw new NotImplementedException(); }
        public async Task<string> CreateRoulette(CancellationTokenSource token = null)
        {
            Roulette roulette = new Roulette
            {
                Id = Guid.NewGuid().ToString(),
                IsOpen = false,
                OpenAt = null,
                ClosedAt = null
            };
            await _cache.SetRecordAsync<Roulette>(RecordKey, roulette);
            return await Task.FromResult(roulette.Id);
        }
        public IAsyncEnumerable<Roulette> GetAllRouletteAsync(CancellationTokenSource token = null)
        { throw new NotImplementedException(); }
        public Task<bool> OpenRoulette(int Id, CancellationTokenSource token = null)
        { throw new NotImplementedException(); }
    }
}
