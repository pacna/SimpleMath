using SimpleMath.PostfixMath.Models;
using SimpleMath.PostfixMath.Types;

namespace SimpleMath.PostfixMath;

public class ShuntingYard
{
    private readonly string[] _input;
    private readonly Stack<string> _operatorStack = new();
    private readonly Queue<string> _outputQueue = new();
    private IReadOnlyDictionary<string, OperatorPrecedenceModel> _operatorPrecedence = new Dictionary<string, OperatorPrecedenceModel>() {
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

    public string[] ToPostfix()
    {
        foreach (string token in _input)
        {
            if (Helper.IsNumber(input: token))
            {
                this._outputQueue.Enqueue(token);
                continue;
            }
            
            if (IsOperator(op: token))
            {
                while (IsThereAnyOperatorInStack() && HasHigherOrEqualPrecedence(incomingOp: token))
                {
                    string opFromStack = this._operatorStack.Pop();
                    this._outputQueue.Enqueue(opFromStack);
                }
                this._operatorStack.Push(token);
                continue;
            }
            
            if (!IsNotLeftParenthesis(incomingOp: token))
            {
                this._operatorStack.Push(token);
                continue;
            }
            
            if (IsRightParenthesis(incomingOp: token))
            {
                while (IsNotLeftParenthesis(incomingOp: this._operatorStack.Peek()))
                {
                    this._outputQueue.Enqueue(this._operatorStack.Pop());
                }
                // if it breaks out of the while loop then it must a left parenthesis. Remove it from the this._operatorStack
                this._operatorStack.Pop();
            }
        }

        while (this._operatorStack.Any())
        {
            this._outputQueue.Enqueue(this._operatorStack.Pop());
        }

        return GetPostfix();
    }

    private string[] GetPostfix()
    {
        List<string> postfix = new();
        foreach (string x in this._outputQueue)
        {
            postfix.Add(x);
        }

        return postfix.ToArray();
    }

    private bool IsThereAnyOperatorInStack()
    {
        return this._operatorStack.Any();
    }

    private bool HasHigherOrEqualPrecedence(string incomingOp)
    {
        if (IsNotLeftParenthesis(incomingOp: this._operatorStack.Peek()))
        {
            OperatorPrecedenceModel incomingOpPrecedence = this._operatorPrecedence[incomingOp];
            OperatorPrecedenceModel stackOpPrecedence = this._operatorPrecedence[this._operatorStack.Peek()];

            if (stackOpPrecedence.Precedence > incomingOpPrecedence.Precedence ||
                (incomingOpPrecedence.Precedence == stackOpPrecedence.Precedence
                    && incomingOpPrecedence.Associativity.Equals(AssociativityTypes.Left)))
            {
                return true;
            }

            return false;
        }

        return false;
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
        return this._operatorPrecedence.ContainsKey(op);
    }

    public double Evaluate()
    {
        Stack<double> outputStack = new Stack<double>();
        string[] postfix = ToPostfix();
        foreach (string x in postfix)
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
                    double result = leftOperand + rightOperand;
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
                else if (x == OperatorTypes.Division)
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