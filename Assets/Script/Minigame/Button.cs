using UnityEngine;

public class Button : MonoBehaviour {

    [SerializeField] private Sprite pressed;
    [SerializeField] private GameObject triggerObject;

    private Sprite unpressed;
    private bool isPressed = false;

	// Use this for initialization
	void Start () {
    	unpressed = GetComponent<SpriteRenderer>().sprite;
	}

    public void PressedButton()
    {
        isPressed = !isPressed;
        if (isPressed)
        {
            GetComponent<SpriteRenderer>().sprite = pressed;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = unpressed;
        }

        // Trigger/disable an action
        triggerObject.GetComponent<SpriteAction>().SetCanDo(isPressed);
    }	
}
