using Avalonia.Controls;
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
        
        RenderCalendar();
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
}
