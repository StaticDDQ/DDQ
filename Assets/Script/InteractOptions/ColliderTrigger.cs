using UnityEngine;

public class ColliderTrigger : MonoBehaviour {

    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        indicator.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        target.GetComponent<Action>().PerformAction();
    }

    private void OnTriggerExit(Collider other)
    {
        indicator.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
        target.GetComponent<Action>().PerformAction();
    }
}
