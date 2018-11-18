using UnityEngine;

// Must have rigidbody for the player
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    private new Rigidbody rigidbody;
    private Transform cam;
    private float distToGround;

    public float Speed = 5f;
    [SerializeField] private float jumpHeight = 5f;

    [HideInInspector]
    public Vector3 moveDir = Vector3.zero;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // jump is added in update to prevent rigidbody bug where jump force is much higher- if player is on top of a moving object
    private void Update()
    {
        if (DisabledInputs.ButtonsEnabled && Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (DisabledInputs.ButtonsEnabled)
        {
            // move based on where the camera is facing as well
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = cam.TransformDirection(moveDir);
            moveDir.y = 0f;

            // if player wants to sprint
            if (Input.GetKey(KeyCode.LeftShift) && IsGrounded())
            {
                Speed = 8f;
            }
            else
            {
                Speed = 5f;
            }

            rigidbody.MovePosition(rigidbody.position + moveDir * Speed * Time.fixedDeltaTime);
        }
    }

    // cast a line downwards and check if an object is directly under the player
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
