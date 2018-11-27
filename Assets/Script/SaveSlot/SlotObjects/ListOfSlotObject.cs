using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlotList", menuName = "Game Objects/SlotList")]
[System.Serializable]
public class ListOfSlotObject : ScriptableObject {

    public List<SlotObject> slotObjectList;
}
