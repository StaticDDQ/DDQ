using UnityEngine;

public class PickUpable : MonoBehaviour {

    public bool carrying = false;
    public bool canRotate = false;
    public float rotSpeed = 90;

    private void OnCollisionEnter(Collision collision)
    {
        if (carrying && collision.transform.tag != "Player")
        {
            GetComponent<Rigidbody>().useGravity = true;
            transform.SetParent(null);
            carrying = false;
        }
    }

    private void Update()
    {
        if (canRotate)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.right, rotSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.right, rotSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Rotate(-Vector3.up, rotSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
            }
        }
    }
}
