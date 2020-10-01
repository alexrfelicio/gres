[System.Serializable]
public class LevelData {
    public int id;
    public int score;
    public bool artefato_1;
    public bool artefato_2;
    public bool artefato_3;
    public bool artefato_4;

    public LevelData(int id, int score, bool artefato_1, bool artefato_2, bool artefato_3, bool artefato_4) {
        this.id = id;
        this.score = score;
        this.artefato_1 = artefato_1;
        this.artefato_2 = artefato_2;
        this.artefato_3 = artefato_3;
        this.artefato_4 = artefato_4;
    }

}
