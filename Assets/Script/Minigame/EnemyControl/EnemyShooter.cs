using UnityEngine;

public class EnemyShooter : EnemyMovement {

    [SerializeField] private GameObject bulletSprite;
    [SerializeField] private float fireRate;
    private float fireDelay;

    private void Start()
    {
        fireDelay = Time.time + fireRate;
    }

    private void Update() {
		if(canShoot && Time.time > fireDelay)
        {
            fireDelay = Time.time + fireRate;
            ShootProjectile();
        }
	}

    private void ShootProjectile()
    {
        Instantiate(bulletSprite, transform.position + transform.up * 0.9f, transform.rotation);
    }
}
