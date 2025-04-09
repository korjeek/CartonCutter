using System;
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

    private void OnDrop(object? sender, DragEventArgs eventArgs)
    {
        if (DataContext is MainViewModel viewModel)
            viewModel.DropFile.Execute(eventArgs);
    }
    
    private void OnPointerPressed(object? sender, PointerPressedEventArgs eventArgs)
    {
        if (DataContext is MainViewModel viewModel)
            viewModel.MoveAndDragWindow.Execute(eventArgs);
    }
    
    [Obsolete("Obsolete")]
    private async void OnBrowseClick(object sender, RoutedEventArgs eventArgs)
    {
        
    }
}