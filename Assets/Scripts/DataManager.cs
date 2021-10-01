using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;


[Serializable]
class SaveData
{
    public string hiScoreName;
    public int hiScore;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public string playerName = "";
    public string hiScoreName = "";
    public int hiScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Save()
    {
        SaveData sd = new SaveData();
        sd.hiScore = hiScore;
        sd.hiScoreName = hiScoreName;

        Debug.Log(sd.hiScore.ToString() + sd.hiScoreName);
        string json = JsonUtility.ToJson(sd);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData sd = JsonUtility.FromJson<SaveData>(json);
            Debug.Log(sd.hiScore.ToString() + sd.hiScoreName);

            hiScore = sd.hiScore;
            hiScoreName = sd.hiScoreName;
        }
    }
}
