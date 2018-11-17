using UnityEngine;

public class SpriteAction : MonoBehaviour {

    [SerializeField] protected bool canDo = false;

    public virtual void SetCanDo(bool canDo)
    {
        this.canDo = canDo;
    }
}
