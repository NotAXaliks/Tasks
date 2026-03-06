using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using TasksAPI.Models;
using TasksApp.Services;
using TasksApp.ViewModels;

namespace TasksApp.Views;

public partial class KanbanWindow : Window
{
    public KanbanWindow()
    {
        InitializeComponent();

        DataContext = new KanbanWindowViewModel();

        Update();
    }

    public async void Update()
    {
        if (DataContext is not KanbanWindowViewModel vm) return;

        var data = await ApiService.Get<Tasks[]>("/kanban/tasks");

        vm.Update(data);
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
        if (DataContext is not KanbanWindowViewModel vm) return;

        if (!e.Data.Contains("task")) return;

        var task = e.Data.Get("task") as Tasks;
        if (task == null) return;

        if (sender is Border border && border.DataContext is KanbanColumn targetColumn)
        {
            var sourceColumn = vm.Columns.ToList().FirstOrDefault(c => c.Children.Contains(task));
            sourceColumn?.Children.Remove(task);

            targetColumn.Children.Add(task);

            task.KanbanColumnId = targetColumn.Column.Id;

            await ApiService.Post<Tasks>($"/kanban/move/{task.Id}/{task.KanbanColumnId}");
        }
    }
}