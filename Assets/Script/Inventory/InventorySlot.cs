using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
	public Image icon;
	public Item currentItem;
	private InventoryUI inventory;

	void Start(){
		inventory = this.transform.parent.parent.GetComponent<InventoryUI> ();
	}

	public void AddItem(Item i){

		currentItem = i;
		icon.sprite = currentItem.sprite;
		icon.enabled = true;
	}

	public void ClearSlot(){
		currentItem = null;

		icon.sprite = null;
		icon.enabled = false;
	}

	public void UseItem(){
		if (currentItem != null) {
			inventory.InteractItem (currentItem);
		}
	}
}
