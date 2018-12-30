using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {

    [SerializeField] private bool isEnemy = false;
    [SerializeField] private float bulletSpeed;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
        Destroy(gameObject, 1.5f);
    }

    // The bullet has different interactions depending on what it collides with
    void OnCollisionEnter2D(Collision2D other)
    {
        if (isEnemy && other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<MoveScreenObject>().LoseHealth();
        }
        else if (!isEnemy && other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyMovement>().TakeDamage();
        }
        else if (other.gameObject.tag == "Redirect")
        {
            other.gameObject.GetComponent<RedirectProjectile>().HitProjectile(!isEnemy);
        }
        Destroy(gameObject);
    }
}
