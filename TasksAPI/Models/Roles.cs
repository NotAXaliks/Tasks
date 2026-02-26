using System.ComponentModel.DataAnnotations.Schema;

namespace TasksAPI.Models;

public class Roles
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int Position { get; set; }

    public virtual ICollection<Users> Users { get; set; } = [];
}