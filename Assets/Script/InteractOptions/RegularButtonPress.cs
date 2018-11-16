using UnityEngine;

public class RegularButtonPress : InteractableOption {

	[SerializeField] private GameObject objectTrigger;
	[SerializeField] private string anim;

    // Causes the object to perform a certain action
	public override void InteractWithPlayer ()
	{
		GetComponent<Animator> ().Play (anim, -1, 0f);
		objectTrigger.GetComponent<Action>().PerformAction ();
	}
}
