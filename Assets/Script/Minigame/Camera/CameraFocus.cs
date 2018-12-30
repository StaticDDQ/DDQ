using UnityEngine;

public class CameraFocus : MonoBehaviour {

    [SerializeField] private float speed;
    public Vector3 pos;

	// Update is called once per frame
	private void Update () {
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed); 
	}
}
