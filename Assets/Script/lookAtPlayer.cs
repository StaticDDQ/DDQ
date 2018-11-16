using UnityEngine;
using System.Collections;

public class lookAtPlayer : MonoBehaviour {

	void OnTriggerStay(Collider player){
		if (player.name == "Player") {
			transform.LookAt (player.transform);
		}
	}
}
