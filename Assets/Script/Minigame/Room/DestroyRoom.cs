using UnityEngine;

public class DestroyRoom : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint")) 
            Destroy(collision.gameObject);
    }
}
