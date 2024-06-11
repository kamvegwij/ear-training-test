using System.Collections;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float timerDuration = 60.0f; //60s or 1 minute
    public float timerMin = 0.0f;

    public float timeLeft;
    public bool isStarted = false;

    [SerializeField] private TextMeshProUGUI timerUI;

    //timerUI.text = Mathf.RoundToInt(timeLeft).ToString();

    public void StartTimer()
    {
        StartCoroutine(HandleTimer());
    }

    IEnumerator HandleTimer()
    {
        if (!isStarted)
        {
            isStarted = true;
            timeLeft = timerDuration - timerMin;
        }
        while(isStarted && timeLeft > 0.0f) 
        {
            timeLeft -= Time.deltaTime;
            timerUI.text = Mathf.RoundToInt(timeLeft).ToString();
            yield return null;
        }
    }
}
