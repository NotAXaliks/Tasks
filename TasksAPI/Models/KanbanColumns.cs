using System.ComponentModel.DataAnnotations.Schema;

namespace TasksAPI.Models;

public class KanbanColumns
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }

    public virtual ICollection<Tasks> Tasks { get; set; } = [];
}