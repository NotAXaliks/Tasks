using System.Collections.ObjectModel;
using System.Linq;
using TasksAPI.Models;

namespace TasksApp.ViewModels;

public record CompanyTreeRoleItem(string Name, bool IsLast = false);

public class CompanyTreeWindowViewModel : ViewModelBase
{
    public ObservableCollection<CompanyTreeRoleItem> Roles { get; } = new();

    public void UpdateRoles(Roles[] roles)
    {
        Roles.Clear();

        for (var i = 0; i < roles.Length; i++)
        {
            Roles.Add(new CompanyTreeRoleItem(roles[i].Name, i == roles.Length - 1));
        }
    }
}