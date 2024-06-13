using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeSpawner : MonoBehaviour
{
    [SerializeField] private Button tryButton;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private List<GameObject> pianoKeysList; 

    private float timerDuration = 15.0f;
    private bool testStarted = false;

    private void Start()
    {
        tryButton.onClick.AddListener(StartTest);
    }
    private void Update()
    {
        if (!testStarted)
        {
            GameManager.isPlaying = false;
            tryButton.interactable = true;
            statusText.text = "Not Started";
        }
        else
        {
            GameManager.isPlaying = true;
            tryButton.interactable = false;
            if (timerDuration > 0.0f)
            {
                timerDuration -= Time.deltaTime;
                statusText.text = "Started";
            }
            else
            {
                testStarted = false;
            }
        }
        timerText.text = Mathf.RoundToInt(timerDuration).ToString();
    }

    private void StartTest()
    {
        if (!testStarted)
        {
            timerDuration = 15.0f;
            testStarted = true;

            //randomly generate an audiosource and play a random note from the key note list.
            switch (GameManager.gameMode)
            {
                case 0:
                    Debug.Log("Spawn easy audio");
                    break;
                case 1:
                    Debug.Log("Spawn tricky audio");
                    break;
                case 2:
                    Debug.Log("Spawn advanced audio");
                    break;
            }
        }
        else
        {
            Debug.Log("Timer Already Started!");
        }
    }
}
