using SoSyaGeServer.Db;

namespace SoSyaGeServer.Model
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CurrentStage { get; set; }
        public int GameCurrency { get; set; }
        public int PremiumCurrency { get; set; }
        public DateTime LastLoginDate { get; set; }

        // Navigation Properties
        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<PlayerReward> PlayerRewards { get; set; }
    }
}
