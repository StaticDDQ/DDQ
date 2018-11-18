using UnityEngine;

public class MouseTrigger : MonoBehaviour {

	public bool pressAgain = false;
	private GameObject MainCamera;
    private float rayLength = 2f;

    void Start(){
        MainCamera = Camera.main.gameObject;
	}

	void Update(){
		RaycastHit hit;
		Ray forwardRay = new Ray (MainCamera.transform.position, MainCamera.transform.forward);

		if (Physics.Raycast (forwardRay, out hit, rayLength)) {
			if (hit.collider.tag.Contains ("interactable") || hit.collider.tag.Contains("pickUp")) {
				IndicatorMethod._instance.SetSize(80);
			} else {
				IndicatorMethod._instance.SetSize(30);
			}

			if (DisabledInputs.ButtonsEnabled && Input.GetKeyDown ("e")) {
				if (hit.collider.tag == "interactable") {
					if (hit.collider.GetComponent<ItemUsable> ().getCanUse()) {
						hit.collider.GetComponent<ItemUsable> ().Interact ();
					} else {
						FindObjectOfType<InventoryUI> ().SetUseOn (hit.collider.gameObject);
						FindObjectOfType<InventoryUI> ().openInventory (true);
					}
				} else if (hit.collider.tag == "interactable2") {
					hit.collider.GetComponent<InteractableOption> ().InteractWithPlayer ();
				} 
			}
		} else {
			IndicatorMethod._instance.SetSize(30);
		}
	}
}
