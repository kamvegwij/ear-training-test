using UnityEngine;
using UnityEngine.UI;

public class KeyNoteManager : MonoBehaviour
{    
    public string noteType;
    public string noteColor;
    public Image image;
    public Color pressedColor;
    public Color normalColor;
    
    public float frequency;

    private Vector3 pressedKeyScale = new Vector3(1f, .94f, 1f);
    private Vector3 originalKeyScale = new Vector3(1f, 1f, 1f);

    private ChallengeSpawner spawner;
    private AudioSource audioSource;

    ProceduralAudio proceduralAudio;
    GameManager gameManager;
    private void Start()
    {
        gameManager = new GameManager();
        proceduralAudio = new ProceduralAudio(0.5f, 44100.0f, 2.0f);

        spawner = GameObject.Find("GameArea").GetComponent<ChallengeSpawner>();
        noteType = this.name;
        normalColor = image.color;

        GenerateWhiteNotePitch();
        GenerateBlackNotePitch();
        CustomAudioSource();
    }
    void OnAudioFilterRead(float[] data, int channels)
    {
        proceduralAudio.GenerateAudioReader(data, channels);
    }
    public void CustomAudioSource()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0;
        audioSource.volume = 1;
        audioSource.panStereo = 1;
        audioSource.Stop();
    }
    public void PressedNote()
    {
        LogSelection(proceduralAudio.frequency.ToString(), 0);

        if (audioSource.isPlaying) return;
        transform.localScale = pressedKeyScale;

        if (gameManager.isPlaying)
        {
            HandleChallengeProgress();
        }
        image.color = pressedColor;
        proceduralAudio.frequency = frequency;
        audioSource.Play();
    }

    public void ReleasedNote()
    {
        audioSource.Stop();
        image.color = normalColor;
    }
    public void MoveFingerFromNote()
    {
        transform.localScale = originalKeyScale;
    }
    private void GenerateWhiteNotePitch()
    {
        switch (noteType)
        {
            case "C4":
                frequency = 261.626f;
                break;
            case "C5":
                frequency = 523.251f;
                break;
            case "C6":
                frequency = 1046.502f;
                break;

            case "D4":
                frequency = 293.66f;
                break;
            case "D5":
                frequency = 587.33f;
                break;
            case "D6":
                frequency = 1174.66f;
                break;

            case "E4":
                frequency = 329.63f;
                break;
            case "E5":
                frequency = 659.26f;
                break;
            case "E6":
                frequency = 1318.51f;
                break;

            case "F4":
                frequency = 349.23f;
                break;
            case "F5":
                frequency = 698.46f;
                break;
            case "F6":
                frequency = 1396.91f;
                break;

            case "G4":
                frequency = 392.00f;
                break;
            case "G5":
                frequency = 783.99f;
                break;
            case "G6":
                frequency = 1567.98f;
                break;

            case "A4":
                frequency = 440f;
                break;
            case "A5":
                frequency = 880.00f;
                break;
            case "A6":
                frequency = 1760.00f;
                break;

            case "B4":
                frequency = 493.88f;
                break;
            case "B5":
                frequency = 987.77f;
                break;
            case "B6":
                frequency = 1975.53f;
                break;

        }
    }

    private void GenerateBlackNotePitch()
    {
        switch (noteType)
        {
            case "C#4":
                frequency = 277.18f;
                break;
            case "C#5":
                frequency = 554.37f;
                break;
            case "C#6":
                frequency = 1108.73f;

                break;
            case "D#4":
                frequency = 311.13f;
                break;
            case "D#5":
                frequency = 622.25f;
                break;
            case "D#6":
                frequency = 1244.51f;
                break;

            case "F#4":
                frequency = 369.99f;
                break;
            case "F#5":
                frequency = 739.99f;
                break;
            case "F#6":
                frequency = 1479.98f;
                break;

            case "G#4":
                frequency = 415.30f;
                break;
            case "G#5":
                frequency = 830.61f;
                break;
            case "G#6":
                frequency = 1661.22f;
                break;

            case "A#4":
                frequency = 466.16f;
                break;
            case "A#5":
                frequency = 932.33f;
                break;
            case "A#6":
                frequency = 1864.66f;
                break;
        }
    }
    private void HandleChallengeProgress()
    {
        if (noteType == spawner.randomNote.noteType)
        {
            LogSelection("Correct selection", 5);
        }
        else
        {
            LogSelection("Incorrect selection", -3);
        }
    }

    public void LogSelection(string outputMessage, int totalPoints)
    {
        Debug.Log(outputMessage);
        gameManager.totalXP += totalPoints;
    }

}
