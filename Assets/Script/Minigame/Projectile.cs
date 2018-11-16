using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private bool isEnemy = false;

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
        else if (other.gameObject.tag == "Button")
        {
            other.gameObject.GetComponent<Button>().PressedButton();
        }
        else if (other.gameObject.tag == "Redirect")
        {
            other.gameObject.GetComponent<RedirectProjectile>().HitProjectile(!isEnemy);
        }
        Destroy(gameObject);
    }
}
