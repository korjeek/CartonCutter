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
    
    public void OnCloseButtonClick(object sender, RoutedEventArgs e) => Close();
    
    public void OnChangeWindowStateButtonClick(object sender, RoutedEventArgs e)
    {
        var image = ((Button)sender).Content as Image ?? 
                    throw new ArgumentException("Sender is not Button or content is not Image");

        if (WindowState == WindowState.Maximized)
            NormalizeWindow(image);
        else
            MaximizeWindow(image);
    }

    public void OnMoveAndDragWindow(object sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            BeginMoveDrag(e);
    }
    
    public void OnMinimizeButtonClick(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
    
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
    
    private void OnDrop(object? sender, DragEventArgs e)
    {
        var files = e.Data.GetFiles();
        if (files == null) 
            return;
        
        var filePath = files.FirstOrDefault()?.TryGetLocalPath();
        if (!string.IsNullOrEmpty(filePath))
            Console.WriteLine("Good!");
    }
    
    private static void OnDragOver(object? sender, DragEventArgs e) =>
        e.DragEffects = e.Data.Contains(DataFormats.Files) ? DragDropEffects.Copy : DragDropEffects.None;

    private void NormalizeWindow(Image image)
    {
        WindowState = WindowState.Normal;
        image.Source = new Bitmap(AssetLoader.Open(new Uri("avares://CartonCutter/Assets/maximize-512.png")));
    }

    private void MaximizeWindow(Image image)
    {
        WindowState = WindowState.Maximized;
        image.Source = new Bitmap(AssetLoader.Open(new Uri("avares://CartonCutter/Assets/restore-down-512.png")));
    }
}