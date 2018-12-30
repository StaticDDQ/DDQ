using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowTarget : MonoBehaviour {

    private Transform target;
    [SerializeField] protected float speed = 2f;
    [SerializeField] protected float rotSpeed = 2f;

    protected Rigidbody2D rb;

    // Use this for initialization
    private void Start () {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	private void FixedUpdate () {
        
        if(target != null)
        {
            Vector2 dir = (Vector2)target.position - rb.position;

            dir.Normalize();

            float rotAmount = Vector3.Cross(dir, transform.up).z;
            rb.angularVelocity = -rotAmount * rotSpeed;
        }

        rb.velocity = transform.up * speed;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D cd = collision.contacts[0].collider;
        transform.rotation = Quaternion.LookRotation(transform.forward, target.position - transform.position);
        if (cd.tag == "Player")
        {
            collision.gameObject.GetComponent<MoveScreenObject>().LoseHealth();
        }
    }
}
