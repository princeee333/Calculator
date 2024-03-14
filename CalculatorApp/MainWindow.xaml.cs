using System;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorApp
{
    public partial class MainWindow : Window
    {
        private double _lastNumber, _result;
        private SelectedOperator _selectedOperator;
        private bool _isDecimalClicked;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            if (Display.Text == "0" || Display.Text == _result.ToString())
            {
                Display.Text = button.Content.ToString();
            }
            else
            {
                Display.Text += button.Content.ToString();
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Display.Text, out _lastNumber))
            {
                Display.Text = "0";
            }

            if (sender is Button button)
            {
                switch (button.Content.ToString())
                {
                    case "+":
                        _selectedOperator = SelectedOperator.Addition;
                        break;
                    case "-":
                        _selectedOperator = SelectedOperator.Subtraction;
                        break;
                    case "*":
                        _selectedOperator = SelectedOperator.Multiplication;
                        break;
                    case "/":
                        _selectedOperator = SelectedOperator.Division;
                        break;
                }
            }

            _isDecimalClicked = false;
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isDecimalClicked)
            {
                if (Display.Text == string.Empty || Display.Text == "0")
                {
                    Display.Text = "0.";
                }
                else if (!Display.Text.Contains("."))
                {
                    Display.Text += ".";
                }

                _isDecimalClicked = true;
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(Display.Text, out newNumber))
            {
                switch (_selectedOperator)
                {
                    case SelectedOperator.Addition:
                        _result = SimpleMath.Add(_lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        _result = SimpleMath.Subtract(_lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        _result = SimpleMath.Multiply(_lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        if (newNumber == 0)
                        {
                            MessageBox.Show("Cannot divide by zero.");
                            return;
                        }

                        _result = SimpleMath.Divide(_lastNumber, newNumber);
                        break;
                }

                Display.Text = _result.ToString();
                _result = 0;
                _lastNumber = 0;
            }

            _isDecimalClicked = false;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = "0";
            _result = 0;
            _lastNumber = 0;
            _isDecimalClicked = false;
            _selectedOperator = SelectedOperator.None;
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        None
    }

    public static class SimpleMath
    {
        public static double Add(double n1, double n2) => n1 + n2;
        public static double Subtract(double n1, double n2) => n1 - n2;
        public static double Multiply(double n1, double n2) => n1 * n2;

        public static double Divide(double n1, double n2)
        {
            if (n2 == 0) throw new DivideByZeroException();
            return n1 / n2;
        }
    }
}