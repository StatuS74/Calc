using System;
using System.Collections.Generic;


namespace calc
{
    class Operations:IMathOperations
    {

       decimal IMathOperations.Plus(decimal firstOperand, decimal secondOperand)
        {
            decimal summ = firstOperand + secondOperand;
            return summ;
        }
       decimal IMathOperations.Minus(decimal firstOperand, decimal secondOperand)
        {
            decimal summ = firstOperand - secondOperand;
            return summ;
        }
       decimal IMathOperations.Multiply(decimal firstOperand, decimal secondOperand)
        {
            decimal summ = firstOperand * secondOperand;
            return summ;
        }
       decimal IMathOperations.Div(decimal firstOperand, decimal secondOperand)
        {
            if (secondOperand == 0)
            {
               throw new Exception("Деление на ноль");
            }
            decimal summ = firstOperand / secondOperand;
            return summ;
        }
    }

    public class MathSumbol
    {
        
        public string GetPlus
        {
            get { return "+"; }
        }

        public string GetMinus
        {
            get { return "-"; }
        }

        public string GetMultiply
        {
            get { return "*"; }
        }

        public string GetSplit
        {
            get { return "/"; }
        }

        public string DopGetBracketLeft 
        {
            get { return ")"; }
        }

        public string DopGetBracketRight
        {
            get { return "("; }
        }

        public  List<string> GetAllSumbol()
        {
            var allSumbol = new List<string>();
            allSumbol.Add(GetPlus);
            allSumbol.Add(GetMinus);
            allSumbol.Add(GetMultiply);
            allSumbol.Add(GetSplit);
            allSumbol.Add(DopGetBracketLeft);
            allSumbol.Add(DopGetBracketRight);
            return allSumbol;
        }
    }
}
