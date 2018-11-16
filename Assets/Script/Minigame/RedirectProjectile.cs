using UnityEngine;

public class RedirectProjectile : MonoBehaviour {

    [SerializeField] private Transform otherSide;
    [SerializeField] private GameObject playerProj;
    [SerializeField] private GameObject enemyProj;
    [SerializeField] private float bulletSpeed = 10f;
    private GameObject chosenProj;
	
	public void HitProjectile (bool player) {
        // If it is an enemy bullet or not
        if (player)
            chosenProj = playerProj;
        else
            chosenProj = enemyProj;

        var bullet = Instantiate(chosenProj, otherSide.position + otherSide.up * 0.1f, transform.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
        Destroy(bullet, 1.1f);
    }
}
