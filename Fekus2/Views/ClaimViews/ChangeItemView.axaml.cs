using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Fekus2.Models;

namespace Fekus2.Views.ClaimViews;

public partial class ChangeItemView : Window
{
    private Action _action;
    
    private Claim _item = new();

    private bool isEdit = false;
    
    public ChangeItemView(Action action)
    {
        InitializeComponent();
        
        DataContext = _item;

        _action += action;
        
        isEdit = false;
    }
    
    public ChangeItemView(Action action, Claim item)
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
        string sql = $"insert into Claim (DateAdded, EquipmentID, TypeOfFaultID, Description, CustomerID, StatusID, CompletionDate, PriorityID, WorkerID) " +
                     $"values ('{_item.DateAdded.ToString("yyyy-MM-dd H:mm:ss")}', " +
                     $"'{_item.EquipmentId}', " +
                     $"'{_item.TypeOfFaultId}', " +
                     $"'{_item.Description}', " +
                     $"'{_item.CustomerId}', " +
                     $"'{_item.StatusId}', " +
                     $"'{_item.CompletionDate.ToString()}', " +
                     $"'{_item.PriorityId}', " +
                     $"{_item.WorkerId})";

        Database.Open();
        Database.SetData(sql);
        Database.Exit();
        
        _action.Invoke();
        
        Close();
    }

    private void EditItem()
    {
        string sql = $"update Claim as t set " +
                     $"t.DateAdded = '{_item.DateAdded.ToString("yyyy-MM-dd H:mm:ss")}', " +
                     $"t.EquipmentID = '{_item.EquipmentId}', " +
                     $"t.TypeOfFaultID = '{_item.TypeOfFaultId}', " +
                     $"t.Description = '{_item.Description}', " +
                     $"t.CustomerID = '{_item.CustomerId}', " +
                     $"t.StatusID = '{_item.StatusId}', " +
                     $"t.CompletionDate = '{_item.CompletionDate.ToString()}', " +
                     $"t.PriorityID = '{_item.PriorityId}', " +
                     $"t.WorkerID = '{_item.DateAdded}', " +
                     $"where t.ClaimID = '{_item.ClaimId}'";
        
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