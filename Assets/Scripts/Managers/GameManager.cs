[System.Serializable]
public static class GameManager
{

    public static int totalXP { get; set; }
    public static string username { get; set; }
    public static string password { get; set; }
    public static bool isPlaying { get; set; }
    public static int gameMode { get; set; }

    public static void ToggleXP(int val)
    {
        totalXP += val;
    }
    public static void SwitchGameMode(int val)
    {
        gameMode = val;
    }
    public static void SetGamePlayingState(bool state)
    {
        isPlaying = state;
    }
}
