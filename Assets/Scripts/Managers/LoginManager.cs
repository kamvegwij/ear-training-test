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
        errorInfoText.gameObject.SetActive(false);
    }
    
    IEnumerator ShowErrorMessage(string txt)
    {
        errorInfoText.gameObject.SetActive(true);
        errorInfoText.text = txt;
        yield return new WaitForSeconds(5f);
        errorInfoText.gameObject.SetActive(false);
        errorInfoText.text = "";
    }

    private void LoadGameData()
    {
        
        if (progress != null)
        {
            GameManager.totalXP = progress.totalXP;
            GameManager.username = progress.username;
            if (usernameInput.text == progress.username && passwordInput.text == "123")
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
