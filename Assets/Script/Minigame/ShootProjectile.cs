using UnityEngine;

public class ShootProjectile : MonoBehaviour {

    [SerializeField] private GameObject bulletSprite;
    [SerializeField] private float bulletSpeed = 10f;

    // Spawn a bullet
    public void Shoot()
    {
        var bullet = (GameObject)Instantiate(bulletSprite, transform.position + transform.up * 0.9f, transform.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
        Destroy(bullet, 1.1f);
    }
}
