using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TreeEditor.TreeEditorHelper;

[RequireComponent(typeof(AudioSource))]
public class ChallengeSpawner : MonoBehaviour
{
    [SerializeField] private Button tryButton;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI generatedSoundText;
    [SerializeField] private List<GameObject> pianoKeysList;
    [SerializeField] private GameObject testUI;

    private AudioSource audioSource;

    private float timerDuration = 5.0f;
    private bool testStarted = false;

    ProceduralAudio generatedAudio;
    private void OnEnable()
    {
        tryButton.onClick.AddListener(StartTest);
    }
    private void OnDisable()
    {
        tryButton.onClick.RemoveAllListeners();
    }
    private void Awake()
    {
        generatedAudio = new ProceduralAudio(AudioSettings.outputSampleRate, 0.5f);
        audioSource = GetComponent<AudioSource>();

        audioSource.Stop();
        audioSource.playOnAwake = false;
        audioSource.loop = true;
    }
    private void Start()
    {
        testUI.SetActive(false);
    }
    private void Update()
    {
        if (!testStarted)
        {
            ToggleTestGameState(false, false, true);
        }
        else
        {
            ToggleTestGameState(true, true, false);

            if (timerDuration > 0.0f)
            {
                timerDuration -= Time.deltaTime;
            }
            else
            {
                audioSource.Stop();
                testStarted = false;
                MessageLog.LogMessage(GameManager.totalXP.ToString());
            }
        }
        timerText.text = Mathf.RoundToInt(timerDuration).ToString();
    }
    private void ToggleTestGameState(bool testState, bool isPlaying, bool buttonInteract)
    {
        testUI.SetActive(testState);
        GameManager.SetGamePlayingState(isPlaying);
        tryButton.interactable = buttonInteract;
    }
    public KeyNoteManager GetRandomNote()
    {
        //return a piano note object.
        int keyBoardLen = pianoKeysList.Count;
        KeyNoteManager randomNote = pianoKeysList[Random.Range(0, keyBoardLen)].GetComponent<KeyNoteManager>();
        generatedAudio.frequency = randomNote.currentFrequency;

        return randomNote;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        double phaseIncrement = generatedAudio.frequency / generatedAudio.sampleRate;

        for (int sample = 0; sample < data.Length; sample += channels)
        {
            float val = generatedAudio.amplitude * Mathf.Sin((float)generatedAudio.phase * 2 * Mathf.PI);
            generatedAudio.phase = (generatedAudio.phase + phaseIncrement) % 1;
            for (int channel = 0; channel < channels; channel++)
            {
                data[sample + channel] = val;
            }
        }
    }
    private void PlayChallengeAudio()
    {
        generatedSoundText.text = GetRandomNote().noteType;
        audioSource.Play();
    }

    private void StartTest()
    {
        if (testStarted) return;

        timerDuration = 5.0f;
        testStarted = true;

        switch (GameManager.gameMode)
        {
            case 0:
                PlayChallengeAudio();
                break;
            case 1:
                PlayChallengeAudio();
                break;
            case 2:
                PlayChallengeAudio();
                break;
        }
    }
}
