using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class KeyNoteManager : MonoBehaviour
{    
    public string noteType;
    public string noteColor;
    public float currentFrequency;

    [SerializeField] private Color pressedColor;
    [SerializeField] private Color normalColor;


    private bool isPressed = false;

    private Vector3 pressedKeyScale = new Vector3(1f, .94f, 1f);
    private Vector3 originalKeyScale = new Vector3(1f, 1f, 1f);

    private Image image;
    private ChallengeSpawner spawner;
    private AudioSource audioSource;

    ProceduralAudio generatedAudio;

    private void Awake()
    {
        generatedAudio = new ProceduralAudio(AudioSettings.outputSampleRate, 0.5f);
        audioSource = GetComponent<AudioSource>();
        image = GetComponent<Image>();
        
    }
    private void Start()
    {
        spawner = GameObject.Find("GameArea").GetComponent<ChallengeSpawner>();
        AddNoteProperties();
    }
    void OnAudioFilterRead(float[] data, int channels)
    {
        double phaseIncrement = generatedAudio.frequency / generatedAudio.sampleRate;

        for (int sample = 0; sample < data.Length; sample += channels) 
        {
            float val = generatedAudio.amplitude * Mathf.Sin((float)generatedAudio.phase * 2*Mathf.PI);
            generatedAudio.phase = (generatedAudio.phase + phaseIncrement) % 1;
            for (int channel = 0; channel < channels; channel++)
            {
                data[sample + channel] = val;
            }
        }
    }

    public void PressedNote()
    {
        //note make sure the event trigger is OnPointerDown.
        GameManager.ToggleXP(7);
        MessageLog.LogMessage("Current Score: " + GameManager.totalXP.ToString());
        HandleNoteEventState(true, pressedColor, pressedKeyScale);
        audioSource.Play();
    }
    public void ReleasedNote()
    {
        HandleNoteEventState(false, normalColor, originalKeyScale);
        audioSource.Stop();
    }
    private void HandleNoteEventState(bool pressed, Color noteColor, Vector3 size)
    {
        isPressed = pressed;
        image.color = noteColor;
        transform.localScale = size;
    }
    private void AddNoteProperties()
    {
        noteType = this.name;
        normalColor = image.color;
        GenerateWhiteNotePitch();
        GenerateBlackNotePitch();

        //Audio Source toggle:
        audioSource.Stop();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    private void GenerateWhiteNotePitch()
    {
        switch (noteType)
        {
            case "C4":
                generatedAudio.frequency = 261.626f;
                break;
            case "C5":
                generatedAudio.frequency = 523.251f;
                break;
            case "C6":
                generatedAudio.frequency = 1046.502f;
                break;

            case "D4":
                generatedAudio.frequency = 293.66f;
                break;
            case "D5":
                generatedAudio.frequency = 587.33f;
                break;
            case "D6":
                generatedAudio.frequency = 1174.66f;
                break;

            case "E4":
                generatedAudio.frequency = 329.63f;
                break;
            case "E5":
                generatedAudio.frequency = 659.26f;
                break;
            case "E6":
                generatedAudio.frequency = 1318.51f;
                break;

            case "F4":
                generatedAudio.frequency = 349.23f;
                break;
            case "F5":
                generatedAudio.frequency = 698.46f;
                break;
            case "F6":
                generatedAudio.frequency = 1396.91f;
                break;

            case "G4":
                generatedAudio.frequency = 392.00f;
                break;
            case "G5":
                generatedAudio.frequency = 783.99f;
                break;
            case "G6":
                generatedAudio.frequency = 1567.98f;
                break;

            case "A4":
                generatedAudio.frequency = 440f;
                break;
            case "A5":
                generatedAudio.frequency = 880.00f;
                break;
            case "A6":
                generatedAudio.frequency = 1760.00f;
                break;

            case "B4":
                generatedAudio.frequency = 493.88f;
                break;
            case "B5":
                generatedAudio.frequency = 987.77f;
                break;
            case "B6":
                generatedAudio.frequency = 1975.53f;
                break;

        }
        currentFrequency = generatedAudio.frequency;
    }

    private void GenerateBlackNotePitch()
    {
        switch (noteType)
        {
            case "C#4":
                generatedAudio.frequency = 277.18f;
                break;
            case "C#5":
                generatedAudio.frequency = 554.37f;
                break;
            case "C#6":
                generatedAudio.frequency = 1108.73f;

                break;
            case "D#4":
                generatedAudio.frequency = 311.13f;
                break;
            case "D#5":
                generatedAudio.frequency = 622.25f;
                break;
            case "D#6":
                generatedAudio.frequency = 1244.51f;
                break;

            case "F#4":
                generatedAudio.frequency = 369.99f;
                break;
            case "F#5":
                generatedAudio.frequency = 739.99f;
                break;
            case "F#6":
                generatedAudio.frequency = 1479.98f;
                break;

            case "G#4":
                generatedAudio.frequency = 415.30f;
                break;
            case "G#5":
                generatedAudio.frequency = 830.61f;
                break;
            case "G#6":
                generatedAudio.frequency = 1661.22f;
                break;

            case "A#4":
                generatedAudio.frequency = 466.16f;
                break;
            case "A#5":
                generatedAudio.frequency = 932.33f;
                break;
            case "A#6":
                generatedAudio.frequency = 1864.66f;
                break;
        }
        currentFrequency = generatedAudio.frequency;
    }

}
