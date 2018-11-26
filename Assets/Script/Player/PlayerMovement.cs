using UnityEngine;

// Must have rigidbody for the player
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    private new Rigidbody rigidbody;
    private Transform cam;
    private float speed;

    public Character character;

    [HideInInspector]
    private Vector3 moveDir = Vector3.zero;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        speed = character.Speed;
    }

    private void FixedUpdate()
    {
        if (InputChecker.instance.ButtonsEnabled)
        {
            // move based on where the camera is facing as well
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = cam.TransformDirection(moveDir);
            moveDir.y = 0f;

            // if player wants to sprint
            if (Input.GetKey(KeyCode.LeftShift) && !GetComponent<Crouch>().GetIsCrouched())
            {
                speed = character.Speed * 1.5f;
            }
            else
            {
                speed = character.Speed;
            }

            rigidbody.MovePosition(rigidbody.position + moveDir * speed * Time.fixedDeltaTime);
        }
    }
}
