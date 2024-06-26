using UnityEngine;
public class PressedKey : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float sampleRate = 44100;
    [SerializeField] private float waveLengthInSeconds = 2.0f;
    [SerializeField] private float intensity = 0.5f; //Amplitude.

    private int timeIndex = 0;
    private string noteType; //C4-C6 notes
    private Vector3 pressedKeyScale = new Vector3(1f, .94f, 1f);
    private Vector3 originalKeyScale = new Vector3(1f, 1f, 1f);

    private AudioSource audioSource;

    private void Start()
    {
        noteType = this.name;
        CreateAudioSource();
    }
    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            data[i] = MakeNoise(timeIndex, frequency, sampleRate);
            timeIndex++;

            //if timeIndex gets too big, reset it to 0
            if (timeIndex >= (sampleRate * waveLengthInSeconds))
            {
                timeIndex = 0;
            }
        }
    }

    //Make Noise
    public float MakeNoise(int timeIndex, float frequency, float sampleRate)
    {
        float angVelocity = 2*Mathf.PI * timeIndex * frequency; //sine waves don't take hz, convert to angular velo.

        return intensity * Mathf.Sin( (angVelocity) / sampleRate); //sine wave
    }

    public void PressedNote()
    {
        transform.localScale = pressedKeyScale;
        PlayNote();
    }
    public void ReleasedNote()
    {
        StopNote();
    }
    public void MoveFingerFromNote()
    {
        transform.localScale = originalKeyScale;
    }

    private void CreateAudioSource()
    {
        frequency = 440;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0; //force 2D sound
        audioSource.Stop(); //avoids audiosource from starting to play automatically
        audioSource.volume = 1;
        audioSource.loop = false;
    }
    private void PlayNote()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            GenerateWhiteNotePitch();
            GenerateBlackNotePitch();
        }
        else
        {
            return;
        }
        
    }
    private void StopNote()
    {
        audioSource.Stop();
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
