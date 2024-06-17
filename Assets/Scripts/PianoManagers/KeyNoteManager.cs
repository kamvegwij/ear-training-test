using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TreeEditor.TreeEditorHelper;

public class KeyNoteManager : MonoBehaviour
{
    [Range(1, 20000)]  //Creates a slider in the inspector
    public float frequency;
    public string noteType; //C4-C6 notes
    public string noteColor;
    //public TextMeshProUGUI freqText;
    //public TextMeshProUGUI noteName;

    public Image image;

    public Color pressedColor;
    public Color normalColor;

    public AudioSource audioSource;

    private int timeIndex = 0;
    private float sampleRate = 44100;
    private float waveLengthInSeconds = 2.0f;

    private Vector3 pressedKeyScale = new Vector3(1f, .94f, 1f);
    private Vector3 originalKeyScale = new Vector3(1f, 1f, 1f);

    

    private void Start()
    {
        noteType = this.name;
        CreateAudioSource();
        normalColor = image.color;

    }
    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            data[i] = CreateSine(timeIndex, frequency, sampleRate);
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

    public void PressedNote()
    {
        transform.localScale = pressedKeyScale;
        if (!audioSource.isPlaying)
        {
            if (GameManager.isPlaying)
            {
                GameManager.totalXP += 5;
            }
            
            audioSource.Play();
            GenerateWhiteNotePitch();
            GenerateBlackNotePitch();
            //freqText.text = frequency.ToString() + " hz";
            //noteName.text = noteType;
            image.color = pressedColor;
        }
        else
        {
            return;
        }
    }
    public void ReleasedNote()
    {
        audioSource.Stop();
        image.color = normalColor;

    }
    public void MoveFingerFromNote()
    {
        transform.localScale = originalKeyScale;
        //freqText.text = 0.ToString() + " hz";
        
    }

    private void CreateAudioSource()
    {
        frequency = 420;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        //audioSource.spatialBlend = 0; //force 2D sound
        audioSource.Stop(); //avoids audiosource from starting to play automatically
        audioSource.volume = 1;
        audioSource.loop = false;
        //audioSource.panStereo = 1;
    }
    private void GenerateWhiteNotePitch()
    {
        //create a list or 2d array to store note with it's corresponding freq-> {"C5", 523.251f}
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
        //TODO: make this mechanic simpler -> attach frequencies to individually generated notes.
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
}
