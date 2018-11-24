using UnityEngine;
using System.Collections;

public class VerdictGame : MonoBehaviour {

    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failSprite;
    private bool wonGame = false;

    // Play animation when either all enemies are destroyed or player is destroyed
    public void Verdict(bool isSuccess)
    {
        if (!isSuccess)
        {
            GetComponent<SpriteRenderer>().sprite = failSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = successSprite;
            wonGame = true;
        }
        StartCoroutine(ShowVerdict());
    }

    private IEnumerator ShowVerdict()
    {
        GetComponent<Animator>().Play("VerdictAnim");
        yield return new WaitForSeconds(1);
        Time.timeScale = 0.0f;
    }

    public bool GetWonGame()
    {
        return this.wonGame;
    }
}
