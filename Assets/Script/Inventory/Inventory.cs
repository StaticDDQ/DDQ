using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Game Objects/Inventory")]
public class Inventory : ScriptableObject {

    public int slots;
    public List<Item> items;

}
