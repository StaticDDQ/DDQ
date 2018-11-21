using UnityEngine;

public class ColliderTrigger : MonoBehaviour {

    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        indicator.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        target.GetComponent<DoAction>().PerformAction();
    }

    private void OnTriggerExit(Collider other)
    {
        indicator.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
        target.GetComponent<DoAction>().PerformAction();
    }
}
