using static <%= solutionName %>.Core.Helpers.Enum;

namespace <%= solutionName %>.Core.Entities
{
    public class Job : BaseEntity<long>
    {
        public string HashId { get; set; }

        public JobState State { get; set; }

        public int PeerId { get; set; }

        public PeerType PeerType { get; set; }

        public int MaxId { get; set; }
    }
}