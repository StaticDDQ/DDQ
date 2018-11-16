using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insideRange : MonoBehaviour {
    [SerializeField] private GameObject cameraView;

	void OnTriggerEnter (Collider other) {
		if(other.tag == "Player")
        {
            cameraView.GetComponent<observePlayer>().SetRange(true);
        }
	}

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            cameraView.GetComponent<observePlayer>().SetRange(false);
        }
    }
}
