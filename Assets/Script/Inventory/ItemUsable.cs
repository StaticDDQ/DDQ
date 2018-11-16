using UnityEngine;

// Method used to have an object interact with different types of items
public class ItemUsable : MonoBehaviour {

	protected bool canUse = false;

    public virtual void Interact(){}
		
	public virtual bool SetTrue (Item item) {
		return false;
	}

    public bool getCanUse()
    {
        return this.canUse;
    }
}
