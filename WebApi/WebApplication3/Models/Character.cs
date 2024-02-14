namespace SoSyaGeServer.Model
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int AttackPower { get; set; }
        public int DefensePower { get; set; }

        public int PlayerId { get; set; } // Foreign Key
        public Player Player { get; set; } // Navigation Property

        // Navigation Properties
        public ICollection<Item> Items { get; set; }
    }
}
