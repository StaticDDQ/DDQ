using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveSystem : MonoBehaviour {

    public static SaveSystem instance;
    [SerializeField] private Inventory inventoryData;
    [SerializeField] private GameObject player;

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

    public bool IsSavedFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    public void SaveGame(int slotIndex)
    {
        if (!IsSavedFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/game_data" + slotIndex))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/game_data" + slotIndex);
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream inventoryFile = File.Create(Application.persistentDataPath + "/game_save/game_data" + slotIndex + "/inventory.dat");
        FileStream playerFile = File.Create(Application.persistentDataPath + "/game_save/game_data" + slotIndex + "/player.dat");

        PlayerData data = new PlayerData();

        data.x = player.transform.position.x;
        data.y = player.transform.position.y;
        data.z = player.transform.position.z;

        var json = JsonUtility.ToJson(inventoryData);
        bf.Serialize(inventoryFile, json);
        bf.Serialize(playerFile, data);

        playerFile.Close();
        inventoryFile.Close();
    }

    public void LoadGame(int slotIndex)
    {
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/game_data" + slotIndex))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/game_data" + slotIndex);
        }
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(Application.persistentDataPath + "/game_save/game_data" + slotIndex + "/inventory.dat"))
        {
            FileStream inventoryFile = File.Open(Application.persistentDataPath + "/game_save/game_data" + slotIndex + "/inventory.dat",FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(inventoryFile), inventoryData);
            inventoryFile.Close();
        }

        if (File.Exists(Application.persistentDataPath + "/game_save/game_data" + slotIndex + "/player.dat"))
        {
            FileStream playerFile = File.Open(Application.persistentDataPath + "/game_save/game_data" + slotIndex + "/player.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(playerFile);

            player.transform.position = new Vector3(data.x, data.y, data.z);
            playerFile.Close();
        }
    }

    public void DeleteGame(int slotIndex)
    {
        if(File.Exists(Application.persistentDataPath + "/game_save/game_data" + slotIndex))
        {
            try
            {
                File.Delete(Application.persistentDataPath + "/game_save/game_data" + slotIndex);
            } catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}

[Serializable]
class PlayerData
{
    public float x;
    public float y;
    public float z;
}
