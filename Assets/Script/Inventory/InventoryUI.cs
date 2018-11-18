using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class InventoryUI : MonoBehaviour {

	[SerializeField] private Transform itemsParent;
	[SerializeField] private GameObject cancelButton, useButton, inspectButton, inventory;
	[SerializeField] private Transform player;

	public bool inventoryOn = false;
	private ItemDB inventoryDB;
	private Item itemToUse;
	private GameObject useOn;
	private bool inspectOn = false;
	private GameObject itemClone = null;

	InventorySlot[] slots;

	// Use this for initialization
	void Start () {
		inventoryDB = ItemDB._instance;
		inventoryDB.callBack += UpdateUI;

		slots = itemsParent.GetComponentsInChildren<InventorySlot> ();
	}

	void Update(){
		if(!player.GetComponent<PickUp>().pressAgain && Time.timeScale == 1){
			if (Input.GetKeyDown (KeyCode.I)) {
				inventoryOn = !inventory.activeInHierarchy;
				openInventory (inventoryOn);

				if (inspectOn) {
					DisableInspect ();
				}
			}
		}

		if (inspectOn || inventoryOn) {
            DisabledInputs.ButtonsEnabled = false;
			GetComponent<PauseButton> ().ToggleCursorState (true);
		} 
	}

	public void openInventory(bool turnOn){
		inventory.SetActive (turnOn);
		IndicatorMethod._instance.EnableIndicator (!inventoryOn);
        DisabledInputs.ButtonsEnabled = !turnOn;
        GetComponent<PauseButton> ().ToggleCursorState (turnOn);

		Cancel ();
		
	}
	
	public void InteractItem(Item i){
		itemToUse = i;
		cancelButton.SetActive (true);
		if (useOn != null)
			useButton.SetActive (true);
		else
			inspectButton.SetActive (true);
	}

	void UpdateUI(){
		for (int i = 0; i < slots.Length; i++) {
			if (i < inventoryDB.sortedItem.Count) {
				slots [i].AddItem (inventoryDB.sortedItem [i]);
			} else {
				slots [i].ClearSlot ();
			}
		}
	}

	public void Cancel(){
		cancelButton.SetActive (false);
		useButton.SetActive (false);
		inspectButton.SetActive (false);
	}

	public void UseButton(){

		if (useOn != null) {
			
			if(useOn.GetComponent<ItemUsable>().SetTrue(itemToUse)){

				ItemDB._instance.RemoveItem(itemToUse);
				useOn = null;
			}
		}
		openInventory (false);
		Cancel ();
	}

	public void InspectButton(){
		inspectOn = true;
		itemClone = Instantiate (itemToUse.GetItem(), player.GetChild(0).position + player.GetChild(0).forward, transform.rotation);
		player.GetChild(0).GetComponent<Blur> ().enabled = true;

        player.GetComponent<PickUp>().Grab(itemClone,true);

		openInventory (false);
	}

	void DisableInspect(){
        player.GetComponent<PickUp>().Grab(itemClone, false);
		Destroy (itemClone);
		openInventory (true);
		inventoryOn = true;
		inspectOn = false;
	}

	public void SetUseOn(GameObject t){
		this.useOn = t;
	}
}
