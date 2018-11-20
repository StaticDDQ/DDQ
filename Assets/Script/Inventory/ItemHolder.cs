using UnityEngine;

public class ItemHolder : MonoBehaviour {

    [SerializeField] private Item currItem;

    public Item GetItem()
    {
        return this.currItem;
    }
}
