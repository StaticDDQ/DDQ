using UnityEngine;

public class EnemySplitShot : EnemyMovement {

    [SerializeField] private GameObject bulletSprite;
    [SerializeField] private float fireRate;
    private float fireDelay;

    private void Start()
    {
        fireDelay = Time.time + fireRate;
    }

    private void Update()
    {
        if (canShoot && Time.time > fireDelay)
        {
            fireDelay = Time.time + fireRate;

            ShootProjectile(transform.up, 0);
            ShootProjectile(-transform.up, 180);
            ShootProjectile(-transform.right,90);
            ShootProjectile(transform.right,270);
        }
    }

    private void ShootProjectile(Vector3 dir, float rotate)
    {
        Instantiate(bulletSprite, transform.position + dir * 0.9f, Quaternion.Euler(0,0,transform.eulerAngles.z + rotate));
    }
}
