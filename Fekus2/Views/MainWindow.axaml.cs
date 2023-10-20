using Avalonia.Controls;
using Avalonia.Interactivity;
using Fekus2.ViewModels;

namespace Fekus2.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        DataContext = new ClaimTableViewModel(this);
    }

    private void TypeOfEquipment_OnClick(object? sender, RoutedEventArgs e)
    {
        TypeOfEquipmentViews.Table tableView = new ();
        tableView.ShowDialog(this);
    }

    private void Equipment_OnClick(object? sender, RoutedEventArgs e)
    {
        EquipmentViews.Table tableView = new ();
        tableView.ShowDialog(this);
    }

    private void WorkerRole_OnClick(object? sender, RoutedEventArgs e)
    {
        WorkerRoleViews.Table tableView = new ();
        tableView.ShowDialog(this);
    }

    private void Worker_OnClick(object? sender, RoutedEventArgs e)
    {
        WorkerViews.Table tableView = new ();
        tableView.ShowDialog(this);
    }

    private void TypeOfFault_OnClick(object? sender, RoutedEventArgs e)
    {
        TypeOfFaultViews.Table tableView = new ();
        tableView.ShowDialog(this);
    }

    private void Customer_OnClick(object? sender, RoutedEventArgs e)
    {
        CustomerViews.Table tableView = new ();
        tableView.ShowDialog(this);
    }

    private void ClaimStatus_OnClick(object? sender, RoutedEventArgs e)
    {
        ClaimStatusViews.Table tableView = new ();
        tableView.ShowDialog(this);
    }

    private void ClaimPriority_OnClick(object? sender, RoutedEventArgs e)
    {
        ClaimPriorityViews.Table tableView = new ();
        tableView.ShowDialog(this);
    }

    private void Claim_OnClick(object? sender, RoutedEventArgs e)
    {
        ClaimViews.Table tableView = new ();
        tableView.ShowDialog(this);
    }
}