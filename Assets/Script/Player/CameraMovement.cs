using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private const float RANGE = 75;
    [SerializeField] private float mouseSpeed;

    private float pitch;
    private float yaw;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
	}

    // Update is called once per frame
    void Update () {
        if (DisabledInputs.ButtonsEnabled)
        {
            // Moving the pitch and yaw using mouse movement and applying a certain speed
            yaw += mouseSpeed * Input.GetAxis("Mouse X");
            pitch -= mouseSpeed * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(Mathf.Clamp(pitch, -RANGE, RANGE), yaw, 0);
        }
    }
}
