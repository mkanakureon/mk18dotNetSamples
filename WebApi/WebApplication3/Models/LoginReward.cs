namespace SoSyaGeServer.Model
{
    public class LoginReward
    {
        public Guid Id { get; set; }
        public int DayCount { get; set; }
        public Guid RewardId { get; set; }

        public virtual Reward Reward { get; set; }
    }
}
