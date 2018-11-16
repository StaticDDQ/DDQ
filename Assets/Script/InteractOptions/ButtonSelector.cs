using UnityEngine;

public class ButtonSelector : InteractableOption {

    [SerializeField] private GameObject target;

    public override void InteractWithPlayer()
    {
        isSatisfied = !isSatisfied;
        if (isSatisfied)
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        }
    }

    public void TriggerTarget()
    {
        target.GetComponent<Action>().PerformAction();
    }
}
