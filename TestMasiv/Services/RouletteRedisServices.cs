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
        public async Task<bool> BetRoulette(int IdUser, string idRoulette, Bet betRequest, CancellationTokenSource token = null)
        {
            var roulette = listRoulette.FirstOrDefault(x => x.Id.Equals(idRoulette));
            if(roulette == null)
            {
                throw new Exception("Roulette Not Found");
            }
            var betUser = roulette.ListUsers.FirstOrDefault(x => x.Id.Equals(IdUser));
            if(betUser == null)
            {
                betUser = new BetUsers
                {
                    Id = IdUser,
                };
                roulette.ListUsers.Add(betUser);
            }
            var bet = new Bet
            {
                BetValue = betRequest.BetValue,
                Position = betRequest.Position,
                Color = betRequest.Color,
            };
            betUser.Bets.Add(bet);
            await SaveChangesAsync(listRoulette);
            return true;
        }
        public async IAsyncEnumerable<BetUsers> CloseRoulette(string Id)
        {
            var roulette = listRoulette.FirstOrDefault(x => x.Id.Equals(Id));
            if(roulette == null)
            {
                throw new Exception("Roulette Not Found");
            }
            if(roulette.IsOpen)
            {
                if(!roulette.IsClosed)
                {
                    roulette.ClosedAt = DateTime.Now;
                    CalculatePrize(roulette);
                    await SaveChangesAsync(listRoulette);
                    foreach(var betUser in roulette.ListUsers)
                    {
                        yield return betUser;
                    }
                }
                else
                {
                    foreach(var betUser in roulette.ListUsers)
                    {
                        yield return betUser;
                    }
                }
            }
            else
            {
                throw new Exception("Roulette is not opened, please open first before close");
            }
        }
        private void CalculatePrize(Roulette roulette)
        {
            var winNumber = new Random().Next(0, 36);
            var winColor = winNumber % 2 == 0 ? ColorEnum.Red : ColorEnum.Black;
            foreach(var betUser in roulette.ListUsers)
            {
                var sumPrize = 0d;
                foreach(var bet in betUser.Bets)
                {
                    if(bet.Position == winNumber)
                        sumPrize += bet.BetValue * 5;
                    if(bet.Position == -1 && bet.Color == winColor)
                        sumPrize += bet.BetValue * 1.8;
                }
                betUser.WinMoney = sumPrize;
            }
        }
        public async Task<string> CreateRoulette(CancellationTokenSource token = null)
        {
            Roulette roulette = new Roulette
            {
                Id = Guid.NewGuid().ToString(),
                OpenAt = null,
                ClosedAt = null
            };
            listRoulette.Add(roulette);
            await SaveChangesAsync(listRoulette);
            return roulette.Id;
        }
        public async IAsyncEnumerable<Roulette> GetAllRouletteAsync(CancellationTokenSource token = null)
        {
            listRoulette = await _cache.GetRecordAsync<List<Roulette>>(RecordKey);
            foreach(var roulette in listRoulette)
            {
                yield return roulette;
            }
        }
        public async Task<bool> OpenRoulette(string Id, CancellationTokenSource token = null)
        {
            var roulette = listRoulette.FirstOrDefault(x => x.Id.Equals(Id));
            if(roulette == null)
            {
                throw new Exception("Roulette Not Found");
            }
            if(!roulette.IsOpen)
            {
                roulette.OpenAt = DateTime.Now;
                await SaveChangesAsync(listRoulette);
                return true;
            }
            if(roulette.IsClosed)
            {
                throw new Exception("Roulette has been closed");
            }
            else
            {
                throw new Exception("Roulette is open");
            }
        }
        private async Task SaveChangesAsync<T>(List<T> list) { await _cache.SetRecordAsync(RecordKey, list); }
    }
}