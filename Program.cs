using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace calc
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Введите выражение");
            string stroka = Console.ReadLine();
            var post = new ParseExpression();
            OperationResult result = post.result(stroka.Trim());
            if (result.Success == true)
            {
                Console.WriteLine("Ответ = " + result.Value.ToString());    
            }
            Console.ReadKey();

        }
    }


}


