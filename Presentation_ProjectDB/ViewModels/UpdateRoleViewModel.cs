﻿

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Dtos;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;

namespace Presentation_ProjectDB.ViewModels;

public partial class UpdateRoleViewModel : ObservableObject
{
    private readonly IServiceProvider _sp;
    private readonly RoleService _roleService;

    public UpdateRoleViewModel(IServiceProvider sp, RoleService roleService)
    {
        _sp = sp;
        _roleService = roleService;

        Roles = new ObservableCollection<RoleDto>(_roleService.GetAllRoles());

        Role = _roleService.CurrentRole;
    }

    [ObservableProperty]
    private ObservableCollection<RoleDto> _roles = new ObservableCollection<RoleDto>();

    [ObservableProperty]
    private RoleDto role = new RoleDto();


    /// <summary>
    /// Update role button
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task UpdatedRole()
    {
        await _roleService.UpdateRoleAsync(Role);

        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<RoleViewModel>();
    }

    /// <summary>
    /// Navigate to list 
    /// </summary>
    [RelayCommand]
    private void NavigateToRoleList()
    {
        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<RoleViewModel>();
    }

    /// <summary>
    /// Navigate to Update role
    /// </summary>
    /// <param name="role"></param>
    [RelayCommand]
    private void NavigateToUpdateRole(RoleDto role)
    {

        _roleService.CurrentRole = role;

        var mainViewModel = _sp.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _sp.GetRequiredService<UpdateRoleViewModel>();
    }

    /// <summary>
    /// Navigate to delete
    /// </summary>
    /// <param name="role"></param>
    [RelayCommand]
    private void NavigateToDelete(RoleDto role)
    {

        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this role?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            _roleService.DeleteRole(role);

            var mainViewModel = _sp.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _sp.GetRequiredService<RoleViewModel>();
        }

    }
}
