using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Double result = 3.145671231345;
        string Operationis;
        Double FirstSide;
        Double SecondSide;
        string LastEquation;
        List<equation> equations = new List<equation>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonNumber_Click(object sender, RoutedEventArgs e)
        {
            if (double.Parse(txt_answer.Text) == result)
            {
                SetHistory();
            }
            Button btn = sender as Button;
            string num = btn.Content.ToString();
            if (num == ".")
            {
                if (txt_answer.Text.Contains(num))
                {
                    return;
                }
                else
                    txt_answer.Text = txt_answer.Text + num;
                btn_zero.IsEnabled = true;
            }
            else
            {
                if (txt_answer.Text == "0")
                {
                    txt_answer.Text = "";
                }
                btn_zero.IsEnabled = true;
                txt_answer.Text = txt_answer.Text + num;
            }
        }

        private void btn_Operation_Click(object sender, RoutedEventArgs e)
        {
            FirstSide = double.Parse(txt_answer.Text);
            txt_equation.Text = FirstSide.ToString();
            txt_answer.Text = "0";
            Button btn = sender as Button;
            Operationis = btn.Name.ToString().Replace("btn_", "");
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            if (double.Parse(txt_answer.Text) == result)
            {
                SetHistory();
            }
            txt_answer.Text = "0";
            txt_equation.Text = "";
            btn_zero.IsEnabled = false;
        }

        private void btn_equal_Click(object sender, RoutedEventArgs e)
        {
            equalOperation();
        }

        private void equalOperation()
        {
            SecondSide = double.Parse(txt_answer.Text);
            switch (Operationis)
            {
                case "plus":
                    txt_equation.Text = txt_equation.Text + " + " + txt_answer.Text;
                    result = FirstSide + SecondSide;
                    txt_answer.Text = result.ToString();
                    LastEquation = txt_equation.Text + " = " + result.ToString();
                    break;
                case "minus":
                    txt_equation.Text = txt_equation.Text + " - " + txt_answer.Text;
                    result = FirstSide - SecondSide;
                    txt_answer.Text = result.ToString();
                    LastEquation = txt_equation.Text + " = " + result.ToString();
                    break;
                case "multiplication":
                    txt_equation.Text = txt_equation.Text + " * " + txt_answer.Text;
                    result = FirstSide * SecondSide;
                    txt_answer.Text = result.ToString();
                    LastEquation = txt_equation.Text + " = " + result.ToString();
                    break;
                case "division":
                    txt_equation.Text = txt_equation.Text + " / " + txt_answer.Text;
                    result = FirstSide / SecondSide;
                    txt_answer.Text = result.ToString();
                    LastEquation = txt_equation.Text + " = " + result.ToString();
                    break;
                case "percent":
                    txt_equation.Text = txt_equation.Text + " % " + txt_answer.Text;
                    result = (FirstSide * SecondSide) / 100;
                    txt_answer.Text = result.ToString();
                    LastEquation = txt_equation.Text + " = " + result.ToString();
                    break;
                default:
                    break;

            }
        }

        private void SetHistory()
        {
            txt_answer.Text = "0";
            txt_equation.Text = "";
            equation equation = new equation();
            txt_Previousequation.Text = LastEquation;
            equation.Equation = LastEquation;
            CalculatorDataAccess.SaveEquations(equation);
        }

        private void btn_plusminus_Click(object sender, RoutedEventArgs e)
        {
            double v = double.Parse(txt_answer.Text);
            result = -v;
            txt_answer.Text = result.ToString();
        }

        private void calculator_KeyDown(object sender, KeyEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "0"))
            {
                if (txt_answer.Text == "0")
                    return;
                else
                    txt_answer.Text = txt_answer.Text + "0";
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "[1-9]"))
            {
                if (txt_answer.Text == "0")
                {
                    txt_answer.Text = "";
                }
                btn_zero.IsEnabled = true;
                if (e.Key.ToString().Contains("NumPad"))
                {
                    string num = e.Key.ToString().Replace("NumPad", "");
                    txt_answer.Text = txt_answer.Text + num;
                }
                else if (e.Key.ToString().Contains("D") && e.Key.ToString().Length == 2)
                {
                    string num = e.Key.ToString().Replace("D", "");
                    txt_answer.Text = txt_answer.Text + num;
                }
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "."))
            {
                if (e.Key == Key.Enter)
                {
                    equalOperation();
                }

                switch (e.Key.ToString())
                {
                    case "OemPeriod":
                        DotApply();
                        break;
                    case "Decimal":
                        DotApply();
                        break;
                    case "Divide":
                        StartOperationKeyboard("division");
                        break;
                    case "Multiply":
                        StartOperationKeyboard("multiplication");
                        break;
                    case "Substract":
                        StartOperationKeyboard("minus");
                        break;
                    case "Add":
                        StartOperationKeyboard("plus");
                        break;
                }

            }
            else e.Handled = true;
        }
        private void DotApply()
        {
            string num = ".";
            if (txt_answer.Text.Contains(num))
            {
                return;
            }
            else
                txt_answer.Text = txt_answer.Text + num;
            btn_zero.IsEnabled = true;
        }
        private void StartOperationKeyboard(string operation)
        {
            FirstSide = double.Parse(txt_answer.Text);
            txt_equation.Text = FirstSide.ToString();
            txt_answer.Text = "0";
            Operationis = operation;
        }

        private void btn_History_Click(object sender, RoutedEventArgs e)
        {
            if (History.Visibility == Visibility.Collapsed && History.Opacity == 0)
            {
                History.Visibility = Visibility.Visible;
                History.Opacity = 1;
            }
        }

        private void historyList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                History.Visibility = Visibility.Collapsed;
                History.Opacity = 0;
            }
        }

        private void History_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            equations = CalculatorDataAccess.LoadEquations();
            MyList.ItemsSource = equations;
        }

        private void grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Middle)
            {
                Application.Current.Shutdown(110);
            }
        }
    }
}
