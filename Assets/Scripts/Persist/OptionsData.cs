[System.Serializable]
public class OptionsData {
    public float volume;
    // 0-PT, 1-EN
    public int language;

    public OptionsData(float volume, int language) {
        this.volume = volume;
        this.language = language;
    }

}
