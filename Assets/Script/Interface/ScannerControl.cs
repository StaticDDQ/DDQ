using UnityEngine;

public class ScannerControl : MonoBehaviour, Action {

    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;

    public void PerformAction()
    {
        door1.GetComponent<Action>().PerformAction();
        door2.GetComponent<Action>().PerformAction();
    }
}
