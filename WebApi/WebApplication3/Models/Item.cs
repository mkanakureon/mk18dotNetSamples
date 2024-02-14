namespace SoSyaGeServer.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // e.g. Weapon, Armor, Potion
        public int Power { get; set; }

        public int CharacterId { get; set; } // Foreign Key
        public Character Character { get; set; } // Navigation Property
    }
}
