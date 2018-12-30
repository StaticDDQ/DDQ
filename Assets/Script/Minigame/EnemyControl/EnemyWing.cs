using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyWing : EnemyMovement {

    [SerializeField] private float speed = 2f;
    private Rigidbody2D rb;
    private bool isColliding = false;

    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate () {
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isColliding)
        {
            transform.Rotate(0, 0, transform.rotation.z + 180 + Random.Range(0,45));

            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<MoveScreenObject>().LoseHealth();
            }
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }
}
