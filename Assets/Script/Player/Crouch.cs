using UnityEngine;

public class Crouch : MonoBehaviour {

    // Attributes that should not be adjusted, inital values for player's height
    private float length;
    private float walkSpeed;
    private CapsuleCollider capCollider;

    // Attributes that will change depending if the player is crouched or not
    private float l;
    private bool isCrouched = false;

    // Use this for initialization
    void Start()
    {
        walkSpeed = GetComponent<PlayerMovement>().character.Speed;

        capCollider = GetComponent<CapsuleCollider>();
        length = capCollider.height;

        l = length;
    }

	// Update is called once per frame
	void Update () {

		if (InputChecker.instance.ButtonsEnabled && Input.GetKeyDown (KeyCode.C)) {
			isCrouched = !isCrouched;
            if (isCrouched)
            {
                l = length * 0.5f;
                GetComponent<PlayerMovement>().character.Speed = GetComponent<PlayerMovement>().character.CrouchSpeed;
            }
            else
            {
                l = length;
                GetComponent<PlayerMovement>().character.Speed = walkSpeed;
            }
		}
        capCollider.height = Mathf.Lerp (capCollider.height, l, 3* Time.deltaTime);
	}

    public bool GetIsCrouched()
    {
        return this.isCrouched;
    }
}
