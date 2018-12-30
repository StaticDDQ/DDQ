using System.Collections;
using UnityEngine;

public class ViewScreen : InteractableOption {

    [SerializeField] private Transform camPos;
    [SerializeField] private int screenIndex;

    private Transform player;
	private Transform mainCamera;
    private Quaternion prevRot;
	private bool zoomIn = false;

    // Use this for initialization
    private void Start(){

        mainCamera = Camera.main.transform;
	}

    private void Update()
    {
        if (!InputChecker.instance.switchedMinigame && zoomIn)
        {
            InteractWithPlayer();
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
            InputChecker.instance.ButtonsEnabled = false;

            SceneFade.instance.StartMinigame(this, screenIndex);
        }
        else
        {
            mainCamera.SetParent(player);
        }

        // Transition the camera to what it should face
        StartCoroutine(MoveCamera(0.5f));
    }

    public void WonGame()
    {
        transform.tag = "Untagged";
    }

	// Update is called once per frame
	private IEnumerator MoveCamera(float overtime){
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

        if (!zoomIn)
            InputChecker.instance.ButtonsEnabled = true;
    }
}
