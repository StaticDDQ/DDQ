using UnityEngine;

// Method used to have an object interact with different types of items
public class ItemUsable : MonoBehaviour {

    private Item currItem;
    private GameObject prefabItem;
    [SerializeField] private ItemType correctType;
    [SerializeField] private InventoryUI inventory;
    [SerializeField] private Transform parentItem;

    public void Interact(){
        if (parentItem.childCount == 0)
        {
            InputChecker.instance.ButtonsEnabled = false;
            PauseButton.instance.ToggleCursorState(true);

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
        prefabItem.transform.localRotation = Quaternion.identity;
        prefabItem.transform.localScale = new Vector3(1, 1, 1);

        Destroy(prefabItem.GetComponent<Collider>());
        Destroy(prefabItem.GetComponent<Rigidbody>());
        Destroy(prefabItem.GetComponent<SaveableObject>());

        GetComponent<Animator>().Play("gearlockAnim");
        inventory.openInventory(false);
        parentItem.GetComponent<ChildSaveableObject>().SetDir(currItem.itemName);

        InputChecker.instance.ButtonsEnabled = true;
        PauseButton.instance.ToggleCursorState(false);
    }

    public void RemoveItem()
    {
        ItemDB._instance.AddItem(currItem);
        Destroy(prefabItem);
        currItem = null;
        parentItem.GetComponent<ChildSaveableObject>().SetDir("");
    }
}
