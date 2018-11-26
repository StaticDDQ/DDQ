﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;

public class SaveSystem : MonoBehaviour {

    public static SaveSystem instance;
    public List<SaveableObject> saveObjects { get; set; }

    [SerializeField] private Inventory inventoryData;
    [SerializeField] private GameObject player;
    private int itemCount = 0;

    private void Awake()
    {
        saveObjects = new List<SaveableObject>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
    }

    public bool IsSavedFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    public void Save(int slotIndex)
    {
        if (!IsSavedFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/game_data" + slotIndex))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/game_data" + slotIndex);
        }

        BinaryFormatter bf = new BinaryFormatter();

        #region player
        FileStream playerFile = File.Create(Application.persistentDataPath + "/game_save/game_data" + slotIndex + "/player.dat");
        PlayerData data = new PlayerData();

        data.x = player.transform.position.x;
        data.y = player.transform.position.y;
        data.z = player.transform.position.z;

        bf.Serialize(playerFile, data);
        playerFile.Close();
        #endregion

        #region inventory
        FileStream inventoryFile = File.Create(Application.persistentDataPath + "/game_save/game_data" + slotIndex + "/inventory.dat");

        var json = JsonUtility.ToJson(inventoryData);
        bf.Serialize(inventoryFile, json);
        inventoryFile.Close();
        #endregion

        #region items
        FileStream itemFile = File.Create(Application.persistentDataPath + "/game_save/game_data" + slotIndex + "/item0.dat");
        ItemData itemData = new ItemData();

        for (int i = 0; i < saveObjects.Count; i++)
        {
            if (saveObjects[i].gameObject != null)
            {
                itemData.objDir = saveObjects[i].GetName();
                itemData.x = saveObjects[i].transform.position.x;
                itemData.y = saveObjects[i].transform.position.y;
                itemData.z = saveObjects[i].transform.position.z;
                itemData.rotX = saveObjects[i].transform.rotation.x;
                itemData.rotY = saveObjects[i].transform.rotation.y;
                itemData.rotZ = saveObjects[i].transform.rotation.z;
                itemData.rotW = saveObjects[i].transform.rotation.w;

                bf.Serialize(itemFile, itemData);

                itemFile = File.Create(Application.persistentDataPath + "/game_save/game_data" + slotIndex + "/item" + (++itemCount) + ".dat");
            }
        }
        itemFile.Close();
        #endregion
    }

    public void Load(int slotIndex)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/game_data" + slotIndex))
        {
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();

        #region player
        FileStream playerFile = File.Open(Application.persistentDataPath + "/game_save/game_data" + slotIndex + "/player.dat", FileMode.Open);
        PlayerData data = (PlayerData)bf.Deserialize(playerFile);

        player.transform.position = new Vector3(data.x,data.y,data.z);
        playerFile.Close();
        #endregion

        #region inventory
        FileStream inventoryFile = File.Open(Application.persistentDataPath + "/game_save/game_data" + slotIndex + "/inventory.dat", FileMode.Open);
        JsonUtility.FromJsonOverwrite((string)bf.Deserialize(inventoryFile), inventoryData);
        inventoryFile.Close();
        #endregion

        #region items
        foreach (SaveableObject obj in saveObjects)
        {
            if(obj != null)
            {
                Destroy(obj.gameObject);
            }
        }

        saveObjects.Clear();
        FileStream itemFile = File.Open(Application.persistentDataPath + "/game_save/game_data" + slotIndex + "/item0.dat", FileMode.Open);

        for (int i = 0; i < itemCount; i++)
        {

            ItemData itemData = (ItemData)bf.Deserialize(itemFile);
            print(itemData.objDir);
            GameObject tmp = Instantiate(Resources.Load(itemData.objDir) as GameObject);

            if(tmp != null)
            {
                Vector3 newPos = new Vector3(itemData.x, itemData.y, itemData.z);
                Quaternion newRot = new Quaternion(itemData.rotX, itemData.rotY, itemData.rotZ, itemData.rotW);
                tmp.GetComponent<SaveableObject>().Load(newPos, newRot);
            }
        }

        itemFile.Close();
        #endregion
    }

    public void DeleteGame(int slotIndex)
    {
        if (File.Exists(Application.persistentDataPath + "/game_save/game_data" + slotIndex))
        {
            try
            {
                File.Delete(Application.persistentDataPath + "/game_save/game_data" + slotIndex);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }

    /*
    public Vector3 StringToVector(string value)
    {
        value = value.Trim(new char[] { '(', ')' });
        value = value.Replace(" ", "");

        string[] pos = value.Split(',');

        return new Vector3(float.Parse(pos[0]),float.Parse(pos[1]),float.Parse(pos[2]));
    }

    public Quaternion StringToQuaternion(string value)
    {

        value = value.Trim(new char[] { '(', ')' });
        value = value.Replace(" ", "");

        string[] pos = value.Split(',');

        return new Quaternion(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]), float.Parse(pos[3]));
    } */
}

[Serializable]
class ItemData
{
    public string objDir;
    public float x;
    public float y;
    public float z;
    public float rotX;
    public float rotY;
    public float rotZ;
    public float rotW;
}

[Serializable]
class PlayerData
{
    public float x;
    public float y;
    public float z;
}
