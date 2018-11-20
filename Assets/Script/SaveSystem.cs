using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour {

    public static SaveSystem instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public bool isSaveFile()
    {

        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    public void SaveGame()
    {
        if (!isSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/character_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_data");
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/character_data/character_save");

        //var json = JsonUtility.ToJson();
        //bf.Serialize(file, json);
        file.Close();
    }

    public void LoadGame()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/character_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_data");
        }
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(Application.persistentDataPath + "/game_save/character_data/character_save"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/character_data/character_save", FileMode.Open);
            //JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), );
            file.Close();
        }
    }
}
