using UnityEngine;

public class Crouch : MonoBehaviour {

    // Attributes that should not be adjusted, inital values for player's height
    private float length;
    private float walkSpeed;
    private CapsuleCollider capCollider;

    [SerializeField] private float crouchSpeed = 1.75f;

    // Attributes that will change depending if the player is crouched or not
    private float l;

    private bool isCrouched = false;

    // Use this for initialization
    void Start()
    {
        walkSpeed = GetComponent<PlayerMovement>().Speed;

        capCollider = GetComponent<CapsuleCollider>();
        length = capCollider.height;

        l = length;
    }

	// Update is called once per frame
	void Update () {

		if (InputChecker.instance.ButtonsEnabled && Input.GetKeyDown (KeyCode.LeftShift)) {
			isCrouched = !isCrouched;
            if (isCrouched)
            {
                l = length * 0.5f;
                GetComponent<PlayerMovement>().Speed = crouchSpeed;
            }
            else
            {
                l = length;
                GetComponent<PlayerMovement>().Speed = walkSpeed;
            }
		}
        capCollider.height = Mathf.Lerp (capCollider.height, l, 3* Time.deltaTime);
	}
}
