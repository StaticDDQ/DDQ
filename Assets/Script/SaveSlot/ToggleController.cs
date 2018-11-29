using System;
using UnityEngine;
using System.Collections;

public class ToggleController : MonoBehaviour {

    private SaveSlot selectedSlot;
    private Camera mainCam;
    private Texture2D screenshot;
    [SerializeField] private SaveSlot[] slots;

    private void Start()
    {
        mainCam = Camera.main;
        foreach (SlotObject slot in SaveSlotManager.instance.GetSlots())
        {
            slots[slot.index].SetData(slot.screenshotBytes,slot.dateSaved);
        }
    }

    public void SetSelectedSlot(SaveSlot slot)
    {
        if(selectedSlot != null) {
            selectedSlot.TurnToggleOff();
        }
        selectedSlot = slot;
    }

    public void SavingSlot()
    {
        if(selectedSlot != null)
        {
            SaveSystem.instance.Save(selectedSlot.GetIndex());
            StartCoroutine(TakeScreenshot(selectedSlot, DateTime.Now.ToString()));
        }
    }

    private IEnumerator TakeScreenshot(SaveSlot slot, string date)
    {
        yield return new WaitForEndOfFrame();

        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        mainCam.targetTexture = rt;
        screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        mainCam.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        mainCam.targetTexture = null;
        RenderTexture.active = null;

        Destroy(rt);

        slot.SetData(screenshot.EncodeToPNG(), date);

        SaveSlotManager.instance.AddSaveSlot(slot.GetSlotObject());
    }

    public void DeleteSlot()
    {
        if(selectedSlot != null && selectedSlot.GetHasSaveFile())
        {
            SaveSystem.instance.DeleteGame(selectedSlot.GetIndex());
            SaveSlotManager.instance.RemoveSaveSlot(selectedSlot.GetSlotObject().index);
            selectedSlot.SetData(null,"");
        }
    }

    public void LoadSlot()
    {
        if (selectedSlot != null && selectedSlot.GetHasSaveFile())
        {
            SaveSystem.instance.Load(selectedSlot.GetIndex());
        }
    }
}
