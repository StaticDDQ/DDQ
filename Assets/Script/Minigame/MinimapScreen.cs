using UnityEngine;

public class MinimapScreen : MonoBehaviour {

    [SerializeField] private GameObject minimapScreen;
    private bool isShown = false;

	// Update is called once per frame
	private void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isShown = !isShown;
            ShowMinimap(isShown);
        }
	}

    private void ShowMinimap(bool isShown)
    {
        Time.timeScale = (isShown) ? 0.0f : 1.0f;
        minimapScreen.SetActive(isShown);
    }
}
