using UnityEngine;

public class LoadLevel : MonoBehaviour {

    [SerializeField] private int level;

    // immediately starts a level if player lands on this trigger
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneFade.instance.StartLevel(level);
        }
    }
}
