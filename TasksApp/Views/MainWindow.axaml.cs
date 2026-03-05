using Avalonia.Controls;
using Avalonia.Interactivity;
using TasksApp.ViewModels;
using TasksAPI.Models;
using TasksApp.Services;

namespace TasksApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel();

        RenderUserData();
        RenderCalendar();
    }

    public async void RenderUserData()
    {
        if (DataContext is MainWindowViewModel vm)
        {
            var user = await ApiService.Get<Users>("/users");

            vm.UserName = user.Name;
            vm.UserRole = user.Role.Name;
            vm.UserDepartment = user.Department.Name;
        }
    }

    public async void RenderCalendar()
    {
        if (DataContext is MainWindowViewModel vm)
        {
            var calendarData = await ApiService.Get<Holidays[]>("/holidays");

            vm.UpdateEvents(calendarData);
        }
    }

    private async void OnSelectedMonth(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            var calendarData = await ApiService.Get<Holidays[]>("/holidays");

            vm.UpdateEvents(calendarData);
        }
    }

    private void OnProfileEdit(object? sender, RoutedEventArgs e)
    {
        Window editProfileWindow = new EditProfileWindow();

        editProfileWindow.Show();
    }

    private void ShowGanttChart(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void ShowKanbanBoard(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void ShowCompanyTree(object? sender, RoutedEventArgs e)
    {
        Window companyTreeWindow = new CompanyTreeWindow();

        companyTreeWindow.Show();
    }
}