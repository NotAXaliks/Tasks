using System.Collections.ObjectModel;
using Avalonia.Controls;
using TasksAPI.Models;
using TasksApp.Services;
using TasksApp.ViewModels;

namespace TasksApp.Views;

public record CompanyTreeRoleItem(string Name, bool IsLast = false);

public partial class CompanyTreeWindow : Window
{
    public ObservableCollection<CompanyTreeRoleItem> Roles { get; } = new();
    
    public CompanyTreeWindow()
    {
        InitializeComponent();

        DataContext = this;

        UpdateData();
    }

    public async void UpdateData()
    {
            var data = await ApiService.Get<UsersData>("/users/data");
            
            Roles.Clear();

            for (var i = 0; i < data.Roles.Length; i++)
            {
                Roles.Add(new CompanyTreeRoleItem(data.Roles[i].Name, i == data.Roles.Length - 1));
            }
    }
}