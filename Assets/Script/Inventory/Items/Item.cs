using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Game Objects/Items")]
public class Item : ScriptableObject {
	
	public Sprite sprite;
	public GameObject prefabItem;
}