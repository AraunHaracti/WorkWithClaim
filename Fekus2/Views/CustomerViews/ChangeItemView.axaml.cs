using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Fekus2.Models;

namespace Fekus2.Views.CustomerViews;

public partial class ChangeItemView : Window
{
    private readonly Action _action;
    
    private readonly Сustomer _item;

    private readonly bool _isEdit;
    
    public ChangeItemView(Action action)
    {
        InitializeComponent();

        _item = new Сustomer();
        
        DataContext = _item;

        _action += action;
        
        _isEdit = false;
    }
    
    public ChangeItemView(Action action, Сustomer item)
    {
        InitializeComponent();

        _item = item;
        
        DataContext = _item;

        _action += action;

        _isEdit = true;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Change_OnClick(object? sender, RoutedEventArgs e)
    {
        if (_isEdit)
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
        string sql = $"insert into Сustomer (Name, Phone) " +
                     $"values ('{_item.Name}', '{_item.Phone}')";

        Database.Open();
        Database.SetData(sql);
        Database.Exit();
        
        _action.Invoke();
        
        Close();
    }

    private void EditItem()
    {
        string sql = $"update Сustomer as t set " +
                     $"t.Name = '{_item.Name}', " +
                     $"t.Phone = '{_item.Phone}', " +
                     $"where t.CustomerID = '{_item.CustomerId}'";
        
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