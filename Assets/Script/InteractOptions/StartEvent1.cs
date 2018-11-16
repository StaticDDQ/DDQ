using System.Collections;
using UnityEngine;

public class StartEvent1 : InteractableOption {

	[SerializeField] private GameObject eventObject;
	private bool runOnce = false;

	public override void InteractWithPlayer () {
		if (!isSatisfied)
			ReadEvent ();
		else
			PlayAnim ();
	}

	private void ReadEvent () {
		errorIndicator._instance.PlayAnim ();
		if (!runOnce) {
			runOnce = true;
			StartCoroutine (WaitForAction ());
		}
	}

    // Turn on the screen, event
	private IEnumerator WaitForAction(){
		yield return new WaitForSeconds (5);
		eventObject.GetComponent<InteractTV> ().ScreenOn ();
		eventObject.transform.tag = "interactable2";
	}

    // Open the door
	private void PlayAnim(){
		GetComponent<BoxCollider> ().enabled = false;
		GetComponent<Animator> ().Play ("shelfAnimate1");
	}
}
