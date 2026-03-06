using System.Collections.ObjectModel;
using System.Linq;
using TasksAPI.Models;

namespace TasksApp.ViewModels;

public record KanbanColumn(KanbanColumns Column, ObservableCollection<Tasks> Children);

public partial class KanbanWindowViewModel : ViewModelBase
{
    public ObservableCollection<KanbanColumn> Columns { get; set; } = [];
    
    public void Update(Tasks[] tasks)
    {
        foreach (var task in tasks)
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
}