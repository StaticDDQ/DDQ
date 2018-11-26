using UnityEngine;

public class PauseButton : MonoBehaviour {

    public static PauseButton instance;
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject slotScreen;
	private bool pauseGame = false;
	private bool cursorLocked = true;
    private GameObject Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
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
            slotScreen.SetActive(false);
        }
        if (!Player.GetComponent<PickUp>().pressAgain)
        {
            InputChecker.instance.ButtonsEnabled = !openMenu;
        }
        // Enable cursor to be seen if it is paused
        ToggleCursorState (openMenu);
		pauseGame = openMenu;
        PauseScreen.SetActive (openMenu);
	}

	public void ToggleCursorState(bool active){
		cursorLocked = !active;
		if (cursorLocked && !InputChecker.instance.switchedMinigame) {
			Cursor.visible = false;
		} else {
			Cursor.visible = true;
		}
	}

    public void SaveSlot()
    {
        slotScreen.SetActive(true);
        PauseScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        openPauseMenu(false);
    }

    public void BackPauseScreen()
    {
        slotScreen.SetActive(false);
        PauseScreen.SetActive(true);
    }
}


