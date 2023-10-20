using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Fekus2.Models;

namespace Fekus2.Views.TypeOfFaultViews;

public partial class ChangeItemView : Window
{
    private readonly Action _action;
    
    private readonly TypeOfFault _item;

    private readonly bool _isEdit;
    
    public ChangeItemView(Action action)
    {
        InitializeComponent();

        _item = new TypeOfFault();
        
        DataContext = _item;

        _action += action;
        
        _isEdit = false;
    }
    
    public ChangeItemView(Action action, TypeOfFault item)
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
        string sql = $"insert into TypeOfFault (TypeName) " +
                     $"values ('{_item.TypeName}')";

        Database.Open();
        Database.SetData(sql);
        Database.Exit();
        
        _action.Invoke();
        
        Close();
    }

    private void EditItem()
    {
        string sql = $"update TypeOfFault as t set " +
                     $"t.TypeName = '{_item.TypeName}' " +
                     $"where t.TypeID = '{_item.TypeId}'";
        
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