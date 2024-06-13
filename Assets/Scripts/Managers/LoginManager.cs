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

    private UserData progress;

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
        progress = SaveData.LoadFromFile();
        errorContainer.SetActive(false);
        Debug.Log("Username: " + progress.username);
        Debug.Log("password: " + progress.password);
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
            GameManager.totalXP = progress.totalXP;
            GameManager.username = progress.username;
            GameManager.password = progress.password;
            GameManager.gameMode = progress.gameMode;

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
            GameManager.username = usernameInput.text;
            GameManager.password = passwordInput.text;
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
        Debug.Log("Opening Twitter...");
    }
    private void OpenFacebook()
    {
        Debug.Log("Opening Facebook...");
    }
    private void OpenWebsite()
    {
        Debug.Log("Opening Website...");
    }
    
}
