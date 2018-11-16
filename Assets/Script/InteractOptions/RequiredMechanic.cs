using UnityEngine;

public class RequiredMechanic : InteractableOption {

    [SerializeField] private string anim1;
    [SerializeField] private string anim2;

    public override void InteractWithPlayer()
    {
        isSatisfied = !isSatisfied;
        if (isSatisfied)
            GetComponent<Animator>().SetTrigger(anim1);
        else
            GetComponent<Animator>().SetTrigger(anim2);
    }
}
