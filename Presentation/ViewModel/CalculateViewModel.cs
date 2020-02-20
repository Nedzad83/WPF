using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Presentation.Commands;

namespace Presentation.ViewModel
{
    public class CalculateViewModel : ViewModelBase
    {
        #region Private members

        private Models.CalculationModel calculation;

        private DelegateCommand<string> digitButtonPressCommand;
        private DelegateCommand<string> withdrawalButtonPressCommand;
        private DelegateCommand<string> alg2PressCommand;
        
        private bool newDisplayRequired = false;
        private static double state = 2000.01;
        private string display;
        private string balance;
        private bool isChecked1 = true;

        #endregion

        #region Constructor

        public CalculateViewModel()
        {
            this.calculation = new CalculationModel();
            this.display = "0";
            this.Amount = string.Empty;
            this.Operation = string.Empty;
            this.balance = state.ToString();
            this.isChecked1 = IsChecked1;
        }

        #endregion

        #region Public Properties

        public string Amount
        {
            get { return calculation.Amount; }
            set { calculation.Amount = value; }
        }

        public string Operation
        {
            get { return calculation.Operation; }
            set { calculation.Operation = value; }
        }

        public string Result
        {
            get { return calculation.Result; }
        }

        public string Display
        {
            get { return display; }
            set
            {
                display = value;
                OnPropertyChanged("Display");
            }
        }

        public string Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                OnPropertyChanged("Balance");
            }
        }
        public bool IsChecked1
        {
            get { return isChecked1; }
            set { isChecked1 = value; }
        }
        
        #endregion

        #region Commands

        public ICommand WithdrawalButtonPressCommand
        {
            get
            {
                if (withdrawalButtonPressCommand == null)
                {
                    withdrawalButtonPressCommand = new DelegateCommand<string>(
                        OperationButtonPress, CanOperationButtonPress);
                }
                return withdrawalButtonPressCommand;
            }
        }

        private static bool CanOperationButtonPress(string number)
        {
            return true;
        }

        public ICommand DigitButtonPressCommand
        {
            get
            {
                if (digitButtonPressCommand == null)
                {
                    digitButtonPressCommand = new DelegateCommand<string>(
                        DigitButtonPress, CanDigitButtonPress);
                }
                return digitButtonPressCommand;
            }
        }

        private static bool CanDigitButtonPress(string button)
        {
            return true;
        }
        
        public void DigitButtonPress(string button)
        {
            switch (button)
            {
                case "C":
                    Display = "0";
                    Amount = string.Empty;
                    Operation = string.Empty;
                    //Balance = string.Empty;
                    break;
                case "Del":
                    if (display.Length > 1)
                        Display = display.Substring(0, display.Length - 1);
                    else Display = "0";
                    break;
                case ".":
                    if (newDisplayRequired)
                    {
                        Display = "0.";
                    }
                    else
                    {
                        if (!display.Contains("."))
                        {
                            Display = display + ".";
                        }
                    }
                    break;
                default:
                    if (display == "0" || newDisplayRequired)
                        Display = button;
                    else
                        Display = display + button;
                    break;
            }
            newDisplayRequired = false;
        }
        
        public void OperationButtonPress(string operation)
        {
            try
            {
                if (operation == "CALC")
                {
                    double val = 0;
                    try { val = Convert.ToDouble(display); } catch { }
                    if (val == 0)
                        return;
                    Amount = display;
                    if (state > Convert.ToDouble(Amount))
                    {
                        calculation.CalculateResult(isChecked1);
                        Display = string.Empty;
                        Balance = (Math.Round(Convert.ToDouble(state), 2) - Math.Round(Convert.ToDouble(Amount), 2)).ToString();
                        state -= Math.Round(Convert.ToDouble(Amount), 2);
                        Display = Result;
                    }
                    else
                    {
                        Display = "You don't have enough money on your account !";
                    }
                }

                newDisplayRequired = true;
            }
            catch (Exception ex)
            {
                Display = Result == string.Empty ? "Error - see event log" : Result;
                LogExceptionInformation(ex);
            }
        }
        
        private static void LogExceptionInformation(Exception ex)
        {
            // implement some exception info..
        }

        #endregion
    }
}
