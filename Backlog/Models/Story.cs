using System.ComponentModel.DataAnnotations.Schema;

namespace Backlog.Models
{
    public class Story
    {
        public int Id { get; set; }
        [ForeignKey("Epic")]
        public int? EpicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public int? Priority { get; set; }
        public Epic Epic { get; set; }
    }
}
