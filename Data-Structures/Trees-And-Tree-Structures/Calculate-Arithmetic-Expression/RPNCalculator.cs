using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Calculate_Arithmetic_Expression
{
    /// <summary>
    /// Simple calculator using Reversed Polish Notation.
    /// </summary>
    class RPNCalculator
    {
        static readonly string[] OperatorsInPriorityOrder = new string[]
                { "(", "*", "/", "+", "-", ")" };
        static readonly ISet<string> Operators =
                new HashSet<string>(OperatorsInPriorityOrder);

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var expressionTokens = Console.ReadLine()
                .Replace("(", "( ")
                .Replace(")", " )")
                .Split(new string[] { " " },
                    StringSplitOptions.RemoveEmptyEntries);

            var tokensInPostfixOrder = ConvertToPostfix(expressionTokens);

            Console.WriteLine(CalculateRPN(tokensInPostfixOrder));
        }

        /// <summary>
        /// Infix to postfix conversion.
        /// </summary>
        /// <param name="tokens">tokens in infix</param>
        /// <returns>tokens in postfix</returns>
        static IList<string> ConvertToPostfix(string[] tokens)
        {
            var tokensInPostfix = new List<string>();
            var operatorsStack = new Stack<string>();
            foreach (var token in tokens)
            {
                if (IsOperator(token))
                {

                    while (operatorsStack.Count > 0
                        && HasLowerPriority(token, operatorsStack.Peek()))
                    {
                        var operation = operatorsStack.Peek();
                        if (operation.Equals("("))
                        {
                            if (token.Equals(")"))
                            { operatorsStack.Pop(); }

                            break;
                        }

                        tokensInPostfix.Add(operatorsStack.Pop());
                    }

                    if (token.Equals(")"))
                    { continue; }

                    operatorsStack.Push(token);
                }
                else
                {
                    tokensInPostfix.Add(token);
                }
            }

            while (operatorsStack.Count > 0)
            { tokensInPostfix.Add(operatorsStack.Pop()); }

            return tokensInPostfix;
        }

        private static bool HasLowerPriority(
            string nextOperator, string stackTopOperator)
        {
            int i = 0;
            while (!nextOperator.Equals(OperatorsInPriorityOrder[i]))
            {
                if (stackTopOperator.Equals(OperatorsInPriorityOrder[i++]))
                { return true; }
            }

            return false;
        }

        private static bool IsOperator(string token)
        {
            return Operators.Contains(token);
        }

        /// <summary>
        /// Calculates using RPN.
        /// </summary>
        /// <param name="tokensInPostfix"></param>
        /// <returns>final result</returns>
        static decimal CalculateRPN(IList<string> tokensInPostfix)
        {
            Stack<decimal> stack = new Stack<decimal>();
            decimal number;

            foreach (string token in tokensInPostfix)
            {
                if (decimal.TryParse(token, out number))
                {
                    stack.Push(number);
                }
                else
                {
                    switch (token)
                    {
                        case "*":
                            {
                                stack.Push(stack.Pop() * stack.Pop());
                                break;
                            }
                        case "/":
                            {
                                number = stack.Pop();
                                stack.Push(stack.Pop() / number);
                                break;
                            }
                        case "+":
                            {
                                stack.Push(stack.Pop() + stack.Pop());
                                break;
                            }
                        case "-":
                            {
                                number = stack.Pop();
                                stack.Push(stack.Pop() - number);
                                break;
                            }
                        default:
                            throw new ArithmeticException(
                                string.Format("\"{0}\" is not a supported operation.", token));
                    }
                }
            }

            return stack.Pop();
        }
    }
}
