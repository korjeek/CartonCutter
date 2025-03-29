using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Platform.Storage;

namespace CartonCutter.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        AddHandler(DragDrop.DragOverEvent, OnDragOver);
        AddHandler(DragDrop.DropEvent, OnDrop);
    }
    
    private void OnCloseButtonClick(object sender, RoutedEventArgs e) => Close();
    
    private void OnChangeWindowStateButton(object sender, RoutedEventArgs e)
    {
        var image = ((Button)sender).Content as Image ?? 
                    throw new ArgumentException("Sender is not Button or content is not Image");

        if (WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
            image.Source = new Bitmap(AssetLoader.Open(new Uri("avares://CartonCutter/Assets/maximize-512.png")));
        }
        else
        {
            WindowState = WindowState.Maximized;
            image.Source =new Bitmap(AssetLoader.Open(new Uri("avares://CartonCutter/Assets/restore-down-512.png")));
        }
    }

    private void OnMoveAndDragWindow(object sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            BeginMoveDrag(e);
    }
    
    private void OnMinimizeButtonClick(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
    
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
        if (result != null && result.Any())
        {
            Console.WriteLine("Good!");
        }
    }
    
    private async void OnDrop(object sender, DragEventArgs e)
    {
        var files = e.Data.GetFiles();
        if (files != null)
        {
            var filePath = files.FirstOrDefault()?.TryGetLocalPath();
            if (!string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine("Good!");
            }
        }
    }

    // Проверка формата при перетаскивании
    private void OnDragOver(object sender, DragEventArgs e)
    {
        e.DragEffects = e.Data.Contains(DataFormats.Files) 
            ? DragDropEffects.Copy 
            : DragDropEffects.None;
    }
}