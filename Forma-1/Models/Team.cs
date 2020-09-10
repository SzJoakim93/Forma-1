using System;

namespace Forma_1.Models
{
    public class Team {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Founded { get; set; }
        public int Wins { get; set; }
        public bool IsPaid { get; set; }
    }
}