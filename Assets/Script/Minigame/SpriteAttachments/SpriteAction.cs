using UnityEngine;

public class SpriteAction : MonoBehaviour {

    [SerializeField] protected bool canDo = false;

    public void SetCanDo(bool canDo)
    {
        this.canDo = canDo;
    }
}
