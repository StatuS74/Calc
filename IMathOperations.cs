using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace calc
{
    interface IMathOperations
    {
         decimal Plus(decimal firstOperand, decimal secondOperand);
         decimal Minus(decimal firstOperand, decimal secondOperand);
         decimal Multiply(decimal firstOperand, decimal secondOperand);
         decimal Div(decimal firstOperand, decimal secondOperand);

    }
}
