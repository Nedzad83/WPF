using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class CalculationModel
    {
        // initial state
        static int[] money = { 5000 /*£50*/, 2000 /*£20*/, 1000 /*£10*/, 500 /*£5*/, 200 /*£2*/, 100 /*£1*/, 50, 20, 10, 5, 2, 1 };
        static int[] moneyCount = { 50, 50, 50, 50, 100, 100, 100, 100, 100, 100, 100, 100 };

        #region Private members

        private string result;

        #endregion

        #region Constructors

        public CalculationModel(string amount, string secondOperand, string operation, bool usingAlg1)
        {
            ValidateOperand(amount.ToString());
            ValidateOperand(secondOperand);
            ValidateOperation(operation);

            Amount = amount;
            Operation = operation;
            result = string.Empty;
        }

        public CalculationModel(string amount, string operation)
        {
            ValidateOperand(amount);
            ValidateOperation(operation);

            Amount = string.Empty;
            Operation = operation;
            result = string.Empty;
        }

        public CalculationModel()
        {
            Amount = string.Empty;
            Operation = string.Empty;
            result = string.Empty;
        }

        #endregion

        #region Public properties and methods

        public string Amount { get; set; }
        public string SecondOperand { get; set; }
        public string Operation { get; set; }
        public string Result { get { return result; } }

        public void CalculateResult(bool isAlgorithm1)
        {
            ValidateData();

            try
            {
                result = string.Empty;
                List<List<int>> finalResult = algorithmOne(Convert.ToDouble(Amount), isAlgorithm1);
                updateMoneyCount(finalResult);

                string res1 = ("Amount chached out: " + Amount + "£");

                for (int i = 0; i < finalResult.Count; i++)
                {
                    if (finalResult[i][0] >= 100 && finalResult[i][1] > 0)
                        result += finalResult[i][0] / 100 + "£ x " + finalResult[i][1] + "\n";
                    else if (finalResult[i][0] < 100 && finalResult[i][1] > 0)
                        result += finalResult[i][0] + "p x " + finalResult[i][1] + "\n";
                    //startingAmount = startingAmount + (finalResult[i][0] * moneyState[finalResult[i][0]]);
                }
            }
            catch (Exception)
            {
                result = "Error whilst calculating";
                throw;
            }
        }
        
        public static List<List<int>> calc(double amount, int i, bool isAlgo2)
        {
            int pounds = (int)(amount * 100);
            List<List<int>> result = new List<List<int>>();

            if (isAlgo2 && i < 1)
            {
                if (moneyCount[1] == 0)
                {
                    return null;
                }
                else
                {
                    int currPounds = pounds / money[1];

                    if (currPounds > moneyCount[1])
                    {
                        currPounds = moneyCount[1];
                        pounds = pounds - moneyCount[1] * money[1];
                        List<int> ls = new List<int>();
                        ls.Add(money[1]);
                        ls.Add(currPounds);
                        result.Add(ls); //(money[i], currPounds));
                    }
                    else
                    {
                        List<int> ls = new List<int>();
                        ls.Add(money[1]);
                        ls.Add(currPounds);
                        result.Add(ls);
                        pounds = pounds % money[1];
                    }
                }
            }

            for (; i < money.Length; i++)
            {
                if (moneyCount[i] == 0)
                {
                    return null;
                }
                else
                {
                    int currPounds = pounds / money[i];

                    if (currPounds > moneyCount[i])
                    {
                        currPounds = moneyCount[i];
                        pounds = pounds - moneyCount[i] * money[i];
                        List<int> ls = new List<int>();
                        ls.Add(money[i]);
                        ls.Add(currPounds);
                        result.Add(ls);
                    }
                    else
                    {
                        List<int> ls = new List<int>();
                        ls.Add(money[i]);
                        ls.Add(currPounds);
                        result.Add(ls);
                        pounds = pounds % money[i];
                    }
                }
            }
            return result;
        }

        public List<List<int>> algorithmOne(double amount, bool isAlgorithm1)
        {
            List<List<int>> finalResult = new List<List<int>>();
            int finalMin = 0;
            int currentMin = 0;
            for (int i = 0; i < money.Length; i++)
            {
                List<List<int>> list = calc(amount, i, !isAlgorithm1);
                if (list != null)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        currentMin += list[j][1];
                    }
                    if (finalResult.Count == 0)
                    {
                        finalResult = list;
                        finalMin = currentMin;
                    }
                    else
                    {
                        if (currentMin < finalMin)
                        {
                            finalMin = currentMin;
                            finalResult = list;
                        }
                    }
                }
            }
            return finalResult;
        }

        private static void updateMoneyCount(List<List<int>> finalResult2)
        {
            for (int i = 0, j = 0; i < money.Length; i++)
            {
                if (money[i] == finalResult2[j][0])
                {
                    moneyCount[i] -= finalResult2[j][1];
                    j++;
                }
            }
        }

        private void ValidateOperand(string operand)
        {
            try
            {
                Convert.ToDouble(operand);
            }
            catch (Exception)
            {
                result = "Invalid number: " + operand;
                throw;
            }
        }

        private void ValidateOperation(string operation)
        {
            // do some validation if need..
        }

        private void ValidateData()
        {
            // do some validation here..
        }

        #endregion
    }
}
