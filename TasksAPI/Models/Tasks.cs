using System.ComponentModel.DataAnnotations.Schema;

namespace TasksAPI.Models;

public class Tasks
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public DateTimeOffset StartDate { get; set; }
    
    public DateTimeOffset EndDate { get; set; }
    
    public int? KanbanColumnId { get; set; }
    [ForeignKey(nameof(KanbanColumnId))] public KanbanColumns? KanbanColumn { get; set; }
}