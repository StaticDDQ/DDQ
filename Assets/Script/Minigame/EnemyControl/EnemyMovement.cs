using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	[SerializeField] protected int maxHealth = 2;
    protected EnemyManager manager;
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
            Explode();
        }
	}

    // Explode animation, temporary uninteractable for 1 second
    protected void Explode()
    {
        Destroy(gameObject, 1f);
        GetComponent<Animator>().Play("explodeAnimEnemy");
        GetComponent<Collider2D>().enabled = false;
        if(manager != null)
            manager.DestroyedEnemy();
    }

    public void SetCanShoot(EnemyManager manager, bool canShoot)
    {
        if(manager != null)
            this.manager = manager;
        this.canShoot = canShoot;
    }
}
