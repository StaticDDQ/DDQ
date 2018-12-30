using UnityEngine;

public class RedirectProjectile : MonoBehaviour {

    [SerializeField] private Transform otherSide;
    [SerializeField] private GameObject playerProj;
    [SerializeField] private GameObject enemyProj;

    private GameObject chosenProj;
	
	public void HitProjectile (bool player) {
        // If it is an enemy bullet or not
        if (player)
            chosenProj = playerProj;
        else
            chosenProj = enemyProj;

        var bullet = Instantiate(chosenProj, otherSide.position + otherSide.up * 0.2f, otherSide.rotation);

        Destroy(bullet, 1.1f);
    }
}
