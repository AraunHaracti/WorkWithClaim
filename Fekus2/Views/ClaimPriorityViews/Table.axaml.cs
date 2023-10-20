using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Fekus2.ViewModels;

namespace Fekus2.Views.ClaimPriorityViews;

public partial class Table : Window
{
    public Table()
    {
        InitializeComponent();

        DataContext = new ClaimPriorityTableViewModel();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}