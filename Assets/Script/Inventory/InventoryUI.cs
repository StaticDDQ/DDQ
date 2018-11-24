using UnityEngine;

public class InventoryUI : MonoBehaviour {

	[SerializeField] private GameObject cancelButton, useButton, inspectButton;

	public bool inventoryOn = false;
    private bool inspectOn = false;
    private Item itemToUse;
	private GameObject useOn;
	private GameObject itemClone = null;

    private GameObject player;
    private GameObject itemsParent;
    private ItemDB inventoryDB;
    private InventorySlot[] slots;

    // Use this for initialization
    void Start () {
		inventoryDB = ItemDB._instance;

        player = GameObject.FindGameObjectWithTag("Player");
        itemsParent = transform.GetChild(0).gameObject;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }

	void Update(){
		if(!InputChecker.instance.switchedMinigame && !player.GetComponent<PickUp>().pressAgain && Time.timeScale == 1 && Input.GetKeyDown(KeyCode.I)) {

			inventoryOn = !itemsParent.activeInHierarchy;
            InputChecker.instance.ButtonsEnabled = !inventoryOn;
            openInventory (inventoryOn);
            UpdateUI();
			if (inspectOn) {
				DisableInspect ();
			}
		}
	}

	public void openInventory(bool turnOn){
        itemsParent.SetActive (turnOn);
		IndicatorMethod._instance.EnableIndicator (!inventoryOn);
        PauseButton.instance.ToggleCursorState (turnOn);

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

	private void UpdateUI(){
		for (int i = 0; i < slots.Length; i++) {
			if (i < inventoryDB.containedItems.Count) {
				slots [i].AddItem (inventoryDB.containedItems[i]);
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
        itemClone = Instantiate(itemToUse.prefabItem, player.transform.position + player.transform.forward, player.transform.rotation);
        player.GetComponent<PickUp>().ShowObject(itemClone, false);
		openInventory (false);
	}

	private void DisableInspect(){
        player.GetComponent<PickUp>().ShowObject(itemClone, true);
		Destroy (itemClone);
		openInventory (true);
		inventoryOn = true;
		inspectOn = false;
	}

	public void SetUseOn(GameObject t){
		this.useOn = t;
	}
}
