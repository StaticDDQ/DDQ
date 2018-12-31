using UnityEngine;

public class DestroyRoom : MonoBehaviour {

    [SerializeField] private float waitTime = 5f;

    private void Start()
    {
        Destroy(gameObject, waitTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
        {
            Destroy(collision.gameObject);
        }
    }
}
