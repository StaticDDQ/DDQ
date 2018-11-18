using UnityEngine;

[RequireComponent(typeof(GateControl))]
public class EliminateEnemies : MonoBehaviour {

    [SerializeField] private int enemies;

	public void RemoveEnemy()
    {
        if(--enemies == 0)
        {
            GetComponent<SpriteAction>().SetCanDo(true);
        }
    }
}
