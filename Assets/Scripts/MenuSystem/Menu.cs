using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI errorInfoText;

    [SerializeField] private Button continueBtn;
    [SerializeField] private Button settingsBtn;

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject buttonSection;

    private void Start()
    {
        continueBtn.onClick.AddListener(ContinueToGame);
        settingsBtn.onClick.AddListener(OpenSettings);
        settingsMenu.SetActive(false);
        buttonSection.SetActive(true);
    }

    private void ContinueToGame()
    {
        SceneManager.LoadScene(2);
    }

    
    private void OpenSettings()
    {
        if (settingsMenu != null)
        {
            buttonSection.SetActive(false);
            settingsMenu.SetActive(true);
        }
    }
    public void CloseSettings()
    {
        buttonSection.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
