using UnityEngine;

public class HealthGain : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<MoveScreenObject>().GainHealth())
        {
            Destroy(this.gameObject);
        }
    }
}
