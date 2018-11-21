using System.Collections;
using UnityEngine;

public class ElevatorAction : MonoBehaviour,DoAction {

    private bool isOpened = false;
    private bool waitAnim = false;
	[SerializeField] private GameObject blocker;

    // Animation of elevator door opening and closing
	public void PerformAction(){

		if (!waitAnim) {

			waitAnim = true;
			isOpened = !isOpened;

            StartCoroutine(PlayEvent()); 
		}
	}

    private IEnumerator PlayEvent()
    {
        blocker.gameObject.SetActive(!isOpened);

        if (isOpened)
        {
            GetComponent<Animator>().Play("ElevatorDoorOpen");
        }
        else
        {
            GetComponent<Animator>().Play("ElevatorDoorClose");
        }

        yield return new WaitForSeconds(1);
        waitAnim = false;
    }

	public bool GetIsOpened(){
		return this.isOpened;
	}
}
