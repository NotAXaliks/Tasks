using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using TasksAPI.Models;
using TasksApp.Services;
using TasksApp.ViewModels;

namespace TasksApp.Views;

public record KanbanColumn(KanbanColumns Column, ObservableCollection<Tasks> Children);

public partial class KanbanWindow : Window
{
    public ObservableCollection<KanbanColumn> Columns { get; set; } = [];
    
    public KanbanWindow()
    {
        InitializeComponent();

        ColumnsControl.ItemsSource = Columns;
        DataContext = this;

        Update();
    }

    public async void Update()
    {
        var data = await ApiService.Get<Tasks[]>("/kanban/tasks");
        
        foreach (var task in data)
        {
            if (task.KanbanColumn == null) return;
            
            var column = Columns.FirstOrDefault(k => k.Column.Id == task.KanbanColumnId);
            if (column == null)
            {
                column = new KanbanColumn(task.KanbanColumn!, [task]);
                Columns.Add(column);
            }
            else
            {
                column.Children.Add(task);
            }
        }
    }

    private async void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is not Border border) return;
        if (border.DataContext is not Tasks task) return;

        var data = new DataObject();
        data.Set("task", task);

        await DragDrop.DoDragDrop(e, data, DragDropEffects.Move);
    }

    private async void Column_Drop(object? sender, DragEventArgs e)
    {
        if (!e.Data.Contains("task")) return;

        var task = e.Data.Get("task") as Tasks;
        if (task == null) return;

        if (sender is Border border && border.DataContext is KanbanColumn targetColumn)
        {
            var sourceColumn = Columns.ToList().FirstOrDefault(c => c.Children.Contains(task));
            sourceColumn?.Children.Remove(task);

            targetColumn.Children.Add(task);

            task.KanbanColumnId = targetColumn.Column.Id;

            await ApiService.Post<Tasks>($"/kanban/move/{task.Id}/{task.KanbanColumnId}");
        }
    }
}