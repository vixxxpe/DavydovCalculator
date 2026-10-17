using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DavydovCalculator.Core;

namespace DavydovCalculator.Wpf;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly ExpressionCalculator _calculator = new ExpressionCalculator();
    private string _expression = "";
    private string _result = "0";

    public MainViewModel()
    {
        AddSymbolCommand = new RelayCommand(parameter => AddSymbol(parameter?.ToString() ?? ""));
        CalculateCommand = new RelayCommand(_ => Calculate());
        ClearCommand = new RelayCommand(_ => Clear());
        BackspaceCommand = new RelayCommand(_ => Backspace());
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Expression
    {
        get => _expression;
        set
        {
            _expression = value;
            OnPropertyChanged();
        }
    }

    public string Result
    {
        get => _result;
        set
        {
            _result = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddSymbolCommand { get; }

    public ICommand CalculateCommand { get; }

    public ICommand ClearCommand { get; }

    public ICommand BackspaceCommand { get; }

    public void AddSymbol(string symbol)
    {
        Expression += symbol;
    }

    public void Calculate()
    {
        try
        {
            Result = _calculator.Calculate(Expression).ToString();
        }
        catch (Exception ex)
        {
            Result = "Ошибка: " + ex.Message;
        }
    }

    public void Clear()
    {
        Expression = "";
        Result = "0";
    }

    public void Backspace()
    {
        if (Expression.Length > 0)
        {
            Expression = Expression.Substring(0, Expression.Length - 1);
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
