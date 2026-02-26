using System.ComponentModel.DataAnnotations.Schema;

namespace TasksAPI.Models;

public partial class Users
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Avatar { get; set; }
    
    public int DepartmentId { get; set; }
    [ForeignKey(nameof(DepartmentId))] public Departments Department { get; set; }
    
    public int RoleId { get; set; }
    [ForeignKey(nameof(RoleId))] public Roles Role { get; set; }
}
