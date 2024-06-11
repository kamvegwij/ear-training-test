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
    [SerializeField] private Button loadBtn;

    private void Start()
    {
        continueBtn.onClick.AddListener(ContinueToGame);
        loadBtn.onClick.AddListener(LoadGameData);
    }

    private void ContinueToGame()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadGameData()
    {
        UserData progress = SaveData.LoadFromFile();
        if (progress != null ) 
        {
            errorInfoText.text = "Save file found, loading data..";
            GameManager.totalXP = progress.totalXP;
            GameManager.username = progress.username;
        }
        else
        {
            errorInfoText.text = "No save file found!";
        }
    }
}
