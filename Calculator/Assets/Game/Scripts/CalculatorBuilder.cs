using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalculatorBuilder : MonoBehaviour
{
    [Header("Ui Components")]
    [SerializeField] TextMeshProUGUI screenDisplay; //text for Displaying the inputs
    [SerializeField] Button[] operators; //operators like +,-,*,/,.
    [SerializeField] Button[] numbers; // numbers
    [SerializeField] Button[] emptyButtons; //Empty Buttons Not in Use
    [SerializeField] Button allClear; // All Clear button
    [SerializeField] Button clear; //Button that clears Last Input
    [SerializeField] Button result; // Result Button

    [SerializeField] string[] operatorList; // List of operrators to display

    //Constructing The Calculator
    private void Awake()
    {
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
        screenDisplay.text += number.ToString();
    }

    //Inserts the Operator in the Display
    public void InsertOperator(string symbol)
    {
        screenDisplay.text += symbol.ToString();
    }

    //Clears the Expression in the Display
    public void OnClearingAll()
    {
        screenDisplay.text = "";
    }

    //Shows The Result
    public void OnShowResult()
    {
        Debug.Log("Show Result");
    }

    //Clear Last Input
    public void ClearCurrentInput()
    {
        if (!string.IsNullOrEmpty(screenDisplay.text))
        {
            screenDisplay.text = screenDisplay.text.Substring(0, screenDisplay.text.Length - 1);
        }
    }
}
