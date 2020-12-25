using System;
using System.Collections.Generic;
using System.Linq;

namespace Terminal.Calculator
{
    public class ShuntingYard: IShuntingYard
    {
        private readonly string[] _input;
        private Stack<string> _operatorStack = new Stack<string>();
        private Queue<string> _outputQueue = new Queue<string>();
        private Dictionary<string, OperatorPrecedenceModel> _operatorPrecedence = new Dictionary<string, OperatorPrecedenceModel>() {
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
            this._input = Helper.SplitOperatorsAndNumbers(input: input);
        }

        public void ConvertToPostfix()
        {
            foreach (var token in _input)
            {
                if (Helper.IsNumber(input: token))
                {
                    _outputQueue.Enqueue(token);
                }
                else if (IsOperator(op: token))
                {
                    while (IsThereAnyOperatorInStack() && HasHigherOrEqualPrecedence(incomingOp: token))
                    {
                        string opFromStack = _operatorStack.Pop();
                        _outputQueue.Enqueue(opFromStack);
                    }
                    _operatorStack.Push(token);
                }
                else if (!IsNotLeftParenthesis(incomingOp: token))
                {
                    _operatorStack.Push(token);
                }
                else if (IsRightParenthesis(incomingOp: token))
                {
                    while (IsNotLeftParenthesis(incomingOp: _operatorStack.Peek()))
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

        public string[] GetPostfix()
        {
            List<string> postfix = new List<string>();
            foreach(var x in _outputQueue)
            {
                postfix.Add(x);
            }
            
            return postfix.ToArray();
        }

        private bool IsThereAnyOperatorInStack()
        {
            return _operatorStack.Any();
        }

        private bool HasHigherOrEqualPrecedence(string incomingOp)
        {
            if (IsNotLeftParenthesis(incomingOp: _operatorStack.Peek()))
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

        private bool IsNotLeftParenthesis(string incomingOp)
        {
            return incomingOp != OperatorTypes.LeftParenthesis;
        }

        private bool IsRightParenthesis(string incomingOp)
        {
            return incomingOp == OperatorTypes.RightParenthesis;
        }

        private bool IsOperator(string op)
        {
            bool doesExist = _operatorPrecedence.ContainsKey(op);
            return doesExist;
        }
        
        public double Evaluate()
        {
            Stack<double> outputStack = new Stack<double>();
            string[] postfix = GetPostfix();
            foreach(var x in postfix)
            {
                if (Helper.IsNumber(input: x))
                {
                    double operand = Convert.ToDouble(x);
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