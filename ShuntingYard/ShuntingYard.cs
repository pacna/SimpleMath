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
                '^', 
                new OperatorPrecedenceModel() 
                {
                    Precedence = 3,
                    Associativity = AssociativityTypes.Right
                }
            },
            {
                '*',
                new OperatorPrecedenceModel()
                {
                    Precedence = 2,
                    Associativity = AssociativityTypes.Left
                }
                
            },
            {
                '/',
                new OperatorPrecedenceModel()
                {
                    Precedence = 2,
                    Associativity = AssociativityTypes.Left
                }
            },
            {
                '+',
                new OperatorPrecedenceModel()
                {
                    Precedence = 1,
                    Associativity = AssociativityTypes.Left
                }
            },
            {
                '-',
                new OperatorPrecedenceModel()
                {
                    Precedence = 1,
                    Associativity = AssociativityTypes.Left
                }
            }
        };

        private readonly char[] trimmer = {'\0'};
        public ShuntingYard(string input)
        {
            input = RemoveWhitespace(input);
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

        private bool IsThereAnyOperatorInStack()
        {
            if(_operatorStack.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
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
            return incomingOp != '(';
        }

        private bool IsRightParenthesis(char incomingOp)
        {
            return incomingOp == ')';
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
        
        public void Output()
        {
            Console.WriteLine(_input.Length);
        }

        public string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

    }
}