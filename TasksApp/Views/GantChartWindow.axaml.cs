using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using TasksAPI.Models;
using TasksApp.Services;

namespace TasksApp.Views;

public partial class GantChartWindow : Window
{
    public GantChartWindow()
    {
        InitializeComponent();
        
        Update();
    }

    public async void Update()
    {
        DiagramGrid.Children.Clear();
        DiagramGrid.RowDefinitions.Clear();
        DiagramGrid.ColumnDefinitions.Clear();

        var data = await ApiService.Get<Tasks[]>("/tasks");

        var tasks = data
            .Where(t => t.EndDate >= DateTimeOffset.Now)
            .OrderBy(t => t.StartDate)
            .ToList();

        DiagramGrid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(200)));
        DiagramGrid.RowDefinitions.Add(new RowDefinition(new GridLength(40)));

        // 30 дней
        for (int i = 0; i < 30; i++)
            DiagramGrid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(40)));
        
        // Сетка
        for (var row = 0; row <= tasks.Count; row++)
        {
            for (var col = 0; col <= 30; col++)
            {
                var cell = new Border
                {
                    BorderBrush = Brushes.LightGray,
                    BorderThickness = new Thickness(0, 0, 1, 1)
                };

                Grid.SetRow(cell, row);
                Grid.SetColumn(cell, col);

                DiagramGrid.Children.Add(cell);
            }
        }

        // Заголовок
        var header = new TextBlock
        {
            Text = "Задача",
            FontSize = 20,
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            Margin = new Thickness(5)
        };

        Grid.SetRow(header, 0);
        Grid.SetColumn(header, 0);

        DiagramGrid.Children.Add(header);

        // Даты
        for (var i = 0; i < 30; i++)
        {
            var date = DateTimeOffset.Now.Date.AddDays(i);

            var dateBorder = new Border()
            {
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(0, 0, 1, 1),
                Child = new TextBlock()
                {
                    Text = date.Day.ToString(),
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
                }
            };

            Grid.SetRow(dateBorder, 0);
            Grid.SetColumn(dateBorder, i + 1);

            DiagramGrid.Children.Add(dateBorder);
        }

        var rowIndex = 0;

        foreach (var task in tasks)
        {
            rowIndex++;

            DiagramGrid.RowDefinitions.Add(new RowDefinition(new GridLength(40)));

            // Название задачи
            var nameBorder = new Border
            {
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(0, 0, 1, 1),
                Child = new TextBlock
                {
                    Text = task.Name,
                    Margin = new Thickness(5),
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
                }
            };

            Grid.SetRow(nameBorder, rowIndex);
            Grid.SetColumn(nameBorder, 0);

            DiagramGrid.Children.Add(nameBorder);

            var duration = (task.EndDate - task.StartDate).Days + 1;
            var startIndex = 0;
            if (task.StartDate > DateTimeOffset.Now)
                startIndex = (task.StartDate - DateTimeOffset.Now).Days;

            // Бар
            var taskBar = new Border
            {
                Background = Brushes.DodgerBlue,
                CornerRadius = new CornerRadius(2),
                Margin = new Thickness(1),
                Height = 20
            };

            Grid.SetRow(taskBar, rowIndex);
            Grid.SetColumn(taskBar, startIndex + 1);
            Grid.SetColumnSpan(taskBar, duration);

            DiagramGrid.Children.Add(taskBar);
        }
    }
}