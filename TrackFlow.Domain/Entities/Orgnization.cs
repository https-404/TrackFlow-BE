namespace TrackFlow.Domain.Entities;


    public class Organization : BaseEntity
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Description { get; set; }

        public Guid CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public ICollection<UserOrganization> UserOrganizations { get; set; } = new List<UserOrganization>();
    }

