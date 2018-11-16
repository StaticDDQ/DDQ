using UnityEngine;

public class PlayAnim : InteractableOption {

	[SerializeField] private GameObject animatedObject;
	[SerializeField] private string animationName;

    public override void InteractWithPlayer ()
	{
        // Simply play animation
		Animate ();
	}

	private void Animate () {
        // One time occurence so player cannot interact with it again
		animatedObject.GetComponent<Animator> ().Play (animationName);
		transform.tag = "Untagged";
		GetComponent<BoxCollider> ().enabled = false;
	}
}
