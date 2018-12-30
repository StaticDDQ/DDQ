using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TranslateSprite : SpriteAction {

    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private int dir = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update () {
        if(canDo)
            rb.velocity = transform.up * speed * dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        dir = -dir;
    }
}
