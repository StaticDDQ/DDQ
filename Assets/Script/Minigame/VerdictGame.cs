using UnityEngine;
using System.Collections;

public class VerdictGame : MonoBehaviour {

    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failSprite;

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
            InputChecker.instance.wonMinigame = true;
        }
        StartCoroutine(ShowVerdict(isSuccess));
    }

    private IEnumerator ShowVerdict(bool isSuccess)
    {
        GetComponent<Animator>().Play("VerdictAnim");
        yield return new WaitForSeconds(1);
        Time.timeScale = 0.0f;
    }
}
