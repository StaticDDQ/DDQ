using UnityEngine;
using UnityEngine.UI;

public class SavedGame : MonoBehaviour {

    [SerializeField] private Image screenshot;
    [SerializeField] private Text dateSaved;

	public void SetSavedGame(Sprite screen, string date)
    {
        screenshot.sprite = screen;
        dateSaved.text = date;
    }
}
