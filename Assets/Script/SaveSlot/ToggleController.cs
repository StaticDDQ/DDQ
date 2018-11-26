using System.Collections;
using System;
using UnityEngine;

public class ToggleController : MonoBehaviour {

    private SaveSlot selectedSlot;
    private Camera mainCam;
    private Texture2D screenshot;

    private void Start()
    {
        mainCam = Camera.main;
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
            StartCoroutine(TakeScreenshot(selectedSlot, DateTime.Now.ToString()));
            SaveSystem.instance.Save(selectedSlot.GetIndex());
            selectedSlot.SetHasSaveFile(true);
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

        Sprite tempSprite = Sprite.Create(screenshot, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0, 0));

        slot.SetData(tempSprite, date);
    }

    public void DeleteSlot()
    {
        if(selectedSlot != null && selectedSlot.GetHasSaveFile())
        {
            SaveSystem.instance.DeleteGame(selectedSlot.GetIndex());
            selectedSlot.SetData(null, "");
            selectedSlot.SetHasSaveFile(false);
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
