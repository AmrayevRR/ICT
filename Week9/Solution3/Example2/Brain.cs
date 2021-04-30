using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example2
{
    delegate void DisplayMessage(string text);
    class Brain
    {
        DisplayMessage displayMessage;
        public Brain(DisplayMessage _delegate) 
        {
            displayMessage = _delegate;
        }

        string[] nonZeroDigits = {"1", "2", "3", "4", "5", "6", "7", "8", "9"};
        string[] digits = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
        string[] zero = { "0" };
        string[] operation = { "+", "-", "*", "/" };
        string[] equal = { "=" };
        string[] separator = { "," };
        string[] clear = { "C" };
        string[] unaryOperation = { "√", "ln", "sin", "cos", "tan" };
        string[] arrow = { "🠔" };

        enum State
        {
            Zero,
            AccumulateDigits,
            ComputePending,
            Compute,
            AccumulateDigitsWithDecimal,
            Clear,
            ComputeUnary,
            RemoveDigit
        }

        State currentState = State.Zero;
        string previousNumber = "";
        string currentNumber = "";
        string currentOperation = "";
        public void ProcessSignal(string message)
        {
            switch (currentState)
            {
                case State.Zero:
                    ProcessorZeroState(message, false);
                    break;
                case State.AccumulateDigits:
                    ProcessAccumulateDigits(message, false);
                    break;
                case State.ComputePending:
                    ProcessComputePending(message, false);
                    break;
                case State.Compute:
                    ProcessCompute(message, false);
                    break;
                case State.AccumulateDigitsWithDecimal:
                    ProcessAccumulateDigitsWithDecimal(message, false);
                    break;
                case State.ComputeUnary:
                    ProcessComputeUnary(message, false);
                    break;
                case State.RemoveDigit:
                    ProcessRemoveDigit(message, false);
                    break;
                default:
                    break;
            }
        }

        void ProcessorZeroState(string msg, bool income)
        {
            if (income)
            {
                currentState = State.Zero;
                currentNumber = "";
                previousNumber = "0";
                displayMessage("0");
            }
            else
            {
                if (nonZeroDigits.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                }
                else if (zero.Contains(msg)) { }
                else if (operation.Contains(msg))
                {
                    currentNumber = "0";
                    ProcessComputePending(msg, true);
                }
                else if (clear.Contains(msg))
                {
                    ProcessorZeroState(msg, true);
                }
                else if (unaryOperation.Contains(msg))
                {
                    currentNumber = "0";
                    ProcessComputeUnary(msg, true);
                }
                else if (separator.Contains(msg))
                {
                    currentNumber = "0";
                    ProcessAccumulateDigitsWithDecimal(msg, true);
                }
            }
        }

        void ProcessAccumulateDigits(string msg, bool income)
        {
            if (income)
            {
                currentState = State.AccumulateDigits;
                if (zero.Contains(currentNumber))
                {
                    currentNumber = msg;
                }
                else
                {
                    currentNumber = currentNumber + msg;
                }
                displayMessage(currentNumber);
            } 
            else
            {
                if (digits.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                } else if (operation.Contains(msg))
                {
                    ProcessComputePending(msg, true);
                } else if(equal.Contains(msg))
                {
                    ProcessCompute(msg, true);
                } else if (separator.Contains(msg))
                {
                    ProcessAccumulateDigitsWithDecimal(msg, true);
                } else if (clear.Contains(msg))
                {
                    ProcessorZeroState(msg, true);
                } else if (unaryOperation.Contains(msg))
                {
                    ProcessComputeUnary(msg, true);
                } else if (arrow.Contains(msg))
                {
                    displayMessage("Yes");
                    ProcessRemoveDigit(msg, true);
                }
                else
                {
                    displayMessage("No");
                }
            }
        }

        void ProcessComputePending(string msg, bool income)
        {
            if (income)
            {
                currentState = State.ComputePending;
                previousNumber = currentNumber;
                currentNumber = "";
                currentOperation = msg;
            }
            else
            {
                if (digits.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                }
                else if (clear.Contains(msg))
                {
                    ProcessorZeroState(msg, true);
                }
            }
        }

        void ProcessCompute(string msg, bool income)
        {
            if (income)
            {
                currentState = State.Compute;

                double a = double.Parse(previousNumber);
                double b = double.Parse(currentNumber);

                if (currentOperation == "+")
                {
                    currentNumber = (a + b).ToString();
                }
                else if (currentOperation == "-")
                {
                    currentNumber = (a - b).ToString();
                }
                else if (currentOperation == "*")
                {
                    currentNumber = (a * b).ToString();
                }
                else if (currentOperation == "/")
                {
                    currentNumber = (a / b).ToString();
                }

                previousNumber = currentNumber;

                displayMessage(currentNumber);

                currentNumber = "";
            }
            else
            {
                if (nonZeroDigits.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                }
                else if (zero.Contains(msg))
                {
                    ProcessorZeroState(msg, true);
                }
                else if (operation.Contains(msg))
                {
                    currentNumber = previousNumber;
                    ProcessComputePending(msg, true);
                }
                else if (clear.Contains(msg))
                {
                    ProcessorZeroState(msg, true);
                }
                else if (unaryOperation.Contains(msg))
                {
                    currentNumber = previousNumber;
                    ProcessComputeUnary(msg, true);
                }
                else if (separator.Contains(msg))
                {
                    currentNumber = "0";
                    ProcessAccumulateDigitsWithDecimal(msg, true);
                }
            }
        }

        void ProcessAccumulateDigitsWithDecimal(string msg, bool income)
        {
            if (income)
            {
                currentState = State.AccumulateDigitsWithDecimal;

                currentNumber = currentNumber + msg;

                displayMessage(currentNumber);


            }
            else
            {
                if (digits.Contains(msg))
                {
                    ProcessAccumulateDigitsWithDecimal(msg, true);
                }
                else if (operation.Contains(msg))
                {
                    ProcessComputePending(msg, true);
                }
                else if (equal.Contains(msg))
                {
                    ProcessCompute(msg, true);
                }
                else if (separator.Contains(msg)) { }
                else if (clear.Contains(msg))
                {
                    ProcessorZeroState(msg, true);
                }
                else if (unaryOperation.Contains(msg))
                {
                    ProcessComputeUnary(msg, true);
                }
                else if (arrow.Contains(msg))
                {
                    ProcessRemoveDigit(msg, true);
                }
            }
        }

        void ProcessComputeUnary(string msg, bool income)
        {
            if (income)
            {
                currentState = State.ComputeUnary;
                double x = double.Parse(currentNumber);

                if (msg == "√")
                {
                    if (x < 0)
                    {
                        displayMessage("Illegal input");
                    }
                    else
                    {
                        currentNumber = Convert.ToString(Math.Sqrt(x));
                        displayMessage(currentNumber);
                    }
                }
                else if (msg == "ln")
                {
                    if (x < 0)
                    {
                        displayMessage("Illegal input");
                    }
                    else
                    {
                        currentNumber = Convert.ToString(Math.Log(x));
                        displayMessage(currentNumber);
                    }
                }
                else if (msg == "sin")
                {
                    currentNumber = Convert.ToString(Math.Sin(x));
                    displayMessage(currentNumber);
                }
                else if (msg == "cos")
                {
                    currentNumber = Convert.ToString(Math.Cos(x));
                    displayMessage(currentNumber);
                }
                else if (msg == "tan")
                {
                    currentNumber = Convert.ToString(Math.Tan(x));
                    displayMessage(currentNumber);
                }
            }
            else
            {
                if (operation.Contains(msg))
                {
                    ProcessComputePending(msg, true);
                }
                else if (digits.Contains(msg))
                {
                    currentNumber = "";
                    previousNumber = "0";
                    ProcessAccumulateDigits(msg, true);
                }
                else if (unaryOperation.Contains(msg))
                {
                    ProcessComputeUnary(msg, true);
                }
                else if (equal.Contains(msg))
                {
                    ProcessCompute(msg, true);
                }
                else if (clear.Contains(msg))
                {
                    ProcessorZeroState(msg, true);
                }
            }
        }

        void ProcessRemoveDigit(string msg, bool income)
        {
            if (income)
            {
                currentState = State.RemoveDigit;
                if (currentNumber.Length > 0)
                {
                    currentNumber = currentNumber.Remove(currentNumber.Length - 1);
                }
                if (currentNumber.Length > 0)
                {
                    displayMessage(currentNumber);
                }
                else
                {
                    displayMessage("0");
                }
            }
            else
            {
                if (digits.Contains(msg))
                {
                    if (currentNumber.IndexOf(",") == -1)
                    {
                        ProcessAccumulateDigits(msg, true);
                    }
                    else
                    {
                        ProcessAccumulateDigitsWithDecimal(msg, true);
                    }
                }
                else if (arrow.Contains(msg))
                {
                    ProcessRemoveDigit(msg, true);
                }
                else if (equal.Contains(msg))
                {
                    if (currentNumber == "")
                    {
                        currentNumber = "0";
                    }
                    ProcessCompute(msg, true);
                }
                else if (operation.Contains(msg))
                {
                    if (currentNumber == "")
                    {
                        currentNumber = "0";
                    }
                    ProcessComputePending(msg, true);
                }
                else if (unaryOperation.Contains(msg))
                {
                    ProcessComputeUnary(msg, true);
                }
            }
        }
    }
}
