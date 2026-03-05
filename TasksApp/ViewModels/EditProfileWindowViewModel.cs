using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using TasksAPI.Models;

namespace TasksApp.ViewModels;

public partial class EditProfileWindowViewModel : ViewModelBase
{
    public ICollection<Roles> Roles { get; set; } = [];
    public ICollection<Departments> Departments { get; set; } = [];

    [ObservableProperty] private string _userName = "";
    [ObservableProperty] private Roles? _userRole;
    [ObservableProperty] private Departments? _userDepartment;

    public void UpdateData(Users user, Roles[] roles, Departments[] departments)
    {
        UserName = user.Name;
        
        Roles.Clear();
        Departments.Clear();
        
        foreach (var role in roles)
        {
            Roles.Add(role);
            if (role.Id == user.RoleId) UserRole = role;
        }
        
        foreach (var department in departments)
        {
            Departments.Add(department);
            if (department.Id == user.DepartmentId) UserDepartment = department;
        }
        
    }
}