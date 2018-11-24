using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Game Objects/Items")]
public class Item : ScriptableObject {
	
	public Sprite sprite;
	public GameObject prefabItem;
}