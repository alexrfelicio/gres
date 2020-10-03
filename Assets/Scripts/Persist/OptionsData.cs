[System.Serializable]
public class OptionsData {
    public float volume;
    public float sfx;
    // 0-PT, 1-EN
    public int language;

    public OptionsData(float volume, float sfx, int language) {
        this.volume = volume;
        this.sfx = sfx;
        this.language = language;
    }

}
