using UnityEngine;

[CreateAssetMenu(fileName = "PickUp", menuName = "Game Objects/PickUp")]
public class PickUpObjects : ScriptableObject {

    public Vector3 placedScale;
    public Vector3 placedPosition;
    public Quaternion placedRotation;

}
