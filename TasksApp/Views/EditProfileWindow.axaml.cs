using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using TasksAPI.Models;
using TasksApp.Services;
using TasksApp.ViewModels;

namespace TasksApp.Views;

public record UsersData(Roles[] Roles, Departments[] Departments);

public partial class EditProfileWindow : Window
{
    public EditProfileWindow()
    {
        InitializeComponent();

        DataContext = new EditProfileWindowViewModel();

        UpdateData();
    }

    public async void UpdateData()
    {
        if (DataContext is EditProfileWindowViewModel vm)
        {
            var data = await ApiService.Get<UsersData>("/users/data");
            var user = await ApiService.Get<Users>("/users");
            
            vm.UpdateData(user, data.Roles, data.Departments);
        }
    }

    private async void UserSaved(object? sender, RoutedEventArgs e)
    {
        if (DataContext is EditProfileWindowViewModel vm)
        {
            await ApiService.Patch<Users>("/users", new { RoleId = vm.UserRole.Id, DepartmentId = vm.UserDepartment.Id, Name = vm.UserName });
        }

        Close();
    }
}