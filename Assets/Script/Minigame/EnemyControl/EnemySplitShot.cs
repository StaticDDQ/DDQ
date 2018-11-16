using UnityEngine;

public class EnemySplitShot : EnemyMovement {

    [SerializeField] private GameObject bulletSprite;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float fireRate;
    private float fireDelay = 0;
    private bool runOnce = false;


    private void Update()
    {
        if (canShoot && Time.time > fireDelay)
        {
            fireDelay = Time.time + fireRate;
            if (runOnce)
            {
                ShootProjectile(transform.up, 0);
                ShootProjectile(-transform.up, 180);
                ShootProjectile(-transform.right,90);
                ShootProjectile(transform.right,270);
            }
            else
                runOnce = true;
        }
    }

    private void ShootProjectile(Vector3 dir, float rotate)
    {
        var bullet = (GameObject)Instantiate(bulletSprite, transform.position + dir * 0.9f, Quaternion.Euler(0,0,transform.eulerAngles.z + rotate));

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
        Destroy(bullet, 1.1f);
    }
}
