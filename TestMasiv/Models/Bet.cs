using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace TestMasiv.Models
{
    public class Bet
    {
        [Range(-1, 36)]
        public int Position { get; set; }
        public ColorEnum Color { get; set; }
        [Range(0.1d, maximum: 10000)]
        public double Money { get; set; }
    }
}
