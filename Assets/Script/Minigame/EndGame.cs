using UnityEngine;

public class EndGame : MonoBehaviour {

    [SerializeField] private GameObject verdict;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            verdict.GetComponent<VerdictGame>().Verdict(true);
            collision.GetComponent<MoveScreenObject>().enabled = false;
        }
    }
}
