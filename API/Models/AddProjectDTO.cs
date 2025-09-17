using System.Collections.Generic;

namespace Models
{
	public class AddProjectDTO
	{
		public required string Name { get; set; }
		public required string Description { get; set; }
		public required string ProjectManager { get; set; }
		public required string Client { get; set; }
		public List<string> Options { get; set; } = new List<string>();
	}
}
