using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TreeEditor.TreeEditorHelper;
using static UnityEditorInternal.VersionControl.ListControl;

[RequireComponent(typeof(AudioSource))]
public class ChallengeSpawner : MonoBehaviour
{
    public QuestionGenerated[] questions;
    public List<QuestionGenerated> unansweredQuestions;
    public QuestionGenerated currentQuestion;

    [SerializeField] private Button tryButton;
    [SerializeField] private Button reloadButton;
    [SerializeField] private Button closeChallengeButton;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI generatedSoundText;
    [SerializeField] private List<GameObject> pianoKeysList;
    [SerializeField] private GameObject testUI;
    [SerializeField] private GameObject challengeChoiceMenu;

    private AudioSource audioSource;

    private float timerDuration = 5.0f;
    [SerializeField] private bool testStarted = false;
    [SerializeField] private bool makingChoice = false;

    ProceduralAudio generatedAudio;
    private void OnEnable()
    {
        tryButton.onClick.AddListener(BeginTraining);
        reloadButton.onClick.AddListener(ReloadPage);
        closeChallengeButton.onClick.AddListener(CloseChallenge);
    }
    private void OnDisable()
    {
        tryButton.onClick.RemoveAllListeners();
        reloadButton.onClick.RemoveAllListeners();
        closeChallengeButton.onClick.RemoveAllListeners(); 
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
        LoadQuestions();
    }
    private void Update()
    {
        if (!testStarted)
            return;

        if (timerDuration > 0.0f)
        {
            timerDuration -= Time.deltaTime;
            timerText.text = Mathf.RoundToInt(timerDuration).ToString() + "s";
        }
        if (makingChoice)
        {
            if (Input.GetKey(KeyCode.X))
            {
                MessageLog.LogMessage("Correct selection");
                //unansweredQuestions.Remove(currentQuestion);
                makingChoice = false;
                reloadButton.interactable = true;
                closeChallengeButton.interactable = true;
            }
        }
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

    public void SetCurrentQuestion()
    {
        if (unansweredQuestions.Count > 0)
        {
            int randQIndex = Random.Range(0, unansweredQuestions.Count);
            currentQuestion = unansweredQuestions[randQIndex];
            generatedAudio.frequency = currentQuestion.noteFrequency;
        }
    }
    public void LoadQuestions()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<QuestionGenerated>();
        }
        SetCurrentQuestion();
    }
    private void BeginTraining()
    {
        challengeChoiceMenu.SetActive(false);
        reloadButton.interactable = false;
        closeChallengeButton.interactable = false;
        StartCoroutine(StartChallenge());
    }
    private void PlayChallengeAudio()
    {
        generatedSoundText.text = currentQuestion.noteType;
        audioSource.Play();
    }
    private void ToggleTestGameState(bool testState, bool isPlaying, bool buttonInteract)
    {
        testUI.SetActive(testState);
        GameManager.SetGamePlayingState(isPlaying);
        tryButton.interactable = buttonInteract;
    }
    private void MakeChoice()
    {
        challengeChoiceMenu.SetActive(true);
        makingChoice = true;
    }
    private void ReloadPage()
    {
        LoadQuestions();
        BeginTraining();
    }
    private void CloseChallenge()
    {
        testStarted = false;
        ToggleTestGameState(false, false, true);
    }
    IEnumerator StartChallenge()
    {
        float loadTime = 3.0f;
        float durationTime = 5.0f;

        timerDuration = durationTime;

        ToggleTestGameState(true, false, false);
        MessageLog.LogMessage("Not started");
        yield return new WaitForSeconds(loadTime);

        MessageLog.LogMessage("Started");
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
        ToggleTestGameState(true, true, false);

        yield return new WaitForSeconds(durationTime);
        audioSource.Stop();
        MakeChoice();
    }
}
