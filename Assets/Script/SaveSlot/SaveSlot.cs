using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour {

    [SerializeField] private SlotObject slotObj;
    [SerializeField] private Color selectedColor;
    private Color defaultColor;

    private ToggleController controller;
    private Toggle mToggle;
    private GameObject dataControl;

    // Use this for initialization
    void Awake() {
        dataControl = transform.GetChild(0).gameObject;

        controller = transform.parent.GetComponent<ToggleController>();
        mToggle = GetComponent<Toggle>();
        mToggle.onValueChanged.AddListener(delegate
        {
            ToggleChanged(mToggle);
        });

        defaultColor = GetComponent<Image>().color;
    }

    private void ToggleChanged(Toggle change)
    {
        if (mToggle.isOn)
        {
            GetComponent<Image>().color = selectedColor;
            controller.SetSelectedSlot(this);
        } else
        {
            GetComponent<Image>().color = defaultColor;
            controller.SetSelectedSlot(null);
        }
    }

    public void SetData(byte[] imgByte, string date)
    {

        dataControl.SetActive(!date.Equals(""));
        slotObj.hasSavedFile = !date.Equals("");

        slotObj.screenshotBytes = imgByte;
        slotObj.dateSaved = date;

        dataControl.GetComponent<SavedGame>().SetSavedGame(imgByte,date);
    }

    public void TurnToggleOff()
    {
        mToggle.isOn = false;
        GetComponent<Image>().color = defaultColor;
    }

    public bool GetHasSaveFile()
    {
        return this.slotObj.hasSavedFile;
    }

    public int GetIndex()
    {
        return this.slotObj.index;
    }

    public SlotObject GetSlotObject()
    {
        return this.slotObj;
    }
}
