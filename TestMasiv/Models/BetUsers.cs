using System;
using System.Collections.Generic;
using System.Linq;
namespace TestMasiv.Models
{
    public class BetUsers
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public List<Bet> Bets { get; set; }
    }
}
