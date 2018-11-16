using UnityEngine;

public class EnemyShooter : EnemyMovement {

    [SerializeField] private GameObject bulletSprite;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float fireRate;
    private float fireDelay = 0;
    private bool runOnce = false;

    private void Update() {
		if(canShoot && Time.time > fireDelay)
        {
            fireDelay = Time.time + fireRate;
            if (runOnce)
                ShootProjectile();
            else
                runOnce = true;
        }
	}

    private void ShootProjectile()
    {
        var bullet = (GameObject)Instantiate(bulletSprite, transform.position + transform.up * 0.9f, transform.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
        Destroy(bullet, 1.1f);
    }
}
