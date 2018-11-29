using UnityEngine;

public enum ItemType
{
    Puzzle,
    Consumable,
    Misc
}

[CreateAssetMenu(fileName = "Item", menuName = "Game Objects/Items")]
public class Item : ScriptableObject {
	
	public Sprite sprite;
	public GameObject prefabItem;
    public ItemType defType;
}