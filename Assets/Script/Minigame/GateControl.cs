using UnityEngine;

public class GateControl : SpriteAction {

    public override void SetCanDo(bool canDo)
    {
        base.SetCanDo(canDo);

        if (canDo)
        {
            GetComponent<Animator>().SetTrigger("Open");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Close");
        }
    }
}
