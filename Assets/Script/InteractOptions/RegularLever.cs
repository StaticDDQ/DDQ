using UnityEngine;

public class RegularLever : InteractableOption
{

    [SerializeField] private GameObject objectTrigger;

    // Causes the object to perform a certain action
    public override void InteractWithPlayer()
    {
        if (!isSatisfied && !GetComponent<Animator>().GetBool("Down") && !GetComponent<Animator>().GetBool("Up"))
        {
            GetComponent<Animator>().SetTrigger("Down");
            isSatisfied = true;
            objectTrigger.GetComponent<DoAction>().PerformAction();
        }
        else if(isSatisfied && !GetComponent<Animator>().GetBool("Up") && !GetComponent<Animator>().GetBool("Down"))
        {
            GetComponent<Animator>().SetTrigger("Up");
            isSatisfied = false;
            objectTrigger.GetComponent<DoAction>().PerformAction();
        }
    }
}
