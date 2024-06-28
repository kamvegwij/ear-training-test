[System.Serializable]
public class ProceduralAudio
{
    public ProceduralAudio(int sampleRate, float amplitude = 0.5f)
    {
        this.sampleRate = sampleRate;
        this.amplitude = amplitude;
    }

    public float amplitude { get; set; }
    public float frequency { get; set; } 
    public double phase { get; set; }
    public int sampleRate { get; set; }
}
