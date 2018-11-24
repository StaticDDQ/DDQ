using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private const float RANGE = 75;
    [SerializeField] PlayerCamera cam;

    private float yaw;
    private float pitch;

    // Use this for initialization
    private void Start () {
        //Cursor.visible = false;
	}

    // Update is called once per frame
    private void Update () {
        if (InputChecker.instance.ButtonsEnabled)
        {
            // Moving the pitch and yaw using mouse movement and applying a certain speed
            yaw += cam.MouseSpeed * Input.GetAxis("Mouse X");
            pitch -= cam.MouseSpeed * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(Mathf.Clamp(pitch, -RANGE, RANGE), yaw, 0);
        }
    }
}
