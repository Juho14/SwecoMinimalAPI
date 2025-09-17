using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class ProjectEntity
    {
        [Key]
        public long Id { get; set; }

        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ProjectManager { get; set; }
        public required string Client { get; set; }
        public List<string> Options { get; set; } = new List<string>();
    }
}
