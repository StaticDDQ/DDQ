using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public bool canSpawn = false;
    private RoomTemplate template;
    private int enemySpawned = 0;
    [SerializeField] private List<Transform> spawners;
    [SerializeField] private Animator gateAnim;
    [SerializeField] private GameObject heal;

    private void Start()
    {
        template = RoomTemplate.instance;
    }

    public void StartSpawning () {
        if (canSpawn)
        {
            gateAnim.Play("CloseGates");

            enemySpawned = Random.Range((int)template.GetEnemyCountRange().x, (int)template.GetEnemyCountRange().y);
            Transform randSpawner;

            for (int i = 0; i < enemySpawned; i++)
            {
                randSpawner = spawners[Random.Range(0, spawners.Count - 1)];
                spawners.Remove(randSpawner);
                Spawned(randSpawner);
            }
        }
	}

    // Opens gate once enemies are down. spawn heal
    public void DestroyedEnemy()
    {
        enemySpawned--;
        if(enemySpawned == 0)
        {
            gateAnim.Play("OpenGates");
            RoomTemplate.instance.HasFinishedEnemyRooms();
            Instantiate(heal, transform.position, Quaternion.identity);
        }
    }

    // spawn random enemy at designated spawner location
    private void Spawned(Transform spawnerLocation)
    {
        int rand = Random.Range(0, template.enemyTypes.Count - 1);
        var enemy = Instantiate(template.enemyTypes[rand], spawnerLocation.position, Quaternion.identity);

        enemy.GetComponent<EnemyMovement>().SetCanShoot(this, true);
    }
}
