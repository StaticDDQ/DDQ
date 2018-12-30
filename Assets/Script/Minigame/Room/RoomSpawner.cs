using UnityEngine;

public class RoomSpawner : MonoBehaviour {

    public int openingDir;
    // 1 -> R
    // 2 -> L
    // 3 -> D
    // 4 -> U

    private bool spawned = false;
    private RoomTemplate templates;

    public float waitTime = 4f;

    private void Start()
    {
        Destroy(gameObject, waitTime);
        templates = RoomTemplate.instance;
        Invoke("Spawn", 1f);
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

            templates.AddRoom(transform.parent.gameObject);

            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (templates != null && collision.CompareTag("SpawnPoint") && !collision.GetComponent<RoomSpawner>().spawned && !spawned)
        {
            templates.SpawnWallRoom(transform.position);

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        spawned = true;
    }
}
