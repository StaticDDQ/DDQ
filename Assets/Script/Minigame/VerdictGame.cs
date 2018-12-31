using UnityEngine;
using UnityEngine.UI;

public class VerdictGame : MonoBehaviour {

    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failSprite;
    private bool wonGame = false;

    // Play animation when either all enemies are destroyed or player is destroyed
    public void Verdict(bool isSuccess)
    {
        GetComponent<Image>().sprite = (isSuccess) ? successSprite : failSprite;
        wonGame = isSuccess;

        ShowVerdict();
    }

    private void ShowVerdict()
    {
        GetComponent<Animator>().Play("VerdictAnim");
        Time.timeScale = 0.0f;
    }

    public bool GetWonGame()
    {
        return this.wonGame;
    }
}
