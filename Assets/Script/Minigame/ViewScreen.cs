﻿using System.Collections;
using UnityEngine;

public class ViewScreen : InteractableOption {

    [SerializeField] private int buildNumber;
    [SerializeField] private Transform camPos;

    private Transform player;
	private Transform mainCamera;
    private Quaternion prevRot;
	private bool zoomIn = false;

    // Use this for initialization
    void Start(){

        mainCamera = Camera.main.transform;
	}

    private void Update()
    {

        if (!DisabledInputs.switchedMinigame && zoomIn && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithPlayer();
            if (DisabledInputs.wonMinigame)
            {
                DisabledInputs.wonMinigame = false;
                transform.tag = "Untagged";
            }
        }
    }

    public override void InteractWithPlayer()
    {
        zoomIn = !zoomIn;

        IndicatorMethod._instance.EnableIndicator(!zoomIn);
        // Reset game back to original state
        if (zoomIn)
        {
            player = mainCamera.transform.parent;
            prevRot = mainCamera.transform.localRotation;
            mainCamera.SetParent(camPos);
            DisabledInputs.ButtonsEnabled = false;
        }
        else
        {
            mainCamera.SetParent(player);
        }

        // Transition the camera to what it should face
        StartCoroutine(MoveCamera(0.5f));
    }

	// Update is called once per frame
	IEnumerator MoveCamera(float overtime){
        float elapsedTime = 0;

        if (zoomIn)
        {
            SceneFade.instance.StartMinigame(buildNumber);
        }

        while (elapsedTime < overtime) {
			if (zoomIn) {
				mainCamera.localPosition = Vector3.Lerp (mainCamera.localPosition, Vector3.zero, elapsedTime/overtime);
                mainCamera.localRotation = Quaternion.Lerp (mainCamera.localRotation, Quaternion.identity, elapsedTime / overtime);
			} else {
                mainCamera.localPosition = Vector3.Lerp (mainCamera.localPosition, new Vector3(0,1,0), elapsedTime / overtime);
                mainCamera.localRotation = Quaternion.Lerp(mainCamera.localRotation, prevRot, elapsedTime / overtime);
            }
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
		}

        if (!zoomIn)
            DisabledInputs.ButtonsEnabled = true;
    }
}
