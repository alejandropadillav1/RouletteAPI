using System;
using System.Collections.Generic;
using System.Linq;
namespace TestMasiv.Models
{
    public class Roulette
    {
        public string Id { get; set; }
        public bool IsOpen { get => OpenAt != null ? true : false; }
        public bool IsClosed { get => ClosedAt != null ? true : false; }
        public DateTime? OpenAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public List<BetUsers> ListUsers { get; set; } = new List<BetUsers>();
    }
}
