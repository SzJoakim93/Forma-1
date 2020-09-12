using System;

namespace Model
{
    public class Team {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Founded { get; set; }
        public int Wins { get; set; }
        public bool IsPaid { get; set; }
    }
}