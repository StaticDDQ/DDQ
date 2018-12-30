using UnityEngine;

public class ChildSaveableObject : MonoBehaviour {

    [SerializeField] private string childObjDir;

    private void Start()
    {
        SaveSystem.instance.childSaveObjects.Add(this);
    }

    public void SetChild (string dir) {
        if (!dir.Equals(""))
        {
            GameObject tmp = Resources.Load(dir) as GameObject;
            transform.parent.GetComponent<ItemUsable>().ApplyItem(tmp.GetComponent<ItemHolder>().GetItem());

        } else if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
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
