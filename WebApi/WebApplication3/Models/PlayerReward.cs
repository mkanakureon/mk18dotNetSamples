namespace SoSyaGeServer.Model
{
    public class PlayerReward
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public Guid RewardId { get; set; }
        public DateTime ReceivedDate { get; set; }

        public virtual Player Player { get; set; }
        public virtual Reward Reward { get; set; }
    }
}
