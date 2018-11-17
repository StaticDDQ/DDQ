using System.Collections.Generic;
using UnityEngine;

public class EliminateEnemies : MonoBehaviour {

    [SerializeField] private List<GameObject> enemies;

	public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);

        if(enemies.Count == 0)
        {
            GetComponent<SpriteAction>().SetCanDo(true);
        }
    }
}
