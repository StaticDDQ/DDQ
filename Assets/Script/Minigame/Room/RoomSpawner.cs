using UnityEngine;

public class RoomSpawner : MonoBehaviour {

    public int openingDir;
    // 1 -> R
    // 2 -> L
    // 3 -> D
    // 4 -> U

    private bool spawned = false;
    private RoomTemplate templates;

    public float waitTime = 3f;

    private void Start()
    {
        Destroy(gameObject, waitTime);
        templates = RoomTemplate.instance;
        Invoke("Spawn", 2f);
    }

    private void Spawn()
    {
        if (!spawned)
        {
            if (templates.CheckRoomCounter())
            {
                templates.SpawnRoom(openingDir - 1, transform.position);
                templates.roomCounter++;
            } else
            {
                templates.SpawnEndRoom(openingDir - 1, transform.position);
            }
        } else
        {
            templates.SpawnWallRoom(transform.position);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (templates != null && collision.CompareTag("SpawnPoint") && !collision.GetComponent<RoomSpawner>().spawned && !spawned)
        {
            spawned = true;
            collision.GetComponent<RoomSpawner>().spawned = true;
        }
    }
}
