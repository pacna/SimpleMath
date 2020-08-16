using System;
using System.Collections.Generic;
using System.Linq;

namespace Terminal.Calculator
{
    public class ShuntingYard: IShuntingYard
    {
        private readonly char[] _input;
        private Stack<char> _operatorStack = new Stack<char>();
        private Queue<char> _outputQueue = new Queue<char>();
        private Dictionary<char, OperatorPrecedenceModel> _operatorPrecedence = new Dictionary<char, OperatorPrecedenceModel>() {
            {
                OperatorTypes.Power, 
                new OperatorPrecedenceModel() 
                {
                    Precedence = 3,
                    Associativity = AssociativityTypes.Right
                }
            },
            {
                OperatorTypes.Multiplication,
                new OperatorPrecedenceModel()
                {
                    Precedence = 2,
                    Associativity = AssociativityTypes.Left
                }
                
            },
            {
                OperatorTypes.Division,
                new OperatorPrecedenceModel()
                {
                    Precedence = 2,
                    Associativity = AssociativityTypes.Left
                }
            },
            {
                OperatorTypes.Addition,
                new OperatorPrecedenceModel()
                {
                    Precedence = 1,
                    Associativity = AssociativityTypes.Left
                }
            },
            {
                OperatorTypes.Subtraction,
                new OperatorPrecedenceModel()
                {
                    Precedence = 1,
                    Associativity = AssociativityTypes.Left
                }
            }
        };

        public ShuntingYard(string input)
        {
            this._input = input.ToCharArray();
        }

        public void ConvertToPostfix()
        {
            foreach (var token in _input)
            {
                if (Char.IsDigit(token))
                {
                    _outputQueue.Enqueue(token);
                }
                else if (IsOperator(token))
                {
                    while (IsThereAnyOperatorInStack() && hasHigherOrEqualPrecedence(token))
                    {
                        char opFromStack = _operatorStack.Pop();
                        _outputQueue.Enqueue(opFromStack);
                    }
                    _operatorStack.Push(token);
                }
                else if (!IsNotLeftParenthesis(token))
                {
                    _operatorStack.Push(token);
                }
                else if (IsRightParenthesis(token))
                {
                    while (IsNotLeftParenthesis(_operatorStack.Peek()))
                    {
                        _outputQueue.Enqueue(_operatorStack.Pop());
                    }
                    // if it breaks out of the while loop then it must a left parenthesis. Remove it from the _operatorStack
                    _operatorStack.Pop();
                }
            }

            while (_operatorStack.Any())
            {
                _outputQueue.Enqueue(_operatorStack.Pop());
            }
        }

        public string GetPostfix()
        {
            string postfixString = "";
            foreach(var x in _outputQueue)
            {
                postfixString = postfixString + x;
            }
            
            return postfixString;
        }

        private bool IsThereAnyOperatorInStack()
        {
            return _operatorStack.Any();
        }

        private bool hasHigherOrEqualPrecedence(char incomingOp)
        {
            if (IsNotLeftParenthesis(_operatorStack.Peek()))
            {
                OperatorPrecedenceModel incomingOpPrecedence = _operatorPrecedence[incomingOp];
                OperatorPrecedenceModel stackOpPrecedence = _operatorPrecedence[_operatorStack.Peek()];

                if (stackOpPrecedence.Precedence > incomingOpPrecedence.Precedence || 
                    (incomingOpPrecedence.Precedence == stackOpPrecedence.Precedence 
                        && incomingOpPrecedence.Associativity.Equals(AssociativityTypes.Left)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool IsNotLeftParenthesis(char incomingOp)
        {
            return incomingOp != OperatorTypes.LeftParenthesis;
        }

        private bool IsRightParenthesis(char incomingOp)
        {
            return incomingOp == OperatorTypes.RightParenthesis;
        }

        private bool IsOperator(char op)
        {
            bool doesExist = _operatorPrecedence.ContainsKey(op);
            return doesExist;
        }

        public void PrintQueue()
        {
            char[] result = _outputQueue.ToArray();
            for(var i = 0; i < result.Length; i++)
            {
                if (i == result.Length - 1)
                {
                    Console.Write($"{result[i]} \n");
                }
                else
                {
                    Console.Write($"{result[i]}");
                }
            }
        }

        public void PrintStack()
        {
            char[] result = _operatorStack.ToArray();
            for(var i = 0; i < result.Length; i++)
            {
                if (i == result.Length - 1)
                {
                    Console.Write($"{result[i]} \n");
                }
                else
                {
                    Console.Write($"{result[i]}");
                }
            }
        }
        
        public double Evaluate()
        {
            Stack<double> outputStack = new Stack<double>();
            char[] postfix = GetPostfix().ToCharArray();
            foreach(var x in postfix)
            {
                if (Char.IsDigit(x))
                {
                    double operand = Convert.ToDouble(Char.GetNumericValue(x));
                    outputStack.Push(operand);
                }
                else
                {
                    /**
                        34+ (reading from left to right when evaluating)
                        rightOperand is 4
                        leftOperand is 3
                    **/
                    double rightOperand = outputStack.Pop();
                    double leftOperand = outputStack.Pop();

                    if (x == OperatorTypes.Addition)
                    {
                        double result =  leftOperand + rightOperand;
                        outputStack.Push(result);
                    }
                    else if (x == OperatorTypes.Subtraction)
                    {
                        double result = leftOperand - rightOperand;
                        outputStack.Push(result);
                    }
                    else if (x == OperatorTypes.Multiplication)
                    {
                        double result = leftOperand * rightOperand;
                        outputStack.Push(result);
                    }
                    else if ( x == OperatorTypes.Division)
                    {
                        double result = leftOperand / rightOperand;
                        outputStack.Push(result);
                    }
                    else 
                    {// this must be pow
                        double result = Math.Pow(leftOperand, rightOperand);
                        outputStack.Push(result);
                    }
                }
            }
            
            return outputStack.Peek();
        }
    }
}