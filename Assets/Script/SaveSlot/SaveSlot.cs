using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour {

    [SerializeField] private Color selectedColor;
    [SerializeField] private int index;
    private ToggleController controller;
    private Toggle mToggle;
    private GameObject dataControl;
    private Color defaultColor;

    private bool hasSaveFile = false;

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

    public void SetData(Sprite img, string date)
    {
        if(img != null)
        {
            dataControl.SetActive(true);
        } else
        {
            dataControl.SetActive(false);
        }
        dataControl.GetComponent<SavedGame>().SetSavedGame(img, date);
    }

    public void TurnToggleOff()
    {
        mToggle.isOn = false;
        GetComponent<Image>().color = defaultColor;
    }

    public bool GetHasSaveFile()
    {
        return this.hasSaveFile;
    }

    public void SetHasSaveFile(bool savedFile)
    {
        this.hasSaveFile = savedFile;
    }

    public int GetIndex()
    {
        return this.index;
    }
}
