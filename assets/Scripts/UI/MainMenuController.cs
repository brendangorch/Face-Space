using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public TMP_InputField nameField;
    public Button startButton;

    // static variable for name
    public static string playerName;

    void Start()
    {
        // call validate button on start
        ValidateNameField(nameField.text);

        // add listener to the input vield
        nameField.onValueChanged.AddListener(ValidateNameField);
    }

    void ValidateNameField(string input)
    {
        // enable button if name entered
        startButton.interactable = input.Length != 0;
    }

    #region Button OnClick Funcs

    // start game button
    public void StartGame()
    {
        // set the player name to be used in game scene
        playerName = nameField.text;

        // open game scene 
        SceneManager.LoadScene("LevelScene");

    }

    // exit button
    public void Exit()
    {
        Application.Quit();
    }

    #endregion
}
