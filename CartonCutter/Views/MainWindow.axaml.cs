using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using CartonCutter.Services.Interfaces;
using CartonCutter.ViewModels;

namespace CartonCutter.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        DataContext = viewModel;
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
    
    [Obsolete("Obsolete")]
    private async void OnBrowseClick(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Title = "Выберите Excel файл",
            Filters = { new FileDialogFilter { Name = "Excel Files", Extensions = { "xlsx", "xls" } } },
            AllowMultiple = false
        };

        var result = await dialog.ShowAsync(this);
        if (result != null && result.Length != 0)
            Console.WriteLine("Good!");
    }
}