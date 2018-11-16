using System.Collections;
using UnityEngine;

public class ViewScreen : InteractableOption {

    [SerializeField] private int buildNumber;
    [SerializeField] private Transform camPos;
    [SerializeField] private Transform player;

	private Transform mainCamera;

	private bool waitCoroutine = false;
	private bool zoomIn = false;
	private bool runOnce = false;

    // Use this for initialization
    void Start(){

        mainCamera = Camera.main.transform;
	}

    private void Update()
    {
        if (zoomIn && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithPlayer();
        }
    }

    public override void InteractWithPlayer()
    {
        // Turn off video
        if (!runOnce)
        {
            GetComponent<InteractTV>().ScreenOff();
            runOnce = true;
        }

        if (!waitCoroutine)
        {
            zoomIn = !zoomIn;

            DisableInputs.ButtonsEnabled = !zoomIn;
            IndicatorMethod._instance.EnableIndicator(!zoomIn);

            // Reset game back to original state
            if (zoomIn)
            {
                mainCamera.SetParent(camPos);
            }
            else
            {
                mainCamera.SetParent(player);
            }

            // Transition the camera to what it should face
            StartCoroutine(MoveCamera(3f));

        }
    }

	// Update is called once per frame
	IEnumerator MoveCamera(float overtime){
		waitCoroutine = true;

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
                mainCamera.localRotation = Quaternion.Lerp(mainCamera.localRotation, Quaternion.identity, elapsedTime / overtime);
            }
            elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		waitCoroutine = false;
	}
}
