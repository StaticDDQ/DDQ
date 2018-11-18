using System.Collections;
using UnityEngine;

public class ViewScreen : InteractableOption {

    [SerializeField] private int buildNumber;
    [SerializeField] private Transform camPos;
    [SerializeField] private Transform player;

	private Transform mainCamera;
    private Quaternion prevRot;
	private bool zoomIn = false;
	private bool runOnce = false;
    private bool waitEnum = false;

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
        if (!waitEnum)
        {
            zoomIn = !zoomIn;

            IndicatorMethod._instance.EnableIndicator(!zoomIn);
            // Reset game back to original state
            if (zoomIn)
            {
                prevRot = mainCamera.transform.localRotation;
                mainCamera.SetParent(camPos);
                DisabledInputs.ButtonsEnabled = false;
            }
            else
            {
                mainCamera.SetParent(player);
            }

            // Transition the camera to what it should face
            StartCoroutine(MoveCamera(1f));
        }
    }

	// Update is called once per frame
	IEnumerator MoveCamera(float overtime){
        waitEnum = true;
        float elapsedTime = 0;

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

        if (zoomIn)
        {
            SceneFade.instance.StartMinigame(buildNumber);
        }

        if (!zoomIn)
            DisabledInputs.ButtonsEnabled = true;

        waitEnum = false;
    }
}
