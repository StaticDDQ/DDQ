using UnityEngine;

public class TranslateSprite : SpriteAction {

    [SerializeField] private int dir = 1;
    [SerializeField] private float speed;

	// Update is called once per frame
	private void Update () {

        if (canDo)
        {
            GetComponent<Rigidbody2D>().velocity = transform.up * speed * dir;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        dir = -dir;
    }
}
