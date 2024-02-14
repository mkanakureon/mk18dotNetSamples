namespace SoSyaGeServer.Model
{
    public class Achievement
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Goal { get; set; }
        public Guid RewardId { get; set; }

        public virtual Reward Reward { get; set; }
    }
}
