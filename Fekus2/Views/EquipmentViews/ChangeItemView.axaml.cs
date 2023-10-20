using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Fekus2.Models;

namespace Fekus2.Views.EquipmentViews;

public partial class ChangeItemView : Window
{
    private Action _action;
    
    private Equipment _item = new();

    private bool isEdit = false;
    
    public ChangeItemView(Action action)
    {
        InitializeComponent();
        
        DataContext = _item;

        _action += action;
        
        isEdit = false;
    }
    
    public ChangeItemView(Action action, Equipment item)
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
        string sql = $"insert into Equipment (Name, SerialNumber, TypeOfEquipmentId) " +
                     $"values ('{_item.Name}', " +
                     $"'{_item.SerialNumber}', " +
                     $"{_item.TypeOfEquipmentId})";

        Database.Open();
        Database.SetData(sql);
        Database.Exit();
        
        _action.Invoke();
        
        Close();
    }

    private void EditItem()
    {
        string sql = $"update Equipment as t set " +
                     $"t.Name = '{_item.Name}', " +
                     $"t.SerialNumber = '{_item.SerialNumber}', " +
                     $"t.TypeOfEquipmentId = {_item.TypeOfEquipmentId} " +
                     $"where t.EquipmentId = '{_item.EquipmentId}'";
        
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