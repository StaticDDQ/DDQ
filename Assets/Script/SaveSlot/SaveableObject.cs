using UnityEngine;

public class SaveableObject : MonoBehaviour {

    [SerializeField] protected string objName;

	// Use this for initialization
	private void Start () {
        SaveSystem.instance.saveObjects.Add(this);
	}

    public void Load(Vector3 newPos, Quaternion newRot)
    {
        transform.position = newPos;
        transform.rotation = newRot;
    }

    public void DestroySaveable()
    {
        SaveSystem.instance.saveObjects.Remove(this);
        Destroy(gameObject);
    }

    public string GetName()
    {
        return this.objName;
    }
}
