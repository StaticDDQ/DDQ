using UnityEngine;

public class CamZoom : MonoBehaviour {

	[SerializeField] private int zoom = 40;
	[SerializeField] private int normal = 70;
	[SerializeField] private float smooth = 5;
	[SerializeField] private GameObject itemCamera;

	private bool isZoomed = false;

	void Update()
	{
		if (DisabledInputs.ButtonsEnabled && Input.GetMouseButtonDown (1)) 
		{
			isZoomed = !isZoomed;
		}
		if (isZoomed) {
			GetComponent<Camera> ().fieldOfView = Mathf.Lerp (GetComponent<Camera> ().fieldOfView, zoom, Time.deltaTime * smooth);
			itemCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp (GetComponent<Camera> ().fieldOfView, zoom, Time.deltaTime * smooth);
		} 
		else
		{
			GetComponent<Camera> ().fieldOfView = Mathf.Lerp (GetComponent<Camera> ().fieldOfView, normal, Time.deltaTime * smooth);
			itemCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp (GetComponent<Camera> ().fieldOfView, normal, Time.deltaTime * smooth);
		}
	}
}
