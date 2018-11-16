using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class errorIndicator : MonoBehaviour {

	public static errorIndicator _instance;

	// Use this for initialization
	void Awake () {
		if (_instance != null) {
			return;
		}
		_instance = this;
	}

	public void PlayAnim(){
		if (!GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName("ShowError")) {
			GetComponent<Animator> ().Play ("ShowError");
		}
	}
}
