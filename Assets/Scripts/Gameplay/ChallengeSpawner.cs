using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ChallengeSpawner : MonoBehaviour
{
    [SerializeField] private Button tryButton;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI generatedSoundText;
    [SerializeField] private List<GameObject> pianoKeysList;
    [SerializeField] private GameObject testUI;

    private AudioSource audioSource;
    public KeyNoteManager randomNote;

    private float sineFrequency;
    private float timerDuration = 5.0f;
    private bool testStarted = false;
    private bool isActive = false;
    GameManager gameManager;
    private void OnEnable()
    {
        tryButton.onClick.AddListener(StartTest);
    }
    private void OnDisable()
    {
        tryButton.onClick.RemoveListener(StartTest);
    }
    private void Awake()
    {
        gameManager = new GameManager();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        testUI.SetActive(false);
    }
    private void Update()
    {
        if (!testStarted)
        {
            testUI.SetActive(false);
            gameManager.isPlaying = false;
            tryButton.interactable = true;
        }
        else
        {
            testUI.SetActive(true);
            gameManager.isPlaying = true;
            tryButton.interactable = false;

            if (timerDuration > 0.0f)
            {
                timerDuration -= Time.deltaTime;
            }
            else
            {
                audioSource.Stop();
                testStarted = false;
            }
        }
        timerText.text = Mathf.RoundToInt(timerDuration).ToString();
    }
    private void HandleRandomNote()
    {
        //pick a random note on the keyboard piano.
        int keyBoardLen = pianoKeysList.Count;
        randomNote = pianoKeysList[Random.Range(0, keyBoardLen)].GetComponent<KeyNoteManager>();
        sineFrequency = randomNote.frequency;
        generatedSoundText.text = randomNote.noteType;
        audioSource.Play();
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        int timeIndex = 0;
        float sampleRate = 44100;
        float waveLengthInSeconds = 1.0f;

        for (int i = 0; i < data.Length; i += channels)
        {
            data[i] = CreateSine(timeIndex, sineFrequency, sampleRate);
            timeIndex++;

            if (timeIndex >= (sampleRate * waveLengthInSeconds))
            {
                timeIndex = 0;
            }
        }
    }
    public float CreateSine(int timeIndex, float frequency, float sampleRate)
    {
        return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);
    }
    private void CheckGameMode()
    {
        switch (gameManager.gameMode)
        {
            case 0:
                HandleRandomNote();
                break;
            case 1:
                HandleRandomNote();
                break;
            case 2:
                HandleRandomNote();
                break;
        }
    }
    private void StartTest()
    {
        if (testStarted) return;

        timerDuration = 5.0f;
        testStarted = true;
        CheckGameMode();
    }
}
