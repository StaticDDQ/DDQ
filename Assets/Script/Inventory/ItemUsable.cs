using UnityEngine;

// Method used to have an object interact with different types of items
public class ItemUsable : MonoBehaviour {

    private Item currItem;
    private GameObject prefabItem;
    [SerializeField] private ItemType correctType;
    [SerializeField] private InventoryUI inventory;
    [SerializeField] private Transform parentItem;

    public void Interact(){
        if (currItem == null)
        {
            inventory.openInventory(true);
            inventory.canUse = this;
        } else
        {
            RemoveItem();
        }
    }

    public bool ValidItem(ItemType itemType)
    {
        return correctType == itemType;
    }

    public void ApplyItem(Item newItem)
    {
        currItem = newItem;
        prefabItem = Instantiate(newItem.prefabItem, parentItem);
        prefabItem.transform.localPosition = Vector3.zero;
        prefabItem.GetComponent<Rigidbody>().isKinematic = true;
        prefabItem.transform.localRotation = Quaternion.identity;
        prefabItem.transform.localScale = new Vector3(1, 1, 1);

        GetComponent<Animator>().Play("gearlockAnim");
        inventory.openInventory(false);
    }

    public void RemoveItem()
    {
        ItemDB._instance.AddItem(currItem);
        Destroy(prefabItem);
        currItem = null;
    }
}
