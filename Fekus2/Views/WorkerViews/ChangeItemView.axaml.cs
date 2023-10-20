using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Fekus2.Models;

namespace Fekus2.Views.WorkerViews;

public partial class ChangeItemView : Window
{
    private Action _action;
    
    private Worker _item = new();

    private bool isEdit = false;
    
    public ChangeItemView(Action action)
    {
        InitializeComponent();
        
        DataContext = _item;

        _action += action;
        
        isEdit = false;
    }
    
    public ChangeItemView(Action action, Worker item)
    {
        InitializeComponent();

        _item = item;
        
        DataContext = _item;

        _action += action;

        isEdit = true;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Change_OnClick(object? sender, RoutedEventArgs e)
    {
        if (isEdit)
        {
            EditItem();    
        }
        else
        {
            AddItem();    
        }
    }
    
    private void AddItem()
    {
        string sql = $"insert into Worker (Name, Login, Password, RoleId) " +
                     $"values ('{_item.Name}', '{_item.Login}', '{_item.Password}', '{_item.RoleId}')";

        Database.Open();
        Database.SetData(sql);
        Database.Exit();
        
        _action.Invoke();
        
        Close();
    }

    private void EditItem()
    {
        string sql = $"update Worker as t set " +
                     $"t.Name = '{_item.Name}', " +
                     $"t.Login = '{_item.Login}', " +
                     $"t.Password = '{_item.Password}', " +
                     $"t.RoleID = '{_item.RoleId}' " +
                     $"where t.WorkerID = '{_item.WorkerId}'";
        
        Database.Open();
        Database.SetData(sql);
        Database.Exit();
        
        _action.Invoke();

        Close();
    }

    private void Cancel_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}