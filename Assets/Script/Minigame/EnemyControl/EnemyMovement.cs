using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    
	[SerializeField] protected int maxHealth = 2;
    protected int currHealth;
    protected bool canShoot = false;

    // Initialize sprite and health
	private void Awake(){
		currHealth = maxHealth;
	}

    // Play explode animation when health reaches to 0
	public void TakeDamage(){
		currHealth -= 1;
		if (currHealth == 0) {
            canShoot = false;
            StartCoroutine(Explode());
        }
	}

    // Explode animation, temporary uninteractable for 1 second
    protected IEnumerator Explode()
    {
        GetComponent<Animator>().Play("explodeAnimEnemy");
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }

    public void SetCanShoot(bool canShoot)
    {
        this.canShoot = canShoot;
    }
}
