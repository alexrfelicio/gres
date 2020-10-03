using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameSystem {

    public static void SaveOptionsData(float volume, float sfx, int language) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/options.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        OptionsData data = new OptionsData(volume, sfx, language);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static OptionsData LoadOptionsData() {
        string path = Application.persistentDataPath + "/options.bin";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            OptionsData data = formatter.Deserialize(stream) as OptionsData;
            return data;
        } else {
            //Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveLevelData(int id, int score, bool artefato_1,
            bool artefato_2, bool artefato_3, bool artefato_4) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/level" + id.ToString() + ".bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(id, score, artefato_1, artefato_2, artefato_3, artefato_4);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LevelData LoadLevelData(int id) {
        string path = Application.persistentDataPath + "/level" + id.ToString() + ".bin";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            return data;
        } else {
            //Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
