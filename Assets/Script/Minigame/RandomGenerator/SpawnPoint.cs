using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public int openingDirection;
    // 1 b door
    // 2 t door
    // 3 l door
    // 4 r door

    private RoomTemplate templates;
    private int rand;
    private bool spawned = false;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        if (!spawned)
        {
            if (openingDirection == 1)
            {
                //spawn b door
                rand = Random.Range(0, templates.bRooms.Length);
                Instantiate(templates.bRooms[rand], transform.position, templates.bRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                //spawn t door
                rand = Random.Range(0, templates.tRooms.Length);
                Instantiate(templates.tRooms[rand], transform.position, templates.tRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                //spawn l door
                rand = Random.Range(0, templates.lRooms.Length);
                Instantiate(templates.lRooms[rand], transform.position, templates.lRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                //spawn r door
                rand = Random.Range(0, templates.rRooms.Length);
                Instantiate(templates.rRooms[rand], transform.position, templates.rRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SpawnPoint"))
        {
            if (!collision.GetComponent<SpawnPoint>().spawned && !spawned)
            {
                Instantiate(templates.cRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
