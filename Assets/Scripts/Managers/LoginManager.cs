using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TextMeshProUGUI errorInfoText;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button createNewButton;
    [SerializeField] private Button facebookButton;
    [SerializeField] private Button twitterButton;
    [SerializeField] private Button websiteButton;

    [SerializeField] private GameObject errorContainer;

    UserData progress;
    GameManager gameManager;
    private void OnEnable()
    {
        continueButton.onClick.AddListener(LoadGameData);
        createNewButton.onClick.AddListener(CreateNew);
        facebookButton.onClick.AddListener(OpenFacebook);
        twitterButton.onClick.AddListener(OpenTwitter);
        websiteButton.onClick.AddListener(OpenWebsite);
    }
    private void OnDisable()
    {
        continueButton.onClick.RemoveListener(LoadGameData);
        createNewButton.onClick.RemoveListener(CreateNew);
        facebookButton.onClick.RemoveListener(OpenFacebook);
        twitterButton.onClick.RemoveListener(OpenTwitter);
        websiteButton.onClick.RemoveListener(OpenWebsite);
    }

    private void Start()
    {
        gameManager = new GameManager();

        progress = SaveData.LoadFromFile();
        errorContainer.SetActive(false);
    }
    
    IEnumerator ShowErrorMessage(string txt)
    {
        errorContainer.SetActive(true);
        errorInfoText.text = txt;
        yield return new WaitForSeconds(5f);
        errorContainer.SetActive(false);
        errorInfoText.text = "";
    }

    private void LoadGameData()
    {
        
        if (progress != null)
        {
            gameManager.totalXP = progress.totalXP;
            gameManager.username = progress.username;
            gameManager.password = progress.password;
            gameManager.gameMode = progress.gameMode;

            if (usernameInput.text == progress.username && passwordInput.text == progress.password)
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
            gameManager.username = usernameInput.text;
            gameManager.password = passwordInput.text;
            SaveData.SaveToFile();
            StartCoroutine(ShowErrorMessage("Created successfully, you can now continue"));
        }
        else
        {
            StartCoroutine(ShowErrorMessage("This user account already exists"));

        }
    }

    private void OpenTwitter()
    {
        LogMessages("Opening Twitter...");
    }
    private void OpenFacebook()
    {
        LogMessages("Opening Facebook...");
    }
    private void OpenWebsite()
    {
        LogMessages("Opening Website...");
    }
    private void LogMessages(string message)
    {
        Debug.Log(message);
    }
}
