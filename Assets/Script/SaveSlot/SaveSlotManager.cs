using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSlotManager : MonoBehaviour {

    public static SaveSlotManager instance;
    [SerializeField] private ListOfSlotObject slotSaved;

	// Use this for initialization
	void Awake () {
		if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        LaunchGame();
	}

    private bool IsSavedFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    private void SaveList()
    {
        if (!IsSavedFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        BinaryFormatter bf = new BinaryFormatter();

        FileStream fs = new FileStream(Application.persistentDataPath + "/game_save/saveslots.dat", FileMode.Create);
        var json = JsonUtility.ToJson(slotSaved);
        bf.Serialize(fs, json);
        fs.Close();
    }

    private void LaunchGame()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream slotFile = File.Open(Application.persistentDataPath + "/game_save/saveslots.dat", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(slotFile), slotSaved);
            slotFile.Close();

        } catch (DirectoryNotFoundException)
        {
            SaveList();
        }
    }

    public void AddSaveSlot(SlotObject slot)
    {
        if (slotSaved.slotObjectList.Contains(slot))
        {
            slotSaved.slotObjectList.Remove(slot);
        }
        slotSaved.slotObjectList.Add(slot);
        SaveList();
    }

    public void RemoveSaveSlot(int index)
    {
        foreach (SlotObject slot in slotSaved.slotObjectList)
        {
            if(slot.index == index)
            {
                slotSaved.slotObjectList.Remove(slot);
                return;
            }
        }
    }

    public List<SlotObject> GetSlots()
    {
        return this.slotSaved.slotObjectList;
    }
}
