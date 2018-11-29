using UnityEngine;

public class ChildSaveableObject : MonoBehaviour {

    [SerializeField] private string childObjDir;

    private void Start()
    {
        SaveSystem.instance.childSaveObjects.Add(this);
    }

    public void SetChild (string dir) {
        GameObject tmp = Instantiate(Resources.Load(dir) as GameObject);
        tmp.transform.SetParent(this.transform);
        tmp.transform.localPosition = Vector3.zero;
        tmp.transform.localRotation = Quaternion.identity;
    }

    public string GetDir()
    {
        return this.childObjDir;
    }

    public void SetDir(string dir)
    {
        this.childObjDir = dir;
    }
}
