using UnityEngine;

[CreateAssetMenu(fileName = "Slot", menuName = "Game Objects/Slot")]
[System.Serializable]
public class SlotObject : ScriptableObject {

    public int index;
    public byte[] screenshotBytes;
    public string dateSaved;
    public bool hasSavedFile = false;
}
