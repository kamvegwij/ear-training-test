using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeSpawner : MonoBehaviour
{
    [SerializeField] private Button tryButton;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Start()
    {
        tryButton.onClick.AddListener(StartTest);

    }
    private void StartTest()
    {
        Debug.Log("Start test....");
        timerText.text = "0:05s";
    }
}
