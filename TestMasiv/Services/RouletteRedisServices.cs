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
        private string RecordKey = "TABLEROULETTE";
        private List<Roulette> listRoulette;
        public RouletteRedisServices(IDistributedCache cache)
        {
            _cache = cache;
            RecordKey = string.Format("{0}-{1}", RecordKey, DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ssZ"));
            listRoulette = new List<Roulette>();
        }
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
            listRoulette.Add(roulette);
            await _cache.SetRecordAsync(RecordKey, listRoulette);
            return await Task.FromResult(roulette.Id);
        }
        public async IAsyncEnumerable<Roulette> GetAllRouletteAsync(CancellationTokenSource token = null)
        {
            listRoulette = await _cache.GetRecordAsync<List<Roulette>>(RecordKey);
            foreach(var roulette in listRoulette)
            {
                yield return roulette;
            }
        }
        public Task<bool> OpenRoulette(int Id, CancellationTokenSource token = null)
        { throw new NotImplementedException(); }
    }
}