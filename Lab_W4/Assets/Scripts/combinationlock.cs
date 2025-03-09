using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;  // Keep this to use Unity's UI Image
// using UnityEngine.UIElements;  // Remove this to avoid the ambiguity

public class combinationlock : MonoBehaviour
{
    [SerializeField] TMP_Text userInputText;

    [SerializeField] XrButtoninteractable[] comboButtons;

    [SerializeField] Image lockedPanel;  // This will now refer to UnityEngine.UI.Image

    [SerializeField] Color unlockedColor;

    [SerializeField] TMP_Text lockedText;

    private const string unlockedString = "unlocked";

    [SerializeField] bool isLocked;

    [SerializeField] int[] comboValues = new int[3];

    private int[] inputValues;

    private int maxButtonPresses;

    private int buttonPresses;

    void Start()
    {
        maxButtonPresses = comboValues.Length;
        inputValues = new int[comboValues.Length]; // Initialize inputValues first

        ResetUserValues();

        userInputText.text = "";

        for (int i = 0; i < comboButtons.Length; i++)
        {
            comboButtons[i].selectEntered.AddListener(OnComboButtonPressed);
        }
    }

    private void OnComboButtonPressed(SelectEnterEventArgs arg0)
    {
        if (buttonPresses >= maxButtonPresses)
        {
            // Too many button presses
            return;
        }

        for (int i = 0; i < comboButtons.Length; i++)
        {
            if (arg0.interactableObject.transform.name == comboButtons[i].transform.name)
            {
                userInputText.text = i.ToString(); // Fixed missing semicolon
                inputValues[buttonPresses] = i;
            }

            if (comboButtons[i] != null) // Ensure it exists before calling
            {
                comboButtons[i].ResetColor();
            }
        }

        buttonPresses++;
        if (buttonPresses == maxButtonPresses)
        {
            CheckCombo();
        }
    }

    private void CheckCombo()
    {
        int matches = 0;

        for (int i = 0; i < maxButtonPresses; i++)
        {
            if (inputValues[i] == comboValues[i])
            {
                matches++;
            }
        }

        if (matches == maxButtonPresses)
        {
            isLocked = false;
            lockedPanel.color = unlockedColor;
            lockedText.text = unlockedString;
        }
        else
        {
            ResetUserValues();
        }
    }

    private void ResetUserValues()
    {
        inputValues = new int[maxButtonPresses];
        userInputText.text = "";
        buttonPresses = 0;
    }
}
