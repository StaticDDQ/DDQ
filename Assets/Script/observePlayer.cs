using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class observePlayer : MonoBehaviour {

    [SerializeField] private Transform player;
    private bool inRange = false;

    // Update is called once per frame
    void Update () {
        if (inRange)
        {
            transform.LookAt(player);
        }
	}

    public void SetRange(bool range)
    {
        this.inRange = range;
    }
}
