using UnityEngine;
using System.Collections;

public class MoveScreenObject : MonoBehaviour {

    [SerializeField] private float force;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject verdict;
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private SpriteRenderer[] healthbar;
    [SerializeField] private Camera cam;
    private Rigidbody2D rigid;
    private int health = 3;
    private bool isInvulnerable = false;
    private bool hasExploded = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!hasExploded)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = cam.ScreenToWorldPoint(mousePos);

            Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

            transform.up = direction;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootProjectile();
            }
        }
    }

    private void ShootProjectile()
    {
        var bullet = (GameObject)Instantiate(bulletTrail, transform.position + transform.up * 0.9f, transform.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
        Destroy(bullet, 1.1f);
    }

    private void FixedUpdate()
    {
        if (!hasExploded)
        {
            float moveHorizontal = Input.GetAxis("Horizontal") * force;
            float moveVertical = Input.GetAxis("Vertical") * force;
            Vector2 dir = new Vector2(moveHorizontal, moveVertical);

            rigid.AddForce(dir, ForceMode2D.Force);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!isInvulnerable && collision.gameObject.tag == "Enemy")
        {
            LoseHealth();
        }
    }

    public void LoseHealth()
    {
        if (!isInvulnerable)
        {
            health -= 1;
            healthbar[health].enabled = false;
            if (health == 0)
            {
                StartCoroutine(Explode());
                return;
            }
            StartCoroutine(Invulnerable());
        }
    }

    public bool GainHealth()
    {
        if(health < 3)
        {
            healthbar[health].enabled = true;
            health += 1;
            return true;
        }
        return false;
    }

    private IEnumerator Invulnerable()
    {
        isInvulnerable = true;
        GetComponent<Animator>().Play("invulnerableState");
        yield return new WaitForSeconds(2);
        isInvulnerable = false;
    }

    // Explode animation, temporary uninteractable for 1 second
    private IEnumerator Explode()
    {
        hasExploded = true;
        GetComponent<Animator>().Play("explodeAnimPlayer");
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        verdict.GetComponent<VerdictGame>().Verdict(false);
        Destroy(gameObject);
    }
}
