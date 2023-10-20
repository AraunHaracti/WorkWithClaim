using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Fekus2.Models;

namespace Fekus2.Views.TypeOfEquipmentViews;

public partial class ChangeItemView : Window
{
    private Action _action;
    
    private TypeOfEquipment _item = new();

    private bool isEdit = false;
    
    public ChangeItemView(Action action)
    {
        InitializeComponent();
        
        DataContext = _item;

        _action += action;
        
        isEdit = false;
    }
    
    public ChangeItemView(Action action, TypeOfEquipment item)
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
        string sql = $"insert into TypeOfEquipment (Name) " +
                     $"values ('{_item.Name}')";

        Database.Open();
        Database.SetData(sql);
        Database.Exit();
        
        _action.Invoke();
        
        Close();
    }

    private void EditItem()
    {
        string sql = $"update TypeOfEquipment as t set " +
                     $"t.Name = '{_item.Name}' " +
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