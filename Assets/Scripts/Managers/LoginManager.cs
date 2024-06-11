using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TextMeshProUGUI errorInfoText;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button createNewButton;
    private UserData progress;

    private void Start()
    {
        progress = SaveData.LoadFromFile();

        continueButton.onClick.AddListener(LoadGameData);
        createNewButton.onClick.AddListener(CreateNew);
    }
    private void LoadGameData()
    {
        
        if (progress != null)
        {
            GameManager.totalXP = progress.totalXP;
            GameManager.username = progress.username;
            if (usernameInput.text == progress.username)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                StartCoroutine(ShowErrorMessage("Incorrect username! Please try again or create a new account."));
            }
        }
        else
        {
            errorInfoText.text = "No save file found!";
        }
    }
    private void CreateNew()
    {
        if ( usernameInput.text != progress.username)
        {
            GameManager.username = usernameInput.text;
            SaveData.SaveToFile();
            StartCoroutine(ShowErrorMessage("Created successfully, you can now continue"));
        }
        else
        {
            StartCoroutine(ShowErrorMessage("This user account already exists"));

        }
    }
    IEnumerator ShowErrorMessage(string txt)
    {
        errorInfoText.text = txt;
        yield return new WaitForSeconds(5f);
        errorInfoText.text = "";
    }
}
