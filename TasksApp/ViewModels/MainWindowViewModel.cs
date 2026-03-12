using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using TasksAPI.Models;

namespace TasksApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private SelectedMonth _selectedMonth;
    
    [ObservableProperty] private string _userName = "";
    [ObservableProperty] private string? _userRole;
    [ObservableProperty] private string? _userDepartment;

    public ObservableCollection<SelectedMonth> Months { get; set; } =
    [
        new SelectedMonth(1, "Январь", "января"),
        new SelectedMonth(2, "Февраль", "февраля"),
        new SelectedMonth(3,"Март", "марта"),
        new SelectedMonth(4,"Апрель", "апреля"),
        new SelectedMonth(5,"Май", "мая"),
        new SelectedMonth(6,"Июнь", "июня"),
        new SelectedMonth(7,"Июль", "июля"),
        new SelectedMonth(8,"Август", "августа"),
        new SelectedMonth(9, "Сентябрь", "сентября"),
        new SelectedMonth(10, "Октябрь", "октября"),
        new SelectedMonth(11, "Ноябрь", "ноября"),
        new SelectedMonth(12, "Декабрь", "декабря"),
    ];

    public ObservableCollection<CalendarDay> Days { get; } = [];

    public MainWindowViewModel()
    {
        SelectedMonth = Months[DateTime.Now.Month - 1];
    }

    public void UpdateEvents(Holidays[] calendarData)
    {
        Days.Clear();

        var maxDays = DateTime.DaysInMonth(DateTime.Now.Year, SelectedMonth.Id);
        var firstDay = new DateTime(DateTime.Now.Year, SelectedMonth.Id, 1);
        
        // Заполняем пустые пространства, чтобы каждый день не начинался с понедельника
        var offset = ((int)firstDay.DayOfWeek + 6) % 7;
        for (int j = 0; j < offset; j++)
        {
            Days.Add(new CalendarDay(null, "", []));
        }
        
        var day = 0;

        for (var i = 0; i < 6; i++)
        {
            while (day < maxDays)
            {
                day++;
                
                var events = calendarData
                    .ToList()
                    .Where(d => d.StartDate.Day == day && d.StartDate.Month == SelectedMonth.Id)
                    .Select(d => new CalendarEvent(d.Name))
                    .ToList();

                Days.Add(new CalendarDay(day, SelectedMonth.InText, events));
            }
        }
    }
}

public record SelectedMonth(int Id, string Name, string InText);

public record CalendarEvent(string Name);

public record CalendarDay(int? Day, string Name, ICollection<CalendarEvent> Events)
{
    public bool IsEmpty => Day is null;
    
    public string DisplayDate => $"{Day} {Name}";
};