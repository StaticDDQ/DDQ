using UnityEngine;

public class ElevatorManager : InteractableOption {

	private static int currFloor = 3;
    private GameObject selectedButton = null;

	[SerializeField] private GameObject[] buttons;
	[SerializeField] private GameObject player;

	public override void InteractWithPlayer ()
	{
        // If a button is selected and the door is closed. The lift either goes down or up depending on what floor is pressed
		if (selectedButton != null && !this.transform.parent.GetComponent<ElevatorAction>().GetIsOpened()){

            // Deselect the current button
            selectedButton.GetComponent<SelectedButton>().RevertColour();
            selectedButton = null;

            player.transform.SetParent(this.transform.parent);
            DisableButtons();

            // Parent the player to the elevator to allow smooth elevator movement
            // Player can no longer press buttons if elevator is moving
            if (currFloor > selectedButton.GetComponent<SelectedButton>().getFloor())
            {
                this.transform.parent.GetComponent<Animator>().Play("elevatorDown");
            }
            else if(currFloor < selectedButton.GetComponent<SelectedButton>().getFloor())
            {
                this.transform.parent.GetComponent<Animator>().Play("elevatorUp");
            }
		}
	}

	public void SetButton(GameObject button){
        // If a button was selected previously, uncheck the color
		if (selectedButton != null) {
			selectedButton.GetComponent<SelectedButton> ().RevertColour ();
		}
		this.selectedButton = button;
	}

	private void DisableButtons(){
        // Player can no longer interact with them
		foreach (GameObject i in buttons) {
			i.tag = "Untagged";
		}
	}
}
