using CartonCutter.Services;
using CartonCutter.Services.Interfaces;
using CartonCutter.ViewModels;
using CartonCutter.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CartonCutter;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<IWindowService, WindowService>();
        services.AddSingleton<IImageService, WindowTitleBarImageService>();
        services.AddSingleton<IDragDropFileService, DragDropFileService>();

        services.AddSingleton<MainWindow>();
    }   
}