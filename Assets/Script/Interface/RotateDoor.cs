using UnityEngine;

public class RotateDoor : MonoBehaviour, Action {

    [SerializeField] private string trigger1;
    [SerializeField] private string trigger2;
    private bool isOpen = false;

    public void PerformAction()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            GetComponent<Animator>().SetTrigger(trigger1);
        }
        else
        {
            GetComponent<Animator>().SetTrigger(trigger2);
        }
    }
}
