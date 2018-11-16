using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSequence : MonoBehaviour {
	
	[SerializeField] protected GameObject[] requirements;
	protected bool eventDone = false;

	public virtual void CheckEvent1 (){

	}
}
