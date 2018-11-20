using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Game Objects/Character")]
public class Character : ScriptableObject {

    public float CharSpeed;
    public float CrouchSpeed;
    public float CrouchHeight;
    public float InspectDist;
    public float CarryDist;
    public float InteractDist;

    public enum Action
    {
        Viewing,
        Holding,
        Free,
        Carrying
    }
    public Action def = Action.Free;


}
