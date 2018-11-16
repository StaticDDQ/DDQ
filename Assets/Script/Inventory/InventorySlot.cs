using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
	public Image icon;
	public Text count;
	public Item currentItem;
	private InventoryUI inventory;

	void Start(){
		inventory = this.transform.parent.parent.parent.GetComponent<InventoryUI> ();
	}

	public void AddItem(Item i){
		currentItem = i;
		icon.sprite = currentItem.GetSprite();
		count.text = i.GetAmount().ToString();
		icon.enabled = true;
		count.enabled = true;
	}

	public void ClearSlot(){
		currentItem = null;

		icon.sprite = null;
		icon.enabled = false;
		count.enabled = false;
	}

	public void UseItem(){
		if (currentItem != null) {
			inventory.InteractItem (currentItem);
		}
	}
}
