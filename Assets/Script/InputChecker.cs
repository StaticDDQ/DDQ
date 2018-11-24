using UnityEngine;

public class InputChecker : MonoBehaviour
{
    public static InputChecker instance;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public bool ButtonsEnabled = true;
    public bool switchedMinigame = false;
}
