using System;

namespace AvaritiaBot.Models
{
    public class Character
    {
        public long DbId { get; set; }
        public ulong OwnerID { get; set; }
        public string Name { get; set; }
        public int Ap { get; set; }
        public int AAp { get; set; }
        public int Dp { get; set; }
        public int Level { get; set; }
        public string Url { get; set; }
        public uint Class { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
