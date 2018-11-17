using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] protected GameObject listener;
	[SerializeField] protected int maxHealth = 2;
    protected int currHealth;
    protected bool canShoot = false;

    // Initialize sprite and health
	private void Awake(){
		currHealth = maxHealth;
	}

    // Play explode animation when health reaches to 0
	public virtual void TakeDamage(){
		currHealth -= 1;
		if (currHealth == 0) {

            if (listener != null)
            {
                listener.GetComponent<EliminateEnemies>().RemoveEnemy(this.gameObject);
            }

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

    public virtual void SetCanShoot(bool canShoot)
    {
        this.canShoot = canShoot;
    }
}
