using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavBar : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        saveButton.onClick.AddListener(SaveGameplay);
        menuButton.onClick.AddListener(GoToMenu);
    }

    private void SaveGameplay()
    {
        SaveData.SaveToFile();
    }
    private void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
