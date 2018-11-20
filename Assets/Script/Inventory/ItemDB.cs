using UnityEngine;
using System.Collections.Generic;

public class ItemDB : MonoBehaviour {

	public static ItemDB _instance;

	public delegate void OnItemChanged();
	public OnItemChanged callBack;

    [SerializeField] private Inventory inventoryData;
    public List<Item> containedItems;

    void Awake(){
        containedItems = inventoryData.items;
		if (_instance != null) {
			return;
		}
		_instance = this;
	}

	public bool AddItem(Item item){
		if (containedItems.Count == inventoryData.slots) {
			return false;
		}
        containedItems.Add(item);

		if (callBack != null)
			callBack.Invoke ();
		return true;
	}

	public void RemoveItem(Item item){
		Item itemInList = containedItems[containedItems.IndexOf (item)];

		if(itemInList != null)
        {
            containedItems.Remove(itemInList);
            if (callBack != null)
                callBack.Invoke();
        }
	}
}