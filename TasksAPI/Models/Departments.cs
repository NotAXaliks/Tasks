using System.ComponentModel.DataAnnotations.Schema;

namespace TasksAPI.Models;

public class Departments
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }

    public virtual ICollection<Users> Users { get; set; } = [];
}