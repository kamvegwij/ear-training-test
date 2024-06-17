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
    [SerializeField] private TextMeshProUGUI generatedSoundText;
    [SerializeField] private List<GameObject> pianoKeysList;

    private float sineFrequency;
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

    private void GenerateAudio()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = .2f;
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0; //force 2D sound
        audioSource.panStereo = 1;

        //pick a random note on the keyboard piano.
        int keyBoardLen = pianoKeysList.Count;

        KeyNoteManager randomNote = pianoKeysList[Random.Range(0, keyBoardLen)].GetComponent<KeyNoteManager>();
        sineFrequency = randomNote.frequency;
        generatedSoundText.text = randomNote.noteType + "\n" + randomNote.noteColor;

        if (!audioSource.isPlaying)
        {
            //audioSource.Play();
        }
        
    }
    void OnAudioFilterRead(float[] data, int channels)
    {
        int timeIndex = 0;
        float sampleRate = 44100;
        float waveLengthInSeconds = 2.0f;
        

        for (int i = 0; i < data.Length; i += channels)
        {
            data[i] = CreateSine(timeIndex, sineFrequency, sampleRate);
            timeIndex++;

            //if timeIndex gets too big, reset it to 0
            if (timeIndex >= (sampleRate * waveLengthInSeconds))
            {
                timeIndex = 0;
            }
        }
    }

    //Creates a sinewave
    public float CreateSine(int timeIndex, float frequency, float sampleRate)
    {
        return Mathf.Sin(2 * Mathf.PI * timeIndex * frequency / sampleRate);
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
                    GenerateAudio();
                    break;
                case 1:
                    GenerateAudio();
                    break;
                case 2:
                    GenerateAudio();
                    break;
            }
        }
        else
        {
            Debug.Log("Timer Already Started!");
        }
    }
}
