using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class InventoryUI : MonoBehaviour {

	[SerializeField] private GameObject itemsParent, cancelButton, useButton, inspectButton;

	public bool inventoryOn = false;
    public ItemUsable canUse;

    private bool inspectOn = false;

	private GameObject itemClone = null;
    private InventorySlot[] slots;

    private Item itemToUse;
    private Camera mainCam;

    // Use this for initialization
    private void Start () {
        mainCam = Camera.main;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

	private void Update(){
		if(!InputChecker.instance.switchedMinigame && Time.timeScale == 1 && Input.GetKeyDown(KeyCode.I)) {

            InputChecker.instance.ButtonsEnabled = inventoryOn;
            PauseButton.instance.ToggleCursorState(!inventoryOn);

            openInventory(!inventoryOn);
            if (inspectOn) {
				DisableInspect ();
			}
		}
	}

	public void openInventory(bool turnOn){
        inventoryOn = turnOn;

        UpdateUI();
        itemsParent.SetActive (turnOn);

        IndicatorMethod._instance.EnableIndicator (!inventoryOn);

		Cancel ();

        if (!turnOn)
            canUse = null;
	}
	
	public void InteractItem(Item i){
		itemToUse = i;

		cancelButton.SetActive (true);
        inspectButton.SetActive(true);

        if (canUse != null)
        {
            useButton.SetActive(true);
        }
	}

	private void UpdateUI(){
		for (int i = 0; i < slots.Length; i++) {
			if (i < ItemDB._instance.containedItems.Count) {
				slots [i].AddItem (ItemDB._instance.containedItems[i]);
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

        if (itemToUse != null && canUse.ValidItem(itemToUse.defType))
        {
            canUse.ApplyItem(itemToUse);
            canUse = null;
            ItemDB._instance.RemoveItem(itemToUse);
            UpdateUI();
        }

        Cancel();
	}

	public void InspectButton(){
        inspectOn = true;
        itemClone = Instantiate(itemToUse.prefabItem, mainCam.transform.position + mainCam.transform.forward, Quaternion.identity);

        ShowObject(itemClone, true);
        itemsParent.SetActive(false);

        Cancel();
    }

	private void DisableInspect(){
        inspectOn = false;
        ShowObject(itemClone, false);
    }

    private void ShowObject(GameObject obj, bool toShow)
    {
        if (toShow)
        {
            obj.transform.SetParent(mainCam.transform.parent.transform);

            obj.GetComponent<PickUpable>().canRotate = true;
            obj.GetComponent<PickUpable>().carrying = true;

            obj.GetComponent<Rigidbody>().isKinematic = true;
            obj.layer = LayerMask.NameToLayer("pickUp");
        }
        else
        {
            Destroy(obj);
        }

        IndicatorMethod._instance.EnableIndicator(!toShow);
        mainCam.GetComponent<Blur>().enabled = toShow;
    }
}
