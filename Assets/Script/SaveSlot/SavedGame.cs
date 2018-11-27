using UnityEngine;
using UnityEngine.UI;

public class SavedGame : MonoBehaviour {

    [SerializeField] private Image screenshot;
    [SerializeField] private Text dateSaved;

	public void SetSavedGame(byte[] screenBytes, string date)
    {
        Texture2D tex = new Texture2D(Screen.width,Screen.height);
        tex.LoadImage(screenBytes);
        screenshot.sprite = Sprite.Create(tex, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0, 0));
        dateSaved.text = date;
    }
}
