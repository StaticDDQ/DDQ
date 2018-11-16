using UnityEngine;

public class SelectedButton : InteractableOption {

	[SerializeField] private GameObject buttonManager;
	[SerializeField] private int floor;
    private bool isPressed = false;

    public override void InteractWithPlayer ()
	{
        // Selecting the button, it notifies ElevatorManager
		if (!isPressed) {
			isPressed = true;
			buttonManager.GetComponent<ElevatorManager> ().SetButton (this.gameObject);
			GetComponent<Renderer>().material.SetColor ("_EmissionColor", Color.white);
		} 
	}

    // If the button is deselected
	public void RevertColour(){
		GetComponent <Renderer> ().material.SetColor ("_EmissionColor", Color.black);
		isPressed = false;
	}

    public int getFloor()
    {
        return this.floor;
    }
}
