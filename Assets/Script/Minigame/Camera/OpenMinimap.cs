using UnityEngine;

public class OpenMinimap : MonoBehaviour {

    [SerializeField] private GameObject map;
    private bool mapShown = false;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            mapShown = !mapShown;
            if (mapShown)
            {
                Time.timeScale = 0.0f;
            }
            else
            {   
                Time.timeScale = 1.0f;
            }

            DisableInputs.ButtonsEnabled = !mapShown;
            map.SetActive(mapShown);
        }
	}
}
