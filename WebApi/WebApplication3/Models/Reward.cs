using SoSyaGeServer.Db;

namespace SoSyaGeServer.Model
{
    public class Reward
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PlayerReward> PlayerRewards { get; set; }
        public virtual ICollection<LoginReward> LoginRewards { get; set; }
        public virtual ICollection<Achievement> Achievements { get; set; }
    }
}
