using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button logoutBtn;
    [SerializeField] private Button saveBtn;

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject buttonSection;

    private void OnEnable()
    {
        continueBtn.onClick.AddListener(ContinueToGame);
        settingsBtn.onClick.AddListener(OpenSettings);
        logoutBtn.onClick.AddListener(Logout);
        saveBtn.onClick.AddListener(CloseSettings);
    }
    private void OnDisable()
    {
        continueBtn.onClick.RemoveListener(ContinueToGame);
        settingsBtn.onClick.RemoveListener(OpenSettings);
        logoutBtn.onClick.RemoveListener(Logout);
        saveBtn.onClick.RemoveListener(CloseSettings);
    }
    private void Start()
    {
        settingsMenu.SetActive(false);
        buttonSection.SetActive(true);
    }

    private void Logout()
    {
        SceneManager.LoadScene(0);
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
