using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Task8_WPF.UI.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void ExecuteOperation<TService>(
        Func<TService> serviceInstance,
        Action<TService> serviceAction,
        string successMessage)
    {
        try
        {
            var service = serviceInstance();
            serviceAction(service);
            MessageBox.Show($"Operation successful!\n" + successMessage);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    protected bool CanExecuteOperation<TSelectedObj>(TSelectedObj selectedObj)
        where TSelectedObj : class
    {
        return selectedObj is not null;
    }

    protected bool CanExecuteOperation<TSelectedObj>(TSelectedObj selectedObj, string parameter)
        where TSelectedObj : class
    {
        return selectedObj is not null
            && !string.IsNullOrWhiteSpace(parameter);
    }

    protected bool CanExecuteOperation<TSelectedObj>(TSelectedObj selectedObj, string parameter1, string parameter2)
        where TSelectedObj : class
    {
        return selectedObj is not null
            && !string.IsNullOrWhiteSpace(parameter1)
            && !string.IsNullOrWhiteSpace(parameter2);
    }

    protected bool CanExecuteOperation(string parameter1, string parameter2)
    {
        return !string.IsNullOrWhiteSpace(parameter1)
            && !string.IsNullOrWhiteSpace(parameter2);
    }
}
