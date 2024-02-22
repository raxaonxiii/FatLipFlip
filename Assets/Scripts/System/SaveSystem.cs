
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    public static void SaveTime(GameManager gameMan)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(gameMan);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Now Saved");
    }

    public static SaveData LoadSave()
    {
        string path = Application.persistentDataPath + "/save.sav";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            SaveData data = new SaveData();

            formatter.Serialize(stream, data);
            stream.Close();
            return data;
        }

    }
}
