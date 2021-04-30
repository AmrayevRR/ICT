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

        enum State
        {
            Zero,
            AccumulateDigits,
            ComputePending,
            Compute,
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
                default:
                    break;
            }
        }

        void ProcessorZeroState(string msg, bool income)
        {
            if (income)
            {
                currentState = State.Zero;
                previousNumber = "0";
                displayMessage(msg);
            }
            else
            {
                if (nonZeroDigits.Contains(msg))
                {
                    ProcessAccumulateDigits(msg, true);
                }
                else if (zero.Contains(msg)) { }
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
            }
        }

        void ProcessCompute(string msg, bool income)
        {
            if (income)
            {
                currentState = State.Compute;

                int a = int.Parse(previousNumber);
                int b = int.Parse(currentNumber);

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
            }
        }
    }
}
