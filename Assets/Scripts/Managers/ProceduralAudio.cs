using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProceduralAudio
{
    public float frequency { get; set; }
    public float amp { get; set; } //amplitude
    public float timePeriod { get; set; }
    public float angVelocity { get; set; }
    public float sampleRate { get; set; }
    public float waveLengthInSeconds { get; set; }

    private bool isActive { get; set; }
    public ProceduralAudio(float amp, float sampleRate, float waveLengthInSeconds)
    {
        this.amp = amp;
        this.sampleRate = sampleRate;
        this.waveLengthInSeconds = waveLengthInSeconds;
    }

    private float MakeNoise()
    {
        angVelocity = 2 * Mathf.PI * frequency;
        return Mathf.Sin(angVelocity * timePeriod) / sampleRate;
    }
    public void GenerateAudioReader(float[] data, int channels)
    {
        timePeriod = 0;
        float sampleRate = 44100;
        float waveLengthInSeconds = 1.0f;

        for (int i = 0; i < data.Length; i += channels)
        {
            data[i] = MakeNoise();
            timePeriod++;

            if (timePeriod >= (sampleRate * waveLengthInSeconds))
            {
                timePeriod = 0;
            }
        }
    }
}
