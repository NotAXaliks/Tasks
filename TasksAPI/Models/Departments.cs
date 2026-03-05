using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TasksAPI.Models;

public class Departments
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<Users> Users { get; set; } = [];
}