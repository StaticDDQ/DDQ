using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    [SerializeField] private List<GameObject> enemies;

	public void EnableEnemies()
    {
        SetObjects(true);
    }

    public void DisableEnemies()
    {
        SetObjects(false);
    }

    private void SetObjects(bool isTrue)
    {
        foreach (GameObject enemy in enemies)
        {
            if(enemy != null)
            {
                enemy.GetComponent<Animator>().enabled = isTrue;
                enemy.GetComponent<EnemyMovement>().SetCanShoot(isTrue);
            }
        }
    }
}
