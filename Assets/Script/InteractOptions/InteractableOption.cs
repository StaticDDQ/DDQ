using UnityEngine;

public abstract class InteractableOption : MonoBehaviour {

	[SerializeField] protected bool isSatisfied = false;

    public abstract void InteractWithPlayer();

	public void SetSatisfied(bool isTrue){
		this.isSatisfied = isTrue;
	}

	public bool GetBool(){
		return this.isSatisfied;
	}
}
