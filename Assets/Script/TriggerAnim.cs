using UnityEngine;

public class TriggerAnim : MonoBehaviour {

    [SerializeField] private GameObject AnimatedObj;
    [SerializeField] private string Transition1;
    [SerializeField] private string Transition2;
    private bool isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen)
        {
            isOpen = true;
            AnimatedObj.GetComponent<Animator>().SetTrigger(Transition1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isOpen)
        {
            isOpen = false;
            AnimatedObj.GetComponent<Animator>().SetTrigger(Transition2);
        }
    }
}
