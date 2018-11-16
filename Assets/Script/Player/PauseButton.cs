using UnityEngine;

public class PauseButton : MonoBehaviour {

    public static PauseButton instance;
	[SerializeField] private GameObject Player;
    [SerializeField] private GameObject PauseScreen;
	private bool pauseGame = false;

	private bool cursorLocked = true;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    private void Update () {
		if (Input.GetKeyDown (KeyCode.Backspace)) {
			openPauseMenu (!pauseGame);
		} 
	}

	private void openPauseMenu(bool openMenu){

        // When it is paused
        if (openMenu)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        if (!Player.GetComponent<PickUp>().pressAgain)
        {
            DisableInputs.ButtonsEnabled = !openMenu;
        }
        // Enable cursor to be seen if it is paused
        ToggleCursorState (openMenu);
		pauseGame = openMenu;
        PauseScreen.SetActive (openMenu);
	}

	public void ToggleCursorState(bool active){
		cursorLocked = !active;
		if (cursorLocked) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

    public void Quit()
    {
        Application.Quit();
    }

    public void GoMenu()
    {

    }

    public void Resume()
    {
        openPauseMenu(false);
    }
}


