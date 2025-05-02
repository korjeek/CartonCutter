using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using CartonCutter.ViewModels;

namespace CartonCutter.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        AddHandler(DragDrop.DragOverEvent, OnDragOver);
        AddHandler(DragDrop.DropEvent, OnDrop);
    }
    
    private void OnDragOver(object? sender, DragEventArgs eventArgs)
    {
        if (DataContext is MainViewModel viewModel)
            viewModel.DragOverFile.Execute(eventArgs);
    }

    private async void OnDrop(object? sender, DragEventArgs eventArgs)
    {
        if (DataContext is MainViewModel viewModel)
            await viewModel.DropFile(eventArgs);
    }
    
    private void OnPointerPressed(object? sender, PointerPressedEventArgs eventArgs)
    {
        if (DataContext is MainViewModel viewModel)
            viewModel.MoveAndDragWindow.Execute(eventArgs);
    }

    private void OnPercentOfCutChange(object? sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox || string.IsNullOrEmpty(textBox.Text)) 
            return;

        if (!int.TryParse(textBox.Text, out var value))
        {
            textBox.Text = "0";
            textBox.CaretIndex = textBox.Text.Length;
            return;
        }
        
        switch (value)
        {
            case < 0:
                textBox.Text = "0";
                textBox.CaretIndex = textBox.Text.Length;
                break;
            case > 100:
                textBox.Text = "100";
                textBox.CaretIndex = textBox.Text.Length;
                break;
        }
    }
}