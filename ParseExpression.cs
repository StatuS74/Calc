using System;
using System.Collections.Generic;
using System.Linq;

namespace calc
{
    class ParseExpression
    {
        private readonly List<string> operators;
        readonly MathSumbol simbol = new MathSumbol();
        readonly IMathOperations mathOperations = new Operations();

        public ParseExpression()
        {

            operators = new List<string>(simbol.GetAllSumbol());
        }

        public IEnumerable<string> Separate(string inputOperanrs)
        {
            //var inputSeparated = new List<string>();
            int position = 0;
            while (position < inputOperanrs.Length)
            {
                string singleOperand = string.Empty + inputOperanrs[position];
                if (!simbol.GetAllSumbol().Contains(inputOperanrs[position].ToString()))
                {
                    if (Char.IsDigit(inputOperanrs[position]))
                        for (int i = position + 1;
                             i < inputOperanrs.Length &&
                             (Char.IsDigit(inputOperanrs[i]) || inputOperanrs[i] == ',' || inputOperanrs[i] == '.');
                             i++)
                            singleOperand += inputOperanrs[i];
                    else if (Char.IsLetter(inputOperanrs[position]))
                        for (int i = position + 1;
                             i < inputOperanrs.Length &&
                             (Char.IsLetter(inputOperanrs[i]) || Char.IsDigit(inputOperanrs[i]));
                             i++)
                            singleOperand += inputOperanrs[i];
                }
                yield return singleOperand;
                position += singleOperand.Length;
            }
        }

        public IEnumerable<string> ConvertToPostfixNotation(string inputOperanrs)
        {
            var outputSeparated = new List<string>();
            var stack = new Stack<string>();
            foreach (string singleOperand in Separate(inputOperanrs))
            {
                if (operators.Contains(singleOperand))
                {
                    if (stack.Count > 0 && !singleOperand.Equals(simbol.DopGetBracketRight))
                    {
                        if (singleOperand.Equals(simbol.DopGetBracketLeft))
                        {
                            string singleBracket = stack.Pop();
                            while (singleBracket != simbol.DopGetBracketRight)
                            {
                                outputSeparated.Add(singleBracket);
                                singleBracket = stack.Pop();
                            }
                        }
                        else if (GetPriority(singleOperand) >= GetPriority(stack.Peek()))
                            stack.Push(singleOperand);
                        else
                        {
                            while (stack.Count > 0 && GetPriority(singleOperand) < GetPriority(stack.Peek()))
                                outputSeparated.Add(stack.Pop());
                            stack.Push(singleOperand);
                        }
                    }
                    else
                        stack.Push(singleOperand);
                }
                else
                    outputSeparated.Add(singleOperand);
            }
            if (stack.Count > 0)
                foreach (string c in stack)
                    outputSeparated.Add(c);

            return outputSeparated.ToArray();
        }
        private byte GetPriority(string operand)
        {
            if (simbol.DopGetBracketLeft == operand)
            {
                return 0;
            }
            if (simbol.DopGetBracketRight == operand)
            {
                return 1;
            }
            if (simbol.GetPlus == operand)
            {
                return 2;
            }
            if (simbol.GetMinus == operand)
            {
                return 3;
            }
            if ((simbol.GetMultiply == operand) || (simbol.GetSplit == operand))
            {
                return 4;
            }
            return 6;
        }

        public OperationResult result(string input)
        {
            var operationsResult = new OperationResult();
            Stack<string> stack = new Stack<string>();
            Queue<string> queue = new Queue<string>(ConvertToPostfixNotation(input));
            string valueOperand = queue.Dequeue();
            while (queue.Count >= 0)
            {
                if (!operators.Contains(valueOperand))
                {
                    stack.Push(valueOperand);
                    valueOperand = queue.Dequeue();
                }
                else
                {
                    decimal summ = 0;
                    decimal firstOperand;
                    decimal seconfOperand;
                    try
                    {
                        Operations operations = new Operations();

                        if (simbol.GetPlus == valueOperand)
                        {
                            firstOperand = Convert.ToDecimal(stack.Pop());
                            seconfOperand = Convert.ToDecimal(stack.Pop());
                            summ = mathOperations.Plus(firstOperand, seconfOperand);

                        }
                        if (simbol.GetMinus == valueOperand)
                        {
                            firstOperand = Convert.ToDecimal(stack.Pop());
                            seconfOperand = Convert.ToDecimal(stack.Pop());
                            summ = mathOperations.Minus(firstOperand, seconfOperand);

                        }
                        if (simbol.GetMultiply == valueOperand)
                        {
                            firstOperand = Convert.ToDecimal(stack.Pop());
                            seconfOperand = Convert.ToDecimal(stack.Pop());
                            summ = mathOperations.Multiply(firstOperand, seconfOperand);

                        }
                        if (simbol.GetSplit == valueOperand)
                        {
                            firstOperand = Convert.ToDecimal(stack.Pop());
                            seconfOperand = Convert.ToDecimal(stack.Pop());
                            summ = mathOperations.Div(firstOperand, seconfOperand);
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Не верно задано выражение. " + ex.Message);
                        operationsResult.Success = false;
                        return operationsResult;

                    }
                    stack.Push(summ.ToString());
                    if (queue.Count > 0)
                        valueOperand = queue.Dequeue();
                    else
                        break;
                }

            }
            operationsResult.Success = true;
            operationsResult.Value = Convert.ToDecimal(stack.Pop());
            return operationsResult;
        }




    }
}
