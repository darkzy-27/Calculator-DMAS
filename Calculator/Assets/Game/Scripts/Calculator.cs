using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Calc
{
    public class Calculator : MonoBehaviour
    {
        public List<string> extractedExpression = new List<string>(); //temperory list for the calculation
        public List<string> operatorOrder = new List<string>(); // Operator Order

        [ContextMenu("Show")]
        public string CalculateTheExpression(string expression)
        {
            //Clearing The List Before Starting
            extractedExpression.Clear();

            //Spliting The Expression
            extractedExpression = SplitExpressions(expression);

            //Handling Actual Calculations
            HandlingOpretionalCalculations(extractedExpression);

            //Get The Answer
            return extractedExpression[0];
        }

        //Spliting the expression in a List
        public List<string> SplitExpressions(string currentExpression)
        {
            //Trimming any operators in the end
            currentExpression = currentExpression.TrimEnd('+', '-', '*', '/', '.');

            //trimming and operators in the start except -
            currentExpression = currentExpression.TrimStart('+', '*', '/');

            //Check if the first operator is minus
            bool isFirstCharacterMinus = currentExpression.Length > 0 && currentExpression[0] == '-';

            //A temporary List To Return
            List<string> seperatedExpression = new List<string>();

            //A temporary number to add the numbers
            string number = "";

            //If First character is minus then make it count as a number
            if (isFirstCharacterMinus)
            {
                number = "-";
                currentExpression = currentExpression.Remove(0, 1);
            }

            //Looping throught the expression so that we can seperate the numbers and the operators
            foreach(char character in currentExpression)
            {
                //checking if the current character is a number or a decimal so that we can sperate them as one entry
                if(char.IsDigit(character) || character == '.')
                {
                    //adding a number till we reach the operator or end
                    number += character;
                }
                else //if the current character is not a number (probablly a operator)
                {
                    if(!string.IsNullOrEmpty(number))
                    {
                        //if the number is not empty then we add it in the list
                        seperatedExpression.Add(number);

                        //reseting the number for further use
                        number = "";
                    }

                    //checking if the next operator is one of these + - / * and add them into string
                    if("+-*/".Contains(character))
                    {
                        //adding it in list
                        seperatedExpression.Add(character.ToString());
                    }
                }
            }

            //this is to close the expression basically ending cause rightnow we end the stuff if the character is an operator so if the string is empties or over then we add it in the expreression
            if(!string.IsNullOrEmpty(number))
            {
                //Adding it in the list
                seperatedExpression.Add(number);
            }

            //returing the seperated Expression
            return seperatedExpression;
        }

        //Handling the Operation Calculations
        public void HandlingOpretionalCalculations(List<string> expressions)
        {
            //A temporary List
            List<string> tempExpression = expressions;

            // This will loop all the operators in oder which are filled in the inspector
            for(int i = 0; i < operatorOrder.Count; i++)
            {
                //looping throught the expression to check if it contains the current ordered operator
                for (int j = 0; j < tempExpression.Count; j++)
                {
                    //if ordered operator is found
                    if (tempExpression[j] == operatorOrder[i])
                    {
                        //we can give the values to this function which will solve the current part like + - / * between two numbers and return the number and replace it with the left number
                        tempExpression[j - 1] = ReturnSolvedNumber(double.Parse(tempExpression[j - 1]), double.Parse(tempExpression[j + 1]), operatorOrder[i]);

                        //removing the operator and the right number
                        tempExpression.RemoveAt(j);
                        tempExpression.RemoveAt(j);

                        //going a step back basically the left number so that it can again check if we can divide by next number
                        j--;
                    }
                }
            }

            extractedExpression = tempExpression;
        }


        //Function taking the left number and right number and operator value to give the solved Expression
        public string ReturnSolvedNumber(double LeftNumber, double RightNumber, string Currentoperator)
        {
            string number = "";

            switch(Currentoperator)
            {
                case "+":
                    number = (LeftNumber + RightNumber).ToString();
                    break;
                case "-":
                    number = (LeftNumber - RightNumber).ToString();
                    break;
                case "*":
                    number = (LeftNumber * RightNumber).ToString();
                    break;
                case "/":
                    number = (LeftNumber / RightNumber).ToString();
                    break;
                default:
                    break;
            }

            return number;
        }
    }
}
