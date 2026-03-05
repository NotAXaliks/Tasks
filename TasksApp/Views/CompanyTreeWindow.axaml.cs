using Avalonia.Controls;
using TasksAPI.Models;
using TasksApp.Services;
using TasksApp.ViewModels;

namespace TasksApp.Views;

public partial class CompanyTreeWindow : Window
{
    public CompanyTreeWindow()
    {
        InitializeComponent();

        DataContext = new CompanyTreeWindowViewModel();

        UpdateData();
    }

    public async void UpdateData()
    {
        if (DataContext is CompanyTreeWindowViewModel vm)
        {
            var data = await ApiService.Get<UsersData>("/users/data");
            
            vm.UpdateRoles(data.Roles);
        }
    }
}