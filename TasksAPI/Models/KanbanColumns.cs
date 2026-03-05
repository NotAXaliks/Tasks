using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TasksAPI.Models;

public class KanbanColumns
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<Tasks> Tasks { get; set; } = [];
}