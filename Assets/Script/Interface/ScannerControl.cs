using UnityEngine;

public class ScannerControl : MonoBehaviour, DoAction {

    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;

    public void PerformAction()
    {
        door1.GetComponent<DoAction>().PerformAction();
        door2.GetComponent<DoAction>().PerformAction();
    }
}
