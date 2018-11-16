using UnityEngine;

public class Item : MonoBehaviour {
	
	private int amount = 1;

	[SerializeField] private string itemName;
	[SerializeField] private Sprite sprite;
	[SerializeField] private GameObject prefabItem;

	public int GetAmount(){
		return this.amount;
	}

	public void SetAmount(int a){
		amount = a;
	}

	public string GetName(){
		return this.itemName;
	}

	public Sprite GetSprite(){
		return this.sprite;
	}

	public GameObject GetItem(){
		return this.prefabItem;
	}
}