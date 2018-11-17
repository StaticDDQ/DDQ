using UnityEngine;

public class SpriteAction : MonoBehaviour {

    protected bool canDo = true;

    public virtual void SetCanDo(bool canDo)
    {
        this.canDo = canDo;
    }
}
