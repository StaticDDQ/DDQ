using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour {

	public static ItemDB _instance;
	private int spaceCount = 8;
	private List<Item> itemList = new List<Item>();
	public List<Item> sortedItem = new List<Item>();

	public delegate void OnItemChanged();
	public OnItemChanged callBack;

	void Awake(){
		if (_instance != null) {
			return;
		}
		_instance = this;
	}

	public bool AddItem(Item item){
		if (itemList.Count == spaceCount) {
			return false;
		}

		for (int i = 0; i < itemList.Count; i++) {
			if (itemList[i].GetName() == item.GetName()) {
				itemList [i].SetAmount (itemList[i].GetAmount() + item.GetAmount ());
				SortAllItem ();
				if (callBack != null)
					callBack.Invoke ();
				return true;
			}
		}

		itemList.Add (item);
		SortAllItem ();
		if (callBack != null)
			callBack.Invoke ();
		return true;
	}

	public void RemoveItem(Item item){
		Item itemInList = itemList [itemList.IndexOf (item)];
		if (itemInList.GetAmount () > 1)
			itemInList.SetAmount (itemInList.GetAmount () - 1);
		else
			itemList.Remove (itemInList);

		SortAllItem ();
		if (callBack != null)
			callBack.Invoke ();
	}

	public void SortAllItem(){
		sortedItem.Clear ();
		foreach (Item i in itemList) {
			sortedItem.Add (i);
		}
	}
}