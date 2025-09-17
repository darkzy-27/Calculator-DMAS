using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Calc
{
    public class CalculatorBuilder : MonoBehaviour
    {
        [Header("Calculation Script")]
        [SerializeField] Calculator calculator;

        [Header("Ui Components")]
        [SerializeField] TextMeshProUGUI screenDisplay; //text for Displaying the inputs
        [SerializeField] Button[] operators; //operators like +,-,*,/,.
        [SerializeField] Button[] numbers; // numbers
        [SerializeField] Button[] emptyButtons; //Empty Buttons Not in Use
        [SerializeField] Button allClear; // All Clear button
        [SerializeField] Button clear; //Button that clears Last Input
        [SerializeField] Button result; // Result Button

        [SerializeField] string[] operatorList; // List of operrators to display

        private string lastInput = "";

        private void Awake()
        {
            //Constructing The Calculator
            ConstructingCalculator();
        }

        public void ConstructingCalculator()
        {
            //Removing Text From the display;
            screenDisplay.text = "";

            //Assigning numbers
            for (int i = 0; i < numbers.Length; i++)
            {
                int index = i;
                numbers[i].onClick.AddListener(() => InsertNumber(index));
                numbers[i].GetComponentInChildren<TextMeshProUGUI>().text = index.ToString();
            }

            //Assigning operators
            for (int i = 0; i < operators.Length; i++)
            {
                string symbol = operatorList[i];
                operators[i].onClick.AddListener(() => InsertOperator(symbol));
                operators[i].GetComponentInChildren<TextMeshProUGUI>().text = symbol;
            }

            //Assigning Empty Buttons
            foreach (Button button in emptyButtons)
            {
                button.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }

            //Assigning Clear Button
            clear.onClick.AddListener(() => ClearCurrentInput());
            clear.GetComponentInChildren<TextMeshProUGUI>().text = "X";


            //Assigning All Clear Button
            allClear.onClick.AddListener(() => OnClearingAll());
            allClear.GetComponentInChildren<TextMeshProUGUI>().text = "AC";

            //Assigning Result Button
            result.onClick.AddListener(() => OnShowResult());
            result.GetComponentInChildren<TextMeshProUGUI>().text = "=";
        }

        //Inserts The Numbers in the Display
        public void InsertNumber(int number)
        {
            Debug.Log(lastInput);
            lastInput = number.ToString();
            screenDisplay.text += number.ToString();
        }

        //Inserts the Operator in the Display
        public void InsertOperator(string symbol)
        {
            Debug.Log("Last input: " + lastInput);

            //Edge Case : to check if first character of the string shouldnt be a +/-
            if (string.IsNullOrEmpty(screenDisplay.text) || screenDisplay.text.Length == 1)
            {
                //if it is the ignore
                if (symbol != "-" && symbol != ".")
                {
                    return;
                }
            }

            //Edge Case : to check if the last input is operator and the next input is also a operator so replaces the last input by current input
            if (!string.IsNullOrEmpty(lastInput) && IsOperator(lastInput))
            {
                ReplaceLastCharacter(symbol);
            }
            //Edge Case : to ignore if decimal is already present and we cant let them to put decimal again
            else if (lastInput == ".")
            {
                //Ignore
            }
            else
            {
                screenDisplay.text += symbol;
            }
            lastInput = symbol;
        }

        //Clears the Expression in the Display
        public void OnClearingAll()
        {
            screenDisplay.text = "";
            lastInput = "";
        }

        //Shows The Result
        public void OnShowResult()
        {
            Debug.Log("Show Result");
            screenDisplay.text = calculator.CalculateTheExpression(screenDisplay.text);

            //After the result updating the last input according to the result
            if (screenDisplay.text.Length > 0)
            {
                lastInput = screenDisplay.text[screenDisplay.text.Length - 1].ToString();
            }
            else
            {
                lastInput = "";
            }
        }

        //Clear Last Input
        public void ClearCurrentInput()
        {
            //this is to remove the current input as well as updating the last input
            if (!string.IsNullOrEmpty(screenDisplay.text))
            {
                screenDisplay.text = screenDisplay.text.Substring(0, screenDisplay.text.Length - 1);

                if (screenDisplay.text.Length > 0)
                {
                    lastInput = screenDisplay.text[screenDisplay.text.Length - 1].ToString();
                }
                else
                {
                    lastInput = "";
                }
            }
        }

        //Replaces Last Character
        public void ReplaceLastCharacter(string newChar)
        {
            if (!string.IsNullOrEmpty(screenDisplay.text))
            {
                screenDisplay.text = screenDisplay.text.Substring(0, screenDisplay.text.Length - 1) + newChar;
            }
        }

        //Check For Operators
        private bool IsOperator(string input)
        {
            return input == "+" || input == "-" || input == "*" || input == "/" || input == ".";
        }
    }
}
